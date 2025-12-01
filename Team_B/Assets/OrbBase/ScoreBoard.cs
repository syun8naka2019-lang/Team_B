using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [Header("表示するテキスト")]
    public Text scoreText; // Inspector で ScoreTxt を設定

    private int score = 0; // 内部スコア管理用

    // 現在スコアを取得するプロパティ（必要に応じて参照）
    public int Score => score;

    // スコア加算メソッド
    public void AddScore(int amount)
    {
        score += amount;      // スコアに加算
        UpdateScoreText();    // UI に反映
    }

    // UI のテキストを更新
    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}
