using UnityEngine;
using UnityEngine.UI;

public class ClearScoreViewer : MonoBehaviour
{
    // クリア画面などに配置するText UI。Inspectorで設定します。
    public Text scoreText;

    void Start()
    {
        // ScoreBoardのインスタンスがnullでないか確認する (NullReferenceException回避)
        if (ScoreBoard.Instance != null)
        {
            // ScoreBoard.Instance の Score プロパティからスコアを取得して表示
            // これで最初に発生していたエラー（GameManager.instanceがない）は解消されます。
            scoreText.text = "Score : " + ScoreBoard.Instance.Score.ToString();
        }
        else
        {
            // ScoreBoard が見つからない場合のエラー処理
            scoreText.text = "Score : ERROR";
            Debug.LogError("ClearScoreViewer: ScoreBoard.Instance が見つかりません。ゲームプレイシーンに ScoreBoard オブジェクトを配置してください。");
        }
    }
}