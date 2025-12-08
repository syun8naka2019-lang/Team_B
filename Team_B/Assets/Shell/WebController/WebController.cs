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

//    private int originalLayer;
//    private int inactiveLayer;
//    private int webEnemyLayer;

//    // 破壊済みフラグ（二重破壊防止）
//    private bool isDestroyed = false;


//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();

//        // Web 自身の元レイヤー
//        originalLayer = gameObject.layer;

//        // Web 捕獲後の無効レイヤー
//        inactiveLayer = LayerMask.NameToLayer("InactiveWeb");

//        // 敵が捕まった時に変更するレイヤー
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
//        else
//            Debug.LogWarning("WebController: WebManager.Instance is null.");
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


//    /// <summary>
//    /// 敵にヒットしたかどうかチェック
//    /// </summary>
//    private void CheckHitEnemy(GameObject obj)
//    {
//        // ★ 敵以外には反応しない（EnemyTag が必要）
//        if (!obj.CompareTag("Enemy"))
//            return;

//        // ★ 敵を WebEnemy レイヤーへ変更
//        if (webEnemyLayer != -1)
//            obj.layer = webEnemyLayer;

//        // ★ Web を停止
//        StopWeb();

//        // ★ Web を無効レイヤーへ変更
//        if (inactiveLayer != -1)
//            gameObject.layer = inactiveLayer;

//        // ★ 敵を停止
//        EnemyBaseController enemy = obj.GetComponent<EnemyBaseController>();
//        if (enemy != null)
//            enemy.Stop();

//        Debug.Log($"WebController: Captured enemy {obj.name}, set to WebEnemy layer.");
//    }


//    private void StopWeb()
//    {
//        isStopped = true;

//        if (rb != null)
//            rb.linearVelocity = Vector2.zero;
//    }


//    /// <summary>
//    /// 外部（WebManager など）から呼ぶ強制破壊
//    /// </summary>
//    public void ForceDestroy()
//    {
//        if (isDestroyed)
//            return;

//        isDestroyed = true;

//        CreateEffect();

//        gameObject.SetActive(false);
//        Destroy(gameObject);
//    }


//    private void CreateEffect()
//    {
//        if (destroyEffectPrefab != null)
//        {
//            GameObject effect = Instantiate(
//                destroyEffectPrefab,
//                transform.position,
//                Quaternion.identity
//            );

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
    [Header("移動設定")]
    public float speed = 10f;
    private Rigidbody2D rb;
    private bool isStopped = false;

    [Header("消滅エフェクト")]
    public GameObject destroyEffectPrefab;
    public float effectLifeTime = 1f;

    private int inactiveLayer;
    private int webEnemyLayer;

    private bool isDestroyed = false;

    // ★ 捕獲した敵
    private EnemyBaseController capturedEnemy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        inactiveLayer = LayerMask.NameToLayer("InactiveWeb");
        webEnemyLayer = LayerMask.NameToLayer("WebEnemy");

        if (inactiveLayer == -1)
            Debug.LogWarning("InactiveWeb レイヤーが存在しません。");
        if (webEnemyLayer == -1)
            Debug.LogWarning("WebEnemy レイヤーが存在しません。");
    }

    private void OnEnable()
    {
        if (WebManager.Instance != null)
            WebManager.Instance.RegisterWeb(this);
    }

    private void OnDisable()
    {
        if (WebManager.Instance != null)
            WebManager.Instance.UnregisterWeb(this);
    }

    void Update()
    {
        if (!isStopped)
            transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CheckHitEnemy(other.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CheckHitEnemy(collision.gameObject);
    }

    private void CheckHitEnemy(GameObject obj)
    {
        if (!obj.CompareTag("Enemy"))
            return;

        // ★ 敵を WebEnemy レイヤーに変更
        obj.layer = webEnemyLayer;

        // ★ 敵を捕獲
        capturedEnemy = obj.GetComponent<EnemyBaseController>();
        if (capturedEnemy != null)
            capturedEnemy.Stop();

        // ★ Web を停止 & 無効レイヤーへ
        StopWeb();
        gameObject.layer = inactiveLayer;

        Debug.Log("Enemy captured by Web.");
    }

    private void StopWeb()
    {
        isStopped = true;
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }

    /// <summary>
    /// WebManager などによる強制破壊
    /// </summary>
    public void ForceDestroy()
    {
        if (isDestroyed)
            return;

        isDestroyed = true;

        // ★ 捕獲していた敵を倒す（スコア加算ここで発生）
        if (capturedEnemy != null)
        {
            capturedEnemy.Die();
            capturedEnemy = null;
        }

        CreateEffect();

        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void CreateEffect()
    {
        if (destroyEffectPrefab != null)
        {
            GameObject effect = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, effectLifeTime);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
