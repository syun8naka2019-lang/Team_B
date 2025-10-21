using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gameOverUI;  // ゲームオーバーUIのCanvasを割り当てる
    private bool isGameOver = false;

    void Start()
    {
        gameOverUI.SetActive(false);
    }

    public void GameOver()
    {
        if (isGameOver) return;  // 二重呼び出し防止
        isGameOver = true;

        // ゲームオーバー画面表示
        gameOverUI.SetActive(true);

        // ゲームを停止したい場合は時間を止める
        Time.timeScale = 0f;
    }

    // 例としてプレイヤーのHPが減る関数
    public void PlayerTakeDamage(int damage)
    {
        // プレイヤーのHP管理は別にあるかもですが、仮にここで判定
        int playerHP = 0; // 仮

        playerHP -= damage;
        if (playerHP <= 0)
        {
            GameOver();
        }
    }
}