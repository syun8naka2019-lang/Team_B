using UnityEngine;

//�v���C���[�̃X�L���̕ϐ�
public class player:MonoBehaviour
{
    public float skillGauge = 0f;           // ���݂̃X�L���Q�[�W
    public float maxSkillGauge = 100f;      // �ő�l

    public void AddSkillGauge(float amount)
    {
        skillGauge += amount;
        skillGauge = Mathf.Clamp(skillGauge, 0f, maxSkillGauge);
        Debug.Log("�X�L���Q�[�W: " + skillGauge);
    }

    public bool IsSkillGaugeFull()
    {
        return skillGauge >= maxSkillGauge;
    }
}

