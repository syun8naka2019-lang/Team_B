using System.Collections;
using UnityEngine;

public class Enemy_Music_Controller : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 2f;                  // 敵の落下速度

    private bool isStopped = false;           // 敵が止まっているかどうか
    private Rigidbody2D rb;                   // Rigidbody2Dコンポーネント

    [Header("爆発設定")]
    public GameObject explosionPrefab;        // 爆発Prefab
    public float explosionRadius = 2f;        // 爆発範囲（半径）

    void Awake()
    {
        // Rigidbody2D取得
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isStopped)
        {
            // 下方向に移動
            rb.velocity = new Vector2(0, -speed);
        }
        else
        {
            // 停止中は速度0
            rb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// 弾が当たったときに呼ぶ
    /// 敵を停止させる
    /// </summary>
    public void Stop()
    {
        if (!isStopped)
        {
            isStopped = true;
            rb.velocity = Vector2.zero;
        }
    }

    void Update()
    {
        // 停止中にLキーが押されたら爆発
        if (isStopped && Input.GetKeyDown(KeyCode.L))
        {
            Explode();
        }
    }

    /// <summary>
    /// 爆発処理
    /// 範囲内の敵や弾を破壊し、自身も破壊する
    /// </summary>
    private void Explode()
    {
        // 見た目用の爆発Prefab生成
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // 爆発範囲内のCollider2Dを取得
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            // WebまたはEnemyタグのオブジェクトを破壊
            if (hit.CompareTag("Web") || hit.CompareTag("Enemy"))
            {
                Destroy(hit.gameObject);
                Debug.Log("爆発で破壊: " + hit.name);
            }
        }

        // 自分も破壊
        Destroy(gameObject);
    }

    // Sceneビューで爆発範囲を可視化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}