using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    // ★ Singleton Instanceの定義
    // この static 変数が、シーンをまたいで唯一のインスタンスを指し続けます。
    public static ScoreBoard Instance;

    private int score = 0; // 内部スコア管理

    // 外部から現在のスコアを読み取るためのプロパティ（読み取り専用）
    public int Score => score;

    private void Awake()
    {
        // ▼ シングルトン設定
        if (Instance == null)
        {
            Instance = this;
            // シーンをまたいでスコア値を保持するために必須の処理
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 既にインスタンスが存在する場合は、新しいものを破棄して重複を防ぐ
            Destroy(gameObject);
        }
    }

    // ▼ 他スクリプトから呼ばれるスコア加算
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("ScoreBoard: Score added. Current Score: " + score);
    }

    // ▼ ステージクリア時などにスコアをリセットするためのメソッド
    // Changescene.cs から呼ばれることを想定しています。
    public void ResetScore()
    {
        score = 0;
        Debug.Log("ScoreBoard: スコアが初期化されました (Score = 0)。");
    }
}

/*using UnityEngine;
public class ScoreBoard : MonoBehaviour
{
    // ★ Singleton Instanceの定義
    // この static 変数が、シーンをまたいで唯一のインスタンスを指し続けます。
    public static ScoreBoard Instance;

    private int score = 0; // 内部スコア管理

    // 外部から現在のスコアを読み取るためのプロパティ（読み取り専用）
    public int Score => score;

    private void Awake()
    {
        // ▼ シングルトン設定
        if (Instance == null)
        {
            Instance = this;
            // シーンをまたいでスコア値を保持するために必須の処理
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 既にインスタンスが存在する場合は、新しいものを破棄して重複を防ぐ
            Destroy(gameObject);
        }
    }

    // ▼ 他スクリプトから呼ばれるスコア加算
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("ScoreBoard: Score added. Current Score: " + score);
    }

    // ▼ ステージクリア時などにスコアをリセットするためのメソッド
    // Changescene.cs から呼ばれることを想定しています。
    public void ResetScore()
    {
        score = 0;
        Debug.Log("ScoreBoard: スコアが初期化されました (Score = 0)。");
    }
}*/