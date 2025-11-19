using UnityEngine;

// 緑のオーブ：HPを回復する
public class GreenOrb : OrbBase
{
    // 回復量（Inspectorで変更可能）
    public int healAmount = 1;

    // 効果を適用
    protected override void ApplyEffect(PlayerStatus player)
    {
        player.hp += healAmount;
        Debug.Log("HP Heal +" + healAmount);
    }
}
