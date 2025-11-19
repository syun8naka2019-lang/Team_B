using UnityEngine;

// 赤のオーブ：スコアを増やす
public class RedOrb : OrbBase
{
    // 増えるスコア（Inspectorで変更可能）
    public int scoreValue = 10;

    // 効果を適用する処理
    protected override void ApplyEffect(PlayerStatus player)
    {
        player.score += scoreValue;
        Debug.Log("Score Up! +" + scoreValue);
    }
}
