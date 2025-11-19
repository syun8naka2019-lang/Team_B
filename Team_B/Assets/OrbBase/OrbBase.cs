using UnityEngine;

public abstract class OrbBase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player に触れたら効果を発動
        if (other.CompareTag("Player"))
        {
            PlayerStatus player = other.GetComponent<PlayerStatus>();
            if (player != null)
            {
                ApplyEffect(player);
            }

            // 取得したらオーブを消す
            Destroy(gameObject);
        }
    }

    // 個別オーブが実装する効果
    protected abstract void ApplyEffect(PlayerStatus player);
}
