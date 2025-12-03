using UnityEngine;

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
            rb.linearVelocity = moveDirection.normalized * speed;
        else
            rb.linearVelocity = Vector2.zero;
    }

    public void Stop()
    {
        isStopped = true;
    }

    void Update()
    {
        if (isStopped && Input.GetKeyDown(KeyCode.L))
            Explode();
    }

    /// <summary>
    /// 敵死亡（他スクリプトが呼ぶ用）
    /// </summary>
    public void Die()
    {
        // スコア加算
        if (ScoreBoard.Instance != null)
            ScoreBoard.Instance.AddScore(50);

        // オーブ生成
        SpawnOrb();

        // エフェクト
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    /// <summary>
    /// 範囲爆発
    /// </summary>
    private void Explode()
    {
        // エフェクト
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // 範囲 damage
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                // ★ 敵の Die を呼ぶ
                EnemyBaseController e = hit.GetComponent<EnemyBaseController>();
                if (e != null)
                    e.Die();
            }
            else if (hit.CompareTag("Web"))
            {
                Destroy(hit.gameObject);
            }
        }

        Destroy(gameObject);
    }

    private void SpawnOrb()
    {
        int r = Random.Range(0, 100);
        GameObject orb = null;

        if (r < redRate) orb = redOrbPrefab;
        else if (r < redRate + greenRate) orb = greenOrbPrefab;
        else orb = blueOrbPrefab;

        if (orb != null)
            Instantiate(orb, transform.position + Vector3.up * 1f, Quaternion.identity);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
