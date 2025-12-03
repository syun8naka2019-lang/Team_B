using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard Instance;   // ★スコアボードの唯一のインスタンス

    [Header("表示するテキスト")]
    public Text scoreText; // Inspector で ScoreTxt を設定

    private int score = 0; // 内部スコア管理

    public int Score => score;

    private void Awake()
    {
        // ▼ シングルトン設定
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // ▼ Text が未設定の場合のエラー
        if (scoreText == null)
        {
            Debug.LogError("ScoreBoard : scoreText が設定されていません！");
        }

        // ▼ 初期表示
        UpdateScoreText();
    }

    // ▼ 他スクリプトから呼ばれるスコア加算
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // ▼ 表示更新
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
