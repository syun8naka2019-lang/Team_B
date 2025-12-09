using System.Collections;
using UnityEngine;

/// <summary>
/// BigExplosion: 大きな爆発用
/// - 爆風範囲を広くして敵を破壊
/// - スコア加算
/// - 見た目の演出（拡大・フェードアウト）
/// </summary>
public class BigExplosion : MonoBehaviour
{
    [Header("爆発設定")]
    public float lifetime = 0.5f;          // 爆発表示時間
    public float radius = 5.0f;            // 爆風半径を大きく
    public int scorePerEnemy = 50;         // 爆発で加算するスコア

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // 爆風範囲内の敵を取得
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Destroy(hit.gameObject);
                Debug.Log("BigExplosionで敵を破壊: " + hit.name);

                // スコア加算
                PlayerStatus player = FindObjectOfType<PlayerStatus>();
                if (player != null)
                    player.AddScore(scorePerEnemy);
            }
        }

        // 見た目の爆発演出開始
        StartCoroutine(ExplosionEffect());
    }

    IEnumerator ExplosionEffect()
    {
        Vector3 startScale = transform.localScale;
        Color startColor = sr.color;

        float elapsed = 0f;
        while (elapsed < lifetime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / lifetime;

            transform.localScale = startScale * (1 + t * 2.0f); // さらに大きく拡大
            sr.color = new Color(startColor.r, startColor.g, startColor.b, 1 - t);

            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
