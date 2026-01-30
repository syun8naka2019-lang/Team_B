//using UnityEngine;

//public class WebController : MonoBehaviour
//{
//    public float speed = 10f;
//    public GameObject destroyEffectPrefab;
//    public float effectLifeTime = 1f;

//    private Rigidbody2D rb;
//    private bool isStopped = false;
//    private bool isDestroyed = false;

//    private int inactiveLayer;
//    private int webEnemyLayer;

//    private EnemyBaseController capturedEnemy;


//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        inactiveLayer = LayerMask.NameToLayer("InactiveWeb");
//        webEnemyLayer = LayerMask.NameToLayer("WebEnemy");
//    }

//    private void OnEnable()
//    {
//        WebManager.Instance?.RegisterWeb(this);
//    }

//    private void OnDisable()
//    {
//        WebManager.Instance?.UnregisterWeb(this);
//    }

//    private void Update()
//    {
//        if (!isStopped)
//            transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        TryCaptureEnemy(other.gameObject);
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        TryCaptureEnemy(collision.gameObject);
//    }

//    private void TryCaptureEnemy(GameObject obj)
//    {
//        if (capturedEnemy != null) return;
//        if (!obj.CompareTag("Enemy")) return;

//        var enemy = obj.GetComponent<EnemyBaseController>();
//        if (enemy == null) return;

//        capturedEnemy = enemy;
//        enemy.Stop();

//        obj.layer = webEnemyLayer;

//        StopWeb();
//        gameObject.layer = inactiveLayer;

//        WebManager.Instance?.RegisterCapturedEnemy(enemy);
//    }

//    private void StopWeb()
//    {
//        isStopped = true;
//        if (rb != null) rb.linearVelocity = Vector2.zero;
//    }

//    // L爆発用
//    public void ForceDestroy()
//    {
//        if (isDestroyed) return;
//        isDestroyed = true;

//        CreateEffect();
//        Destroy(gameObject);
//    }

//    // 通常消滅時（巻き込み爆発など）
//    private void OnDestroy()
//    {
//        if (isDestroyed) return;

//        if (capturedEnemy != null)
//        {
//            capturedEnemy.Die(true);
//            capturedEnemy = null;
//        }
//    }

//    private void CreateEffect()
//    {
//        if (destroyEffectPrefab != null)
//        {
//            var effect = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
//            Destroy(effect, effectLifeTime);
//        }
//    }

//    private void OnBecameInvisible()
//    {
//        Destroy(gameObject);
//    }
//}


using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class WebController : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 10f;

    [Header("消滅エフェクト")]
    public GameObject destroyEffectPrefab;
    public float effectLifeTime = 1f;

    private Rigidbody2D rb;
    private bool isStopped = false;
    private bool isDestroyed = false;

    private int inactiveLayer;
    private int webEnemyLayer;

    private EnemyBaseController capturedEnemy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        inactiveLayer = LayerMask.NameToLayer("InactiveWeb");
        webEnemyLayer = LayerMask.NameToLayer("WebEnemy");
    }

    private void OnEnable()
    {
        WebManager.Instance?.RegisterWeb(this);
    }

    private void OnDisable()
    {
        WebManager.Instance?.UnregisterWeb(this);
    }

    private void Update()
    {
        if (!isStopped)
            transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollision(other);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.collider);
    }

    private void HandleCollision(Collider2D other)
    {
        if (isDestroyed) return;

        // 🎯 通常の敵を捕獲
        if (capturedEnemy == null && other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyBaseController>();
            if (enemy != null)
            {
                capturedEnemy = enemy;
                enemy.Stop();
                other.gameObject.layer = webEnemyLayer;

                StopWeb();
                gameObject.layer = inactiveLayer;

                WebManager.Instance?.RegisterCapturedEnemy(enemy);
                return;
            }
        }

        // 🎵 音符敵（MusicalNoteBullet）と接触した場合も停止
        var noteEnemy = other.GetComponent<MusicalNoteBullet>();
        if (noteEnemy != null)
        {
            StopWeb();
            return;
        }
    }

    // 🎯 外部からも呼ばれる停止処理（重要）
    public void StopWeb()
    {
        if (isStopped) return;

        isStopped = true;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic; // 完全固定
        }
    }

    // 💥 爆発などで強制破壊
    public void ForceDestroy()
    {
        if (isDestroyed) return;
        isDestroyed = true;

        CreateEffect();
        Destroy(gameObject);
    }

    // 🔥 通常破壊時（爆発巻き込みなど）
    private void OnDestroy()
    {
        if (isDestroyed) return;

        if (capturedEnemy != null)
        {
            capturedEnemy.Die(true);
            capturedEnemy = null;
        }
    }

    private void CreateEffect()
    {
        if (destroyEffectPrefab != null)
        {
            var effect = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, effectLifeTime);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
