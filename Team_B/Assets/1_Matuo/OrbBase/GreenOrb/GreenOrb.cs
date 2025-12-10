using UnityEngine;

/// <summary>
/// 回復オーブ
/// プレイヤーに触れると HP 回復
/// </summary>
public class GreenOrb : MonoBehaviour
{
    public int healAmount = 1; // 回復量

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStatus ps = collision.GetComponent<PlayerStatus>();
            if (ps != null)
            {
                ps.Heal(healAmount); // PlayerStatus 経由で回復
            }

            Destroy(gameObject);
        }
    }
}
