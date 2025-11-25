using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

<<<<<<< HEAD
/// <summary>
/// 共通の敵コントローラスクリプト
/// - 移動方向はInspectorで自由に設定可能
/// - 弾（Web）が当たったら止まる
/// - Lキーで爆発可能
/// </summary>
public class EnemyBaseController : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 2f;                      // 移動速度
    public Vector2 moveDirection = Vector2.down;  // Inspectorで設定できる移動方向

    private bool isStopped = false;               // 敵が止まっているか
    private Rigidbody2D rb;                       // Rigidbody2Dコンポーネント

    [Header("爆発設定")]
    public GameObject explosionPrefab;            // 爆発Prefab
    public float explosionRadius = 2f;            // 爆発範囲

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // 停止していなければ指定方向に移動
        if (!isStopped)
        {
            rb.linearVelocity = moveDirection.normalized * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
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
            rb.linearVelocity = Vector2.zero;
        }
    }

    void Update()
    {
        // 停止中にLキーで爆発
        if (isStopped && Input.GetKeyDown(KeyCode.L))
        {
            Explode();
        }
    }

    /// <summary>
    /// 爆発処理
    /// 範囲内のWebとEnemyを破壊
    /// 自身も破壊
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
            if (hit.CompareTag("Web") || hit.CompareTag("Enemy"))
            {
                Destroy(hit.gameObject);
                Debug.Log("爆発で破壊: " + hit.name);
            }
        }

        // 自身も破壊
        Destroy(gameObject);
    }

    // Sceneビューで爆発範囲を可視化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void OnBecameInvisible()//�ǂ̃J�����ɂ�f��Ȃ��Ƃ�
    {
        Destroy(gameObject); //�I�u�W�F�N�g�����
    }

}
/*
using System.Collections;
using UnityEngine;

/// <summary>
/// 敵がWebで止まり → 震え → 自動 or 手動で爆発 → 連鎖爆発
/// Spriteだけ揺らすので位置ズレなし
/// </summary>
=======
>>>>>>> 0ef132ce225af06552d0bc802f3ac1f540cd7a66
public class EnemyBaseController : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 2f;
    public Vector2 moveDirection = Vector2.down;

    private bool isStopped = false;
    private Rigidbody2D rb;

    [Header("爆発設定")]
    public GameObject explosionPrefab;
    public float explosionRadius = 2f;

    [Header("オーブ設定")]
    public GameObject redOrbPrefab;
    public GameObject greenOrbPrefab;
    public GameObject blueOrbPrefab;

    public int redRate = 70;
    public int greenRate = 20;
    public int blueRate = 10;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isStopped)
        {
            rb.linearVelocity = moveDirection.normalized * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void Stop()
    {
        if (!isStopped)
        {
            isStopped = true;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void Update()
    {
        if (isStopped && Input.GetKeyDown(KeyCode.L))
        {
            Explode();
        }
    }

    // 爆発処理
    private void Explode()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Web") || hit.CompareTag("Enemy"))
            {
                Destroy(hit.gameObject);
            }
        }

        SpawnOrb();

        Destroy(gameObject);
    }

    // ランダムオーブ生成
    private void SpawnOrb()
    {
        int r = Random.Range(0, 100);

        GameObject orb = null;

        if (r < redRate)
            orb = redOrbPrefab;
        else if (r < redRate + greenRate)
            orb = greenOrbPrefab;
        else
            orb = blueOrbPrefab;

        if (orb != null)
        {
            Instantiate(orb, transform.position + Vector3.up * 1f, Quaternion.identity);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
