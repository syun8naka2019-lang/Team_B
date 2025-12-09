using UnityEngine;
using UnityEngine.SceneManagement;

public class Changescene : MonoBehaviour
{
    // Inspectorで設定する次のシーンの名前（例: "Stage2Scene"）
    public string sceneName;

    // シーンを読み込み、必要であればスコアをリセットする
    public void Load()
    {
        // 1. ScoreBoardがSingletonとして存在するか確認する
        if (ScoreBoard.Instance != null)
        {
            // ScoreBoardの値を 0 に初期化する
            ScoreBoard.Instance.ResetScore();
            Debug.Log("Changescene: ScoreBoardがリセットされました。次のステージへ移動します。");
        }
        else
        {
            Debug.LogWarning("Changescene: ScoreBoard.Instanceが見つかりません。リセットせずにシーンを移動します。");
        }

        // 2. シーンを読み込む
        SceneManager.LoadScene(sceneName);
    }
}



/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Changescene : MonoBehaviour
{
    public string sceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    //シーンを読み込む
    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}*/