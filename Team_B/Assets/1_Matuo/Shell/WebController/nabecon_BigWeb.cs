using System.Collections;
using UnityEngine;

public class Nabecon_Big : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 10f;

    [Header("爆発設定")]
    public GameObject bigExplosionPrefab;
    public float explosionRadius = 3f; // 爆発範囲
    public int scorePerEnemy = 50;

    private Rigidbody2D rb;
    private bool isStopped = false;
    private bool hasHitEnemy = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.up * speed;
        }
    }

    void Update()
    {
        if (isStopped && Input.GetKeyDown(KeyCode.L))
        {
            Explode();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasHitEnemy && other.CompareTag("Enemy"))
        {
            hasHitEnemy = true;
            StopWeb();

            // 敵を停止させる
            EnemyBaseController enemy = other.GetComponent<EnemyBaseController>();
            if (enemy != null)
                enemy.Stop();

            Debug.Log("BigWeb が敵にヒットして停止");
        }
    }

    private void StopWeb()
    {
        isStopped = true;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void Explode()
    {
        // 爆発エフェクト生成
        if (bigExplosionPrefab != null)
        {
            Instantiate(bigExplosionPrefab, transform.position, Quaternion.identity);
        }

        // 範囲内の敵を破壊・スコア加算
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Destroy(hit.gameObject);

                // スコア加算
                PlayerStatus player = FindObjectOfType<PlayerStatus>();
                if (player != null)
                    player.AddScore(scorePerEnemy);
            }
        }

        Destroy(gameObject);
    }

    // Sceneビューで爆発範囲を確認
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
