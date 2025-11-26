using System.Collections;
using UnityEngine;

public class WebController : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 10f;

    private Rigidbody2D rb;
    private bool isStopped = false;
    private bool hasCapturedEnemy = false;

    [Header("Webの寿命")]
    public float lifeTime = 3f;

    [Header("消滅エフェクト")]
    public GameObject destroyEffectPrefab;
    public float effectLifeTime = 0.5f;

    private int originalLayer;
    private int inactiveLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        originalLayer = gameObject.layer;
        inactiveLayer = LayerMask.NameToLayer("InactiveWeb");

        if (inactiveLayer == -1)
        {
            Debug.LogWarning("InactiveWeb レイヤーが存在しません。作成してください。");
        }
    }

    void Start()
    {
        // 寿命タイマー開始
        StartCoroutine(DestroyAfterTime());
    }

    void Update()
    {
        if (!isStopped)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
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
        if (hasCapturedEnemy) return;

        if (obj.CompareTag("Enemy"))
        {
            hasCapturedEnemy = true;
            Stop();

            if (inactiveLayer != -1)
                gameObject.layer = inactiveLayer;

            EnemyBaseController enemy = obj.GetComponent<EnemyBaseController>();
            if (enemy != null)
                enemy.Stop();

            Debug.Log($"Web が敵 {obj.name} を捕まえました");
        }
    }

    public void Stop()
    {
        isStopped = true;

        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        CreateEffect();
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        CreateEffect();
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

    public void Release()
    {
        hasCapturedEnemy = false;
        isStopped = false;
        gameObject.layer = originalLayer;
    }
}
