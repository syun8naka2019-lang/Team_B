//using UnityEngine;

//public class EnemyBaseController : MonoBehaviour
//{
//    [Header("移動設定")]
//    public float speed = 2f;
//    public Vector2 moveDirection = Vector2.down;

//    private bool isStopped = false;
//    private Rigidbody2D rb;

//    [Header("爆発設定")]
//    public GameObject explosionPrefab;
//    public float explosionRadius = 2f;

//    [Header("オーブ設定")]
//    public GameObject redOrbPrefab;
//    public GameObject greenOrbPrefab;
//    public GameObject blueOrbPrefab;

//    public int redRate = 70;
//    public int greenRate = 20;
//    public int blueRate = 10;

//    void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }

//    void FixedUpdate()
//    {
//        if (!isStopped)
//            rb.linearVelocity = moveDirection.normalized * speed;
//        else
//            rb.linearVelocity = Vector2.zero;
//    }

//    public void Stop()
//    {
//        isStopped = true;
//    }

//    void Update()
//    {
//        if (isStopped && Input.GetKeyDown(KeyCode.L))
//            Explode();
//    }

//    /// <summary>
//    /// 敵死亡（ここで初めてスコア加算）
//    /// </summary>
//    public void Die()
//    {
//        if (ScoreBoard.Instance != null)
//            ScoreBoard.Instance.AddScore(50);

//        SpawnOrb();

//        if (explosionPrefab != null)
//            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

//        Destroy(gameObject);
//    }

//    private void Explode()
//    {
//        if (explosionPrefab != null)
//            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

//        // 範囲 damage
//        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
//        foreach (Collider2D hit in hits)
//        {
//            if (hit.CompareTag("Enemy"))
//            {
//                EnemyBaseController e = hit.GetComponent<EnemyBaseController>();
//                if (e != null) e.Die();
//            }
//            else if (hit.CompareTag("Web"))
//            {
//                hit.GetComponent<WebController>()?.ForceDestroy();
//            }
//        }

//        Destroy(gameObject);
//    }

//    private void SpawnOrb()
//    {
//        int r = Random.Range(0, 100);
//        GameObject orb = null;

//        if (r < redRate) orb = redOrbPrefab;
//        else if (r < redRate + greenRate) orb = greenOrbPrefab;
//        else orb = blueOrbPrefab;

//        if (orb != null)
//            Instantiate(orb, transform.position + Vector3.up * 1f, Quaternion.identity);
//    }
//}
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

    /// <summary>
    /// 敵死亡（スコア加算）
    /// </summary>
    public void Die()
    {
        if (ScoreBoard.Instance != null)
            ScoreBoard.Instance.AddScore(50);

        SpawnOrb();

        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
        private void Explode()
    {
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // 範囲 damage
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                EnemyBaseController e = hit.GetComponent<EnemyBaseController>();
                if (e != null) e.Die();
            }
            else if (hit.CompareTag("Web"))
            {
                hit.GetComponent<WebController>()?.ForceDestroy();
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
}
