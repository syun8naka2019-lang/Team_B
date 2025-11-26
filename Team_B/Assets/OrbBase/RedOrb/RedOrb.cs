using UnityEngine;

public class RedOrb : OrbBase
{
    public int scoreValue = 10; // Inspector で設定可能

    protected override void ApplyEffect(PlayerStatus player)
    {
        // シーン内の ScoreBoard を取得してスコア加算
        ScoreBoard sb = FindObjectOfType<ScoreBoard>();
        if (sb != null)
        {
            sb.AddScore(scoreValue);
        }

        Debug.Log("Score Up! +" + scoreValue);
    }
}
