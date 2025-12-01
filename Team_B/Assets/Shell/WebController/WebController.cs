<<<<<<< HEAD

/*using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾（Web）のコントローラ
/// - 上方向に移動
/// - 1回ヒットした敵で止まる
/// - 止まった弾は他の敵に影響しない
/// </summary>
public class WebController : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 10f;              // 弾の速度

    private Rigidbody2D rb;                // Rigidbody2D
    private bool isStopped = false;        // 弾が止まったか
    private bool hasHitEnemy = false;      // 1回ヒット済みか

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 停止していなければ上方向に移動
        if (!isStopped)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CheckHitEnemy(collision.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CheckHitEnemy(other.gameObject);
    }

    /// <summary>
    /// 敵や壁との衝突処理
    /// </summary>
    /// <param name="obj">衝突したオブジェクト</param>
    private void CheckHitEnemy(GameObject obj)
    {
        if (isStopped) return; // 弾が止まっている場合は何もしない

        if (obj.CompareTag("Enemy"))
        {
            if (!hasHitEnemy)
            {
                hasHitEnemy = true;   // 1回ヒット済みに設定

                // 弾を止める
                Stop();

                // 敵を止める
                EnemyBaseController enemy = obj.GetComponent<EnemyBaseController>();
                if (enemy != null)
                {
                    enemy.Stop();
                }

                Debug.Log("敵ヒット: " + obj.name);
            }
        }      
    }

    /// <summary>
    /// 弾を停止させる
    /// </summary>
    public void Stop()
    {
        isStopped = true;
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}*/

/*
using System.Collections.Generic;
using UnityEngine;

=======
>>>>>>> 3f693ca58de3b5d593ac4d6ce3cfa3cf3a5ccc49
using System.Collections;
using UnityEngine;

public class WebController : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 10f;

<<<<<<< HEAD

    private Rigidbody2D rb;                // Rigidbody2D
    private bool isStopped = false;        // 弾が止まったか
    private bool hasHitEnemy = false;      // 1回ヒット済みか
 

    private Rigidbody2D rb;
    private bool isStopped = false;
    private bool hasCapturedEnemy = false;

=======
    private Rigidbody2D rb;
    private bool isStopped = false;
    private bool hasCapturedEnemy = false;
>>>>>>> 3f693ca58de3b5d593ac4d6ce3cfa3cf3a5ccc49

    [Header("Webの寿命")]
    public float lifeTime = 3f;

    [Header("消滅エフェクト")]
    public GameObject destroyEffectPrefab;
    public float effectLifeTime = 0.5f;

    [Header("敵1体あたりのスコア")]
    public int scorePerEnemy = 50;

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

            // Webを止める
            StopWeb();

            // 衝突判定用のレイヤー変更
            if (inactiveLayer != -1)
                gameObject.layer = inactiveLayer;

            // 敵を停止させる
            EnemyBaseController enemy = obj.GetComponent<EnemyBaseController>();
            if (enemy != null)
                enemy.Stop();

            // スコア加算
            PlayerStatus player = FindObjectOfType<PlayerStatus>();
            if (player != null)
            {
                player.AddScore(scorePerEnemy);
                Debug.Log($"スコア +{scorePerEnemy}");
            }

            Debug.Log($"Web が敵 {obj.name} を捕まえました");
        }
    }

    private void StopWeb()
    {
        isStopped = true;
        if (rb != null)
            rb.velocity = Vector2.zero;
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
<<<<<<< HEAD
<<<<<<< HEAD
  
      
}
=======
}*/

using UnityEngine;

/// <summary>
/// 弾（Web）のコントローラ
/// - 上方向に移動
/// - 敵に当たったら止まる
/// - 一度敵を捕まえたら他の敵とは反応しない
/// - Colliderは残したまま、Layerを変更して制御
/// </summary>
public class WebController : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 10f;


    private Rigidbody2D rb;                // Rigidbody2D
    private bool isStopped = false;        // 弾が止まったか
    private bool hasHitEnemy = false;      // 1回ヒット済みか
    private bool hasCapturedEnemy = false;


    [Header("Webの寿命")]
    public float lifeTime = 3f;

    [Header("消滅エフェクト")]
    public GameObject destroyEffectPrefab;
    public float effectLifeTime = 0.5f;

    private int originalLayer;
    private int inactiveLayer;
    void Start()
    {
        // 寿命タイマー開始
     
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalLayer = gameObject.layer;
        inactiveLayer = LayerMask.NameToLayer("InactiveWeb"); // 新しいレイヤーを作っておく
    }

    void Update()
    {
        // 停止していなければ上方向に移動
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
        // すでに敵を捕まえていたら無視
        if (hasCapturedEnemy)
            return;

        if (obj.CompareTag("Enemy"))
        {
            hasCapturedEnemy = true; // 捕まえた
            Stop();

            // WebのLayerを切り替える（敵とはもう衝突しない）
            if (inactiveLayer != -1)
                gameObject.layer = inactiveLayer;

            // 敵の動きを止める
            EnemyBaseController enemy = obj.GetComponent<EnemyBaseController>();
            if (enemy != null)
                enemy.Stop();

            Debug.Log($"Web {gameObject.name} が敵 {obj.name} を捕まえました（Layer変更で他の敵を無視）");
        }
    }

    /// <summary>
    /// Webを停止
    /// </summary>
    public void Stop()
    {
        isStopped = true;
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }

    /// <summary>
    /// 再利用したい時（敵が消えたなど）にLayerを戻す
    /// </summary>
    public void Release()
    {
        hasCapturedEnemy = false;
        isStopped = false;
        gameObject.layer = originalLayer;
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
=======
>>>>>>> 3f693ca58de3b5d593ac4d6ce3cfa3cf3a5ccc49
}
