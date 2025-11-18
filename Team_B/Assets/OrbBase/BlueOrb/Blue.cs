using UnityEngine;

public class BlueOrb : OrbBase
{
    [Header("スピード上昇量")]
    public float speedUpAmount = 2f;

    [Header("効果時間")]
    public float duration = 5f;

    protected override void ApplyEffect(PlayerStatus player)
    {
        player.AddSpeedTemporary(speedUpAmount, duration);
    }
}
