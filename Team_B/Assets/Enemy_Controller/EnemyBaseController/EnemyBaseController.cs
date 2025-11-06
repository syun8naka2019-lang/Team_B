using System.Collections;
using UnityEngine;

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
            rb.velocity = moveDirection.normalized * speed;
        }
        else
        {
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
public class EnemyBaseController : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 2f;
    public Vector2 moveDirection = Vector2.down;

    [Header("爆発設定")]
    public GameObject explosionPrefab;
    public float explosionRadius = 2f;
    public float chainDelay = 0.2f;
    public float timeToExplode = 3f;

    [Header("演出設定")]
    public float minShake = 0.02f;
    public float maxShake = 0.06f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Transform spriteTransform;
    private Vector3 spriteOriginalLocalPos;

    private bool isStopped = false;
    private bool isExploded = false;
    private float elapsed = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        spriteTransform = sr != null ? sr.transform : transform;
        spriteOriginalLocalPos = spriteTransform.localPosition;
    }

    void FixedUpdate()
    {
        if (!isStopped)
        {
            rb.velocity = moveDirection.normalized * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void Update()
    {
        if (isStopped && !isExploded)
        {
            elapsed += Time.deltaTime;

            // 手動爆発
            if (Input.GetKeyDown(KeyCode.L))
            {
                ResetShakePosition(); // 爆発前に位置を戻す
                Explode();
                return;
            }

            // 自動爆発
            if (elapsed >= timeToExplode)
            {
                ResetShakePosition();
                Explode();
                return;
            }

            // 爆発前の震え演出
            ShakeAndColorChange(elapsed / timeToExplode);
        }
    }

    public void Stop()
    {
        if (isStopped) return;
        isStopped = true;
        rb.velocity = Vector2.zero;
        elapsed = 0f;
        Debug.Log($"{name} が停止 → 震え開始");
    }

    private void ShakeAndColorChange(float t)
    {
        // Spriteの位置を毎フレーム「元位置＋揺れ」にする
        float intensity = Mathf.Lerp(minShake, maxShake, t);
        spriteTransform.localPosition = spriteOriginalLocalPos + (Vector3)Random.insideUnitCircle * intensity;

        // 赤く変化
        if (sr != null)
        {
            sr.color = Color.Lerp(Color.white, Color.red, t);
        }
    }

    /// <summary>
    /// 爆発前にSpriteの揺れをリセット
    /// </summary>
    private void ResetShakePosition()
    {
        if (spriteTransform != null)
            spriteTransform.localPosition = spriteOriginalLocalPos;
    }

    private void Explode()
    {
        if (isExploded) return;
        isExploded = true;

        // 爆発エフェクト生成
        if (explosionPrefab)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // 爆発範囲の敵を検出
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Web"))
            {
                Destroy(hit.gameObject);
            }

            if (hit.CompareTag("Enemy"))
            {
                EnemyBaseController other = hit.GetComponent<EnemyBaseController>();
                if (other != null && !other.isExploded)
                {
                    Vector2 dirToOther = (other.transform.position - transform.position).normalized;
                    float dot = Vector2.Dot(moveDirection.normalized, dirToOther);

                    // 前方120度以内にいる敵のみ連鎖
                    if (dot > 0.5f)
                    {
                        StartCoroutine(DelayedChainExplosion(other));
                    }
                }
            }
        }

        Destroy(gameObject, 0.05f);
    }

    private IEnumerator DelayedChainExplosion(EnemyBaseController target)
    {
        yield return new WaitForSeconds(chainDelay);
        target.Explode();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(moveDirection.normalized * explosionRadius));
    }
}
*/