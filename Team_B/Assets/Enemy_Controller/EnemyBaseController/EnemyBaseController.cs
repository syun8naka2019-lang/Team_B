using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 共通の敵コントローラ
/// ・移動（Inspectorで方向指定）
/// ・Web で停止
/// ・Lキーで爆発
/// ・爆発で敵/Web破壊
/// ・爆発時にランダムでオーブ生成
/// </summary>
public class EnemyBaseController : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 2f;
    public Vector2 moveDirection = Vector2.down;

    private bool isStopped = false;
    private Rigidbody2D rb;

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
    /// Web が当たった時に呼ぶ → 敵を停止
    /// </summary>
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
        // 停止中に L キーで爆発
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

        // ランダムでオーブ生成
        SpawnOrb();

        // 自分を破壊
        Destroy(gameObject);
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

    // Sceneビューで爆発範囲可視化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
