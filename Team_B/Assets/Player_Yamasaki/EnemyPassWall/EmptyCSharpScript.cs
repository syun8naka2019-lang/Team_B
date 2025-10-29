using UnityEngine;

public class EnemyPassWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Enemyタグを持つオブジェクトは通過
        if (other.CompareTag("Enemy"))
        {
            // 何もしない（通り抜けOK）
        }
        else
        {
            // それ以外は止めたい場合 → 当たったオブジェクトを止める or バウンド
            Rigidbody2D rb = other.attachedRigidbody;
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}