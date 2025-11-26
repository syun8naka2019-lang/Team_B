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
            rb.velocity = moveDirection.normalized * speed;
        else
            rb.velocity = Vector2.zero;
    }

    public void Stop()
    {
        isStopped = true;
        rb.velocity = Vector2.zero;
    }

    void Update()
    {
        // 停止中に Lキーで爆発
        if (isStopped && Input.GetKeyDown(KeyCode.L))
            Explode();
    }

    private void Explode()
    {
        // 爆発エフェクト
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // 範囲内の Enemy / Web を破壊
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                // 敵撃破スコア加算
                ScoreBoard sb = FindObjectOfType<ScoreBoard>();
                if (sb != null)
                    sb.AddScore(50);

                Destroy(hit.gameObject);
            }
            else if (hit.CompareTag("Web"))
            {
                Destroy(hit.gameObject);
            }
        }

        // ランダムでオーブ生成
        SpawnOrb();

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

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Sceneビューで爆発範囲を可視化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
