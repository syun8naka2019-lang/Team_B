using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TextMeshProUGUI scoreText; // UI のスコア表示
    private int score = 0;


    private void Awake()
    {
        // シングルトンの設定
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("ScoreManager Start: scoreText=" + (scoreText == null ? "NULL" : scoreText.name));
        UpdateScoreText();
    }

    // スコアを加算する
    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    // UI テキストを更新
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogError("ScoreText が設定されていません！");
        }
    }
}
