using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard Instance;

    [Header("スコア設定")]
    public int maxScore = 999999;   // ★ スコアの天井（Inspectorで変更可）

    private int score = 0;

    public int Score => score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// スコア加算（天井あり）
    /// </summary>
    public void AddScore(int amount)
    {
        score += amount;

        // ★ 天井チェック
        if (score > maxScore)
            score = maxScore;

        Debug.Log($"ScoreBoard: Score = {score}");
    }

    /// <summary>
    /// スコアリセット
    /// </summary>
    public void ResetScore()
    {
        score = 0;
        Debug.Log("ScoreBoard: スコアをリセットしました");
    }
}
