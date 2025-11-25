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


using System.Collections.Generic;
using UnityEngine;
using System.Collections;
/// <summary>
/// 弾（Web）のコントローラ
/// - 上方向（または自由方向）に移動
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

    [Header("弾の寿命")]
    public float lifeTime = 3f;

    [Header("消滅時エフェクト")]
    public GameObject destroyEffectPrefab;

    [Header("エフェクトの寿命")]
    public float effectLifeTime = 0.5f;
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
    private void CheckHitEnemy(GameObject obj)
    {
        if (isStopped) return; // 弾が止まっていれば無視

        if (obj.CompareTag("Enemy"))
        {
            if (!hasHitEnemy)
            {
                hasHitEnemy = true;   // 1回ヒット済みに設定
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
            rb.linearVelocity = Vector2.zero;
        }
    }


    void Start()
    {
        // 弾が一定時間後に消える処理をコルーチンで実行
        StartCoroutine(DestroyBulletAfterTime());
    }

    IEnumerator DestroyBulletAfterTime()
    {
        // 弾の寿命待機
        yield return new WaitForSeconds(lifeTime);

        // エフェクト生成
        if (destroyEffectPrefab != null)
        {
            GameObject effect = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);

            // エフェクトも一定時間後に消す
            Destroy(effect, effectLifeTime);
        }

        // 弾を削除
        Destroy(gameObject);
    }
    private void OnBecameInvisible()//
    {
        Destroy(gameObject); //
    }
  
      
}