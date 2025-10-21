using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gameOverUI;  // �Q�[���I�[�o�[UI��Canvas�����蓖�Ă�
    private bool isGameOver = false;

    void Start()
    {
        gameOverUI.SetActive(false);
    }

    public void GameOver()
    {
        if (isGameOver) return;  // ��d�Ăяo���h�~
        isGameOver = true;

        // �Q�[���I�[�o�[��ʕ\��
        gameOverUI.SetActive(true);

        // �Q�[�����~�������ꍇ�͎��Ԃ��~�߂�
        Time.timeScale = 0f;
    }

    // ��Ƃ��ăv���C���[��HP������֐�
    public void PlayerTakeDamage(int damage)
    {
        // �v���C���[��HP�Ǘ��͕ʂɂ��邩���ł����A���ɂ����Ŕ���
        int playerHP = 0; // ��

        playerHP -= damage;
        if (playerHP <= 0)
        {
            GameOver();
        }
    }
}