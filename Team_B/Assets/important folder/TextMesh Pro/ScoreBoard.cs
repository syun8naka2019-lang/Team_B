//using UnityEngine;

//public class ScoreBoard : MonoBehaviour
//{
//    public static ScoreBoard Instance;

//    [Header("スコア設定")]
//    public int maxScore = 999999;   // ★ スコアの天井（Inspectorで変更可）

//    private int score = 0;

//    public int Score => score;

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    /// <summary>
//    /// スコア加算（天井あり）
//    /// </summary>
//    public void AddScore(int amount)
//    {
//        score += amount;

//        // ★ 天井チェック
//        if (score > maxScore)
//            score = maxScore;

//        Debug.Log($"ScoreBoard: Score = {score}");
//    }

//    /// <summary>
//    /// スコアリセット
//    /// </summary>
//    public void ResetScore()
//    {
//        score = 0;
//        Debug.Log("ScoreBoard: スコアをリセットしました");
//    }
//}


using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard Instance;

    // ★ 最大スコア（8桁）
    public const long MAX_SCORE = 99999999;

    // ★ 内部は long のみ
    private long score = 0;

    public long Score => score;

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
    /// スコア加算（絶対にマイナス・オーバーフローしない）
    /// </summary>
    public void AddScore(long amount)
    {
        Debug.Log($"AddScore called: amount={amount}, before={score}");

        // ★ 不正値ガード
        if (amount <= 0) return;

        // ★ すでにカンスト
        if (score >= MAX_SCORE)
        {
            score = MAX_SCORE;
            return;
        }

        // ★ 残り分を計算
        long remaining = MAX_SCORE - score;

        // ★ 足せる分だけ足す
        score += (amount > remaining) ? remaining : amount;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
