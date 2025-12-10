using UnityEngine;

public class NabeCon : MonoBehaviour
{
    [Header("弾の移動速度")]
    public float speed = 10f;

    [Header("敵 1 体ごとのスコア")]
    public int scorePerEnemy = 10;

    void Update()
    {
        // 弾を常に上へ移動（貫通）
        transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        HandleEnemyHit(collision.gameObject);
    }

    /// <summary>
    /// 敵ヒット時の処理
    /// </summary>
    private void HandleEnemyHit(GameObject enemy)
    {
        // ▼ スコア加算（ScoreBoard に統一）
        if (ScoreBoard.Instance != null)
            ScoreBoard.Instance.AddScore(scorePerEnemy);

        // ▼ EnemyBaseController を持っていれば Die() を呼ぶ
        EnemyBaseController enemyController = enemy.GetComponent<EnemyBaseController>();
        if (enemyController != null)
        {
            enemyController.Die();
            return;
        }

        // ▼ fallback として Destroy
        Destroy(enemy);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
