//using System.Collections.Generic;
//using UnityEngine;

///// <summary>
///// 画面上の Web を管理し、Lキーでまとめて破壊してボーナスを計算するシングルトン
///// </summary>
//public class WebManager : MonoBehaviour
//{
//    public static WebManager Instance { get; private set; }

//    // 登録された Web の一覧（null を含む可能性があるので注意）
//    private readonly List<WebController> activeWebs = new List<WebController>();

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            // シーン遷移で消したくない場合は DontDestroyOnLoad(gameObject); を追加
//        }
//        else
//        {
//            Destroy(gameObject);
//            return;
//        }
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.L))
//            DestroyAllWebsWithBonus();
//    }

//    /// <summary>
//    /// WebController から登録される（安全に追加する）
//    /// </summary>
//    public void RegisterWeb(WebController web)
//    {
//        if (web == null) return;
//        if (!activeWebs.Contains(web))
//        {
//            activeWebs.Add(web);
//            Debug.Log($"WebManager: Registered Web (now {activeWebs.Count}) - {web.name}");
//        }
//    }

//    /// <summary>
//    /// WebController から解除される
//    /// </summary>
//    public void UnregisterWeb(WebController web)
//    {
//        if (web == null) return;
//        if (activeWebs.Remove(web))
//        {
//            Debug.Log($"WebManager: Unregistered Web (now {activeWebs.Count}) - {web.name}");
//        }
//    }

//    /// <summary>
//    /// Lキーによる全体破壊＋ボーナス計算
//    /// </summary>
//    private void DestroyAllWebsWithBonus()
//    {
//        // まず null を除去して正確なカウントを取得
//        activeWebs.RemoveAll(x => x == null);
//        int count = activeWebs.Count;
//        Debug.Log($"WebManager: DestroyAllWebs called. Current web count = {count}");

//        if (count == 0) return;

//        // 例として：ボーナスは 2^(n-1) * 10 を適用（n>=2 のとき）
//        int bonus = 0;
//        if (count >= 2)
//        {
//            // Mathf.Pow は float を返すので int にキャスト
//            bonus = (int)(Mathf.Pow(2, count - 1) * 10f);
//            if (ScoreBoard.Instance != null)
//            {
//                ScoreBoard.Instance.AddScore(bonus);
//            }
//            else
//            {
//                Debug.LogWarning("WebManager: ScoreBoard.Instance is null - can't add bonus");
//            }
//        }

//        Debug.Log($"WebManager: Destroying {count} webs. Bonus = {bonus}");

//        // 破壊処理（逆順でループして安全に削除）
//        for (int i = activeWebs.Count - 1; i >= 0; i--)
//        {
//            var web = activeWebs[i];
//            if (web != null)
//            {
//                web.ForceDestroy(); // WebController 側で自分自身の登録解除を行う
//            }
//        }

//        // 最後にリストをクリア（念のため）
//        activeWebs.Clear();
//    }
//}

using System.Collections.Generic;
using UnityEngine;

public class WebManager : MonoBehaviour
{
    public static WebManager Instance { get; private set; }

    private readonly List<WebController> activeWebs = new List<WebController>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            DestroyAllWebsWithBonus();
    }

    public void RegisterWeb(WebController web)
    {
        if (web == null) return;
        if (!activeWebs.Contains(web))
            activeWebs.Add(web);
    }

    public void UnregisterWeb(WebController web)
    {
        if (web == null) return;
        activeWebs.Remove(web);
    }

    private void DestroyAllWebsWithBonus()
    {
        activeWebs.RemoveAll(x => x == null);

        int count = activeWebs.Count;
        if (count == 0) return;

        // ★ ボーナス計算
        int bonus = 0;
        if (count >= 2)
        {
            bonus = (int)(Mathf.Pow(2, count - 1) * 10f);
            ScoreBoard.Instance?.AddScore(bonus);
        }

        // ★ Web を破壊（捕獲敵も Die される）
        for (int i = activeWebs.Count - 1; i >= 0; i--)
        {
            activeWebs[i]?.ForceDestroy();
        }

        activeWebs.Clear();
    }
}
