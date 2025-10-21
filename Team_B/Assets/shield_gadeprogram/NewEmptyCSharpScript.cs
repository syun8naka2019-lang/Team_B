using UnityEngine;

//プレイヤーのスキルの変数
public class player:MonoBehaviour
{
    public float skillGauge = 0f;           // 現在のスキルゲージ
    public float maxSkillGauge = 100f;      // 最大値

    public void AddSkillGauge(float amount)
    {
        skillGauge += amount;
        skillGauge = Mathf.Clamp(skillGauge, 0f, maxSkillGauge);
        Debug.Log("スキルゲージ: " + skillGauge);
    }

    public bool IsSkillGaugeFull()
    {
        return skillGauge >= maxSkillGauge;
    }
}

