using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Slime : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Movement")]
    public float speed = 5f;
    public float turnSpeed = 5f;
    public float homingTime = 5f;

    [Header("Explosion")]
    public GameObject explosionPrefab;
    public float explosionRadius = 2f;

    [Header("Visual")]
    public Sprite bulletSprite;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private float timer = 0f;
    private bool isHoming = true;
    private bool isStopped = false;
    private bool hasExploded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (bulletSprite != null)
            sr.sprite = bulletSprite;

        rb.linearVelocity = Vector2.down * speed;
    }

    void Update()
    {
        if (hasExploded) return;

        if (Input.GetKeyDown(KeyCode.L))
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        if (hasExploded) return;

        if (isStopped)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            return;
        }

        timer += Time.fixedDeltaTime;

        if (timer >= homingTime)
            isHoming = false;

        if (isHoming && target != null)
        {
            Vector2 toTarget = (target.position - transform.position).normalized;
            Vector2 currentDir = rb.linearVelocity.normalized;

            Vector2 newDir = Vector2.Lerp(currentDir, toTarget, turnSpeed * Time.fixedDeltaTime);
            rb.linearVelocity = newDir.normalized * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasExploded) return;

        if (other.CompareTag("Player"))
        {
            Die();
        }
        else if (other.CompareTag("Web"))
        {
            HandleWebCollision(other);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasExploded) return;

        if (collision.gameObject.CompareTag("Web"))
        {
            HandleWebCollision(collision.collider);
        }
    }

    private void HandleWebCollision(Collider2D webCollider)
    {
        StopSelf();

        WebController web = webCollider.GetComponent<WebController>();
        if (web != null)
            web.StopWeb();
    }

    private void StopSelf()
    {
        isStopped = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // ★ 自分が死ぬ時（スコアは入らない）
    public void Die()
    {
        if (hasExploded) return;

        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        hasExploded = true;

        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (var hit in hits)
        {
            // ✅ 通常敵を倒す → ここでスコア加算
            var enemy = hit.GetComponent<EnemyBaseController>();
            if (enemy != null)
            {
                enemy.Die(true); // ← スコア加算フラグ復活！！
                continue;
            }

            // Web破壊
            if (hit.CompareTag("Web"))
            {
                hit.GetComponent<WebController>()?.ForceDestroy();
            }
        }
    }
}
