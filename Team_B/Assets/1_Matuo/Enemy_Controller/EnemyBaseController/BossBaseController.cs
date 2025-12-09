using System.Collections;
using UnityEngine;

/// <summary>
/// ボス共通コントローラ
/// ・Web の爆発ダメージに耐える（デフォ50回 / Inspector変更可）
/// ・停止 → Lキーで爆発
/// ・爆発で敵/Web破壊
/// ・HP0で死亡
/// ・オーブランダム生成
/// </summary>
public class BossBaseController : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 2f;
    public Vector2 moveDirection = Vector2.down;

    private bool isStopped = false;
    private Rigidbody2D rb;

    [Header("ボスHP設定")]
    public int maxHp = 50;       // Web爆発耐久回数（Inspectorで変更可能）
    private int currentHp;

    [Header("爆発設定")]
    public GameObject explosionPrefab;
    public float explosionRadius = 2f;

    [Header("オーブ設定")]
    public GameObject redOrbPrefab;
    public GameObject greenOrbPrefab;
    public GameObject blueOrbPrefab;

    public int redRate = 70;
    public int greenRate = 20;
    public int blueRate = 10;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHp = maxHp;       // HP初期化
    }

    void FixedUpdate()
    {
        if (!isStopped)
        {
            rb.linearVelocity = moveDirection.normalized * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Webに触れたときのダメージ処理
    /// </summary>
    public void DamageByWeb()
    {
        currentHp--;

        Debug.Log("Boss Web Damage!  残りHP: " + currentHp);

        if (currentHp <= 0)
        {
            Explode();
        }
    }

    public void Stop()
    {
        if (!isStopped)
        {
            isStopped = true;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void Update()
    {
        // 停止中はLキーで自爆
        if (isStopped && Input.GetKeyDown(KeyCode.L))
        {
            Explode();
        }
    }

    /// <summary>
    /// 爆発処理
    /// </summary>
    private void Explode()
    {
        // 爆発エフェクト
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // 範囲内の Enemy / Web を破壊
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Web") || hit.CompareTag("Enemy"))
            {
                Destroy(hit.gameObject);
            }
        }

        SpawnOrb();                 // Randomオーブ生成
        Destroy(gameObject);        // ボス破壊
        Debug.Log("Boss Defeated!");
    }

    /// <summary>
    /// ランダムオーブ生成
    /// </summary>
    private void SpawnOrb()
    {
        int r = Random.Range(0, 100);
        GameObject orb = null;

        if (r < redRate)
            orb = redOrbPrefab;
        else if (r < redRate + greenRate)
            orb = greenOrbPrefab;
        else
            orb = blueOrbPrefab;

        if (orb != null)
        {
            Instantiate(orb, transform.position + Vector3.up * 1f, Quaternion.identity);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
