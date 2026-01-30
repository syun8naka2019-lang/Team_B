//using UnityEngine;

//// 音符（銃の弾）を下方向に飛ばし、一定時間追尾した後に直進して消えるクラス
//public class MusicalNoteBullet : MonoBehaviour
//{
//    [Header("Target")]
//    public Transform target;          // 追尾するプレイヤー

//    [Header("Movement")]
//    public float speed = 5f;          // 弾の移動スピード
//    public float turnSpeed = 5f;      // 追尾の強さ

//    [Header("Life")]
//    public float homingTime = 5f;     // 追尾する時間
//    public float lifeTime = 7f;       // ★ 消えるまでの総時間

//    [Header("Visual")]
//    public Sprite bulletSprite;       // 弾の画像

//    private Rigidbody2D rb;
//    private SpriteRenderer sr;

//    private float timer = 0f;         // 経過時間
//    private bool isHoming = true;     // 追尾中かどうか

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        sr = GetComponent<SpriteRenderer>();

//        // 弾の画像を設定
//        if (sr != null && bulletSprite != null)
//        {
//            sr.sprite = bulletSprite;
//        }

//        // 最初は下方向に飛ばす
//        rb.linearVelocity = Vector2.down * speed;
//    }

//    void FixedUpdate()
//    {
//        // 経過時間を更新
//        timer += Time.fixedDeltaTime;

//        // ★ 寿命を超えたら消す
//        if (timer >= lifeTime)
//        {
//            Destroy(gameObject);
//            return;
//        }

//        // ★ 追尾時間を超えたら直進モードへ
//        if (timer >= homingTime)
//        {
//            isHoming = false;
//        }

//        // 追尾中のみ方向補正を行う
//        if (isHoming && target != null)
//        {
//            // プレイヤーへの方向
//            Vector2 toTarget = (target.position - transform.position).normalized;

//            // 現在の進行方向
//            Vector2 currentDir = rb.linearVelocity.normalized;

//            // 少しずつターゲット方向へ向ける
//            Vector2 newDir = Vector2.Lerp(
//                currentDir,
//                toTarget,
//                turnSpeed * Time.fixedDeltaTime
//            );

//            // 方向を更新
//            rb.linearVelocity = newDir.normalized * speed;
//        }
//        // 追尾終了後は velocity を触らない → 直進
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            Destroy(gameObject);
//        }
//    }
//}

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class MusicalNoteBullet : MonoBehaviour
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

        // 🔥 Lキーで爆発（他の敵と同じ仕様）
        if (Input.GetKeyDown(KeyCode.L))
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        if (hasExploded) return;

        // Webなどで停止中
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

            Vector2 newDir = Vector2.Lerp(
                currentDir,
                toTarget,
                turnSpeed * Time.fixedDeltaTime
            );

            rb.linearVelocity = newDir.normalized * speed;
        }
    }

    // =========================
    // 当たり判定
    // =========================

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasExploded) return;

        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
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

    // =========================
    // Webに捕まった
    // =========================

    private void HandleWebCollision(Collider2D webCollider)
    {
        StopSelf();

        WebController web = webCollider.GetComponent<WebController>();
        if (web != null)
            web.StopWeb(); // Webも停止
    }

    private void StopSelf()
    {
        isStopped = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic; // 物理バグ防止
    }

    // =========================
    // 死亡処理（共通ルート）
    // =========================

    public void Die(bool addScore = false)
    {
        if (hasExploded) return;

        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                // 通常敵を巻き込む
                var enemy = hit.GetComponent<EnemyBaseController>();
                if (enemy != null)
                    enemy.Die(true);

                // 音符敵同士も連鎖
                var note = hit.GetComponent<MusicalNoteBullet>();
                if (note != null && note != this)
                    note.Die(false);
            }
            else if (hit.CompareTag("Web"))
            {
                hit.GetComponent<WebController>()?.ForceDestroy();
            }
        }
    }
}
