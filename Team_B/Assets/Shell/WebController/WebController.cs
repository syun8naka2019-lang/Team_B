using UnityEngine;

/// <summary>
/// Web の移動・敵捕捉・強制破壊処理（寿命削除は無し）
/// </summary>
public class WebController : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 10f;

    private Rigidbody2D rb;
    private bool isStopped = false;

    [Header("消滅エフェクト")]
    public GameObject destroyEffectPrefab;
    public float effectLifeTime = 1f;

    private int originalLayer;
    private int inactiveLayer;

    // 破壊済みフラグ（二重破壊防止）
    private bool isDestroyed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        originalLayer = gameObject.layer;
        inactiveLayer = LayerMask.NameToLayer("InactiveWeb");

        if (inactiveLayer == -1)
            Debug.LogWarning("InactiveWeb レイヤーが存在しません。");
    }

    private void OnEnable()
    {
        // WebManager があれば登録（なければ警告）
        if (WebManager.Instance != null)
            WebManager.Instance.RegisterWeb(this);
        else
            Debug.LogWarning("WebController: WebManager.Instance is null. Make sure WebManager exists in the scene.");
    }

    private void OnDisable()
    {
        // 登録解除（安全のため）
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

        // Web を停止（スコアはここで与えない仕様）
        StopWeb();

        if (inactiveLayer != -1)
            gameObject.layer = inactiveLayer;

        // 敵を停止
        EnemyBaseController enemy = obj.GetComponent<EnemyBaseController>();
        if (enemy != null)
            enemy.Stop();

        Debug.Log($"WebController: Captured enemy {obj.name}");
    }

    private void StopWeb()
    {
        isStopped = true;
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }

    /// <summary>
    /// 外部（WebManager など）から呼ぶ強制破壊
    /// </summary>
    public void ForceDestroy()
    {
        if (isDestroyed)
            return; // 二重呼び出しを無視

        isDestroyed = true;

        CreateEffect();

        // 解除（WebManager 側は OnDisable で自動で除去される）
        gameObject.SetActive(false); // 先に無効化して OnDisable を呼ばせるのも安全策

        // Destroy を遅らせたい場合はここでタイマーを使うが即時削除でもOK
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
}
