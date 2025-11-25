using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (isStopped && Input.GetKeyDown(KeyCode.L))
        {
            Explode();
        }
    }

    // 爆発処理
    private void Explode()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Web") || hit.CompareTag("Enemy"))
            {
                Destroy(hit.gameObject);
            }
        }

        SpawnOrb();

        Destroy(gameObject);
    }

    // ランダムオーブ生成
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
}
