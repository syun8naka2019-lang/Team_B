using UnityEngine;

// 音符（銃の弾）を下方向に飛ばし、一定時間追尾した後に直進して消えるクラス
public class Slime : MonoBehaviour
{
    [Header("Target")]
    public Transform target;          // 追尾するプレイヤー

    [Header("Movement")]
    public float speed = 5f;          // 弾の移動スピード
    public float turnSpeed = 5f;      // 追尾の強さ

    [Header("Life")]
    public float homingTime = 5f;     // 追尾する時間
    public float lifeTime = 7f;       // ★ 消えるまでの総時間

    [Header("Visual")]
    public Sprite bulletSprite;       // 弾の画像

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private float timer = 0f;         // 経過時間
    private bool isHoming = true;     // 追尾中かどうか

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // 弾の画像を設定
        if (sr != null && bulletSprite != null)
        {
            sr.sprite = bulletSprite;
        }

        // 最初は下方向に飛ばす
        rb.linearVelocity = Vector2.down * speed;
    }

    void FixedUpdate()
    {
        // 経過時間を更新
        timer += Time.fixedDeltaTime;

        // ★ 寿命を超えたら消す
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
            return;
        }

        // ★ 追尾時間を超えたら直進モードへ
        if (timer >= homingTime)
        {
            isHoming = false;
        }

        // 追尾中のみ方向補正を行う
        if (isHoming && target != null)
        {
            // プレイヤーへの方向
            Vector2 toTarget = (target.position - transform.position).normalized;

            // 現在の進行方向
            Vector2 currentDir = rb.linearVelocity.normalized;

            // 少しずつターゲット方向へ向ける
            Vector2 newDir = Vector2.Lerp(
                currentDir,
                toTarget,
                turnSpeed * Time.fixedDeltaTime
            );

            // 方向を更新
            rb.linearVelocity = newDir.normalized * speed;
        }
        // 追尾終了後は velocity を触らない → 直進
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
