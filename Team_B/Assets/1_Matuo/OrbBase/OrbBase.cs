using UnityEngine;

public abstract class OrbBase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーと接触したら
        if (other.CompareTag("Player"))
        {
            PlayerStatus player = other.GetComponent<PlayerStatus>();
            if (player != null)
            {
                ApplyEffect(player); // オーブの効果を発動
            }

            Destroy(gameObject); // オーブは消える
        }
    }

    // 個別オーブごとの効果をここで実装
    protected abstract void ApplyEffect(PlayerStatus player);
}
