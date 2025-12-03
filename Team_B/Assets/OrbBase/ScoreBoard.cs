using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard Instance;   // ★どこからでも参照できるシングルトン

    [Header("表示するテキスト")]
    public Text scoreText; // Inspector で ScoreTxt を設定

    private int score = 0; // 内部スコア管理用

    public int Score => score;

    private void Awake()
    {
        // シングルトン化（重複防止）
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Text が未設定の場合に警告
        if (scoreText == null)
        {
            Debug.LogError("ScoreBoard : scoreText が設定されていません！");
        }

        UpdateScoreText(); // 初期表示
    }

    // スコア加算
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // UI 更新
    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}
