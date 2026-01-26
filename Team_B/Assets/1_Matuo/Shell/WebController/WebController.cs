//using UnityEngine;

//public class WebController : MonoBehaviour
//{
//    [Header("移動設定")]
//    public float speed = 10f;
//    private Rigidbody2D rb;
//    private bool isStopped = false;

//    [Header("消滅エフェクト")]
//    public GameObject destroyEffectPrefab;
//    public float effectLifeTime = 1f;

//    private int inactiveLayer;
//    private int webEnemyLayer;

//    private bool isDestroyed = false;

//    // ★ 捕獲した敵
//    private EnemyBaseController capturedEnemy;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();

//        inactiveLayer = LayerMask.NameToLayer("InactiveWeb");
//        webEnemyLayer = LayerMask.NameToLayer("WebEnemy");

//        if (inactiveLayer == -1)
//            Debug.LogWarning("InactiveWeb レイヤーが存在しません。");
//        if (webEnemyLayer == -1)
//            Debug.LogWarning("WebEnemy レイヤーが存在しません。");
//    }

//    private void OnEnable()
//    {
//        if (WebManager.Instance != null)
//            WebManager.Instance.RegisterWeb(this);
//    }

//    private void OnDisable()
//    {
//        if (WebManager.Instance != null)
//            WebManager.Instance.UnregisterWeb(this);
//    }

//    void Update()
//    {
//        if (!isStopped)
//            transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        CheckHitEnemy(other.gameObject);
//    }

//    void OnCollisionEnter2D(Collision2D collision)
//    {
//        CheckHitEnemy(collision.gameObject);
//    }

//    private void CheckHitEnemy(GameObject obj)
//    {
//        if (!obj.CompareTag("Enemy"))
//            return;

//        // ★ 敵を WebEnemy レイヤーに変更
//        obj.layer = webEnemyLayer;

//        // ★ 敵を捕獲
//        capturedEnemy = obj.GetComponent<EnemyBaseController>();
//        if (capturedEnemy != null)
//            capturedEnemy.Stop();

//        // ★ Web を停止 & 無効レイヤーへ
//        StopWeb();
//        gameObject.layer = inactiveLayer;

//        Debug.Log("Enemy captured by Web.");
//    }

//    private void StopWeb()
//    {
//        isStopped = true;
//        if (rb != null)
//            rb.linearVelocity = Vector2.zero;
//    }

//    /// <summary>
//    /// WebManager などによる強制破壊
//    /// </summary>
//    public void ForceDestroy()
//    {
//        if (isDestroyed)
//            return;

//        isDestroyed = true;

//        // ★ 捕獲していた敵を倒す（スコア加算ここで発生）
//        if (capturedEnemy != null)
//        {
//            capturedEnemy.Die();
//            capturedEnemy = null;
//        }

//        CreateEffect();

//        gameObject.SetActive(false);
//        Destroy(gameObject);
//    }

//    private void CreateEffect()
//    {
//        if (destroyEffectPrefab != null)
//        {
//            GameObject effect = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
//            Destroy(effect, effectLifeTime);
//        }
//    }

//    void OnBecameInvisible()
//    {
//        Destroy(gameObject);
//    }
//}


using UnityEngine;

public class WebController : MonoBehaviour
{
    public float speed = 10f;
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
        TryCaptureEnemy(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryCaptureEnemy(collision.gameObject);
    }

    private void TryCaptureEnemy(GameObject obj)
    {
        if (capturedEnemy != null) return;
        if (!obj.CompareTag("Enemy")) return;

        var enemy = obj.GetComponent<EnemyBaseController>();
        if (enemy == null) return;

        capturedEnemy = enemy;
        enemy.Stop();

        obj.layer = webEnemyLayer;

        StopWeb();
        gameObject.layer = inactiveLayer;

        WebManager.Instance?.RegisterCapturedEnemy(enemy);
    }

    private void StopWeb()
    {
        isStopped = true;
        if (rb != null) rb.linearVelocity = Vector2.zero;
    }

    // L爆発用
    public void ForceDestroy()
    {
        if (isDestroyed) return;
        isDestroyed = true;

        CreateEffect();
        Destroy(gameObject);
    }

    // 通常消滅時（巻き込み爆発など）
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
