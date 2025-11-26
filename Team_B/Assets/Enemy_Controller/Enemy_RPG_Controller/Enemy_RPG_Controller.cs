using UnityEngine;

public class Enemy_RPG_Controller : MonoBehaviour
{
    public float speed = 2f;                  // 落下速度
    private bool isStopped = false;
    private Rigidbody2D rb;

    public GameObject explosionPrefab;         // 爆発Prefab
    public float explosionRadius = 2f;        // 爆発範囲

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isStopped)
        {
            rb.linearVelocity = new Vector2(0, -speed); // 下方向に移動
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    // 弾が当たったら呼ぶ
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
        // 停まっていてLキーが押されたら爆発
        if (isStopped && Input.GetKeyDown(KeyCode.L))
        {
            Explode();
        }
    }

    private void Explode()
    {
        // 爆発Prefab生成（見た目用）
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // 範囲内のWebとEnemyを破壊
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Web") || hit.CompareTag("Enemy"))
            {
                Destroy(hit.gameObject);
                Debug.Log("爆発で破壊: " + hit.name);
            }
        }

        // 自分も消す
        Destroy(gameObject);
    }

    // Sceneビューで爆発範囲を可視化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}