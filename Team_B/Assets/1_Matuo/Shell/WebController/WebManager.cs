//using System.Collections.Generic;
//using UnityEngine;

//public class WebManager : MonoBehaviour
//{
//    public static WebManager Instance { get; private set; }

//    private readonly List<WebController> activeWebs = new List<WebController>();

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
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

//    public void RegisterWeb(WebController web)
//    {
//        if (web == null) return;
//        if (!activeWebs.Contains(web))
//            activeWebs.Add(web);
//    }

//    public void UnregisterWeb(WebController web)
//    {
//        if (web == null) return;
//        activeWebs.Remove(web);
//    }

//    private void DestroyAllWebsWithBonus()
//    {
//        activeWebs.RemoveAll(x => x == null);

//        int count = activeWebs.Count;
//        if (count == 0) return;

//        // ★ ボーナス計算
//        int bonus = 0;
//        if (count >= 2)
//        {
//            bonus = (int)(Mathf.Pow(2, count - 1) * 10f);
//            ScoreBoard.Instance?.AddScore(bonus);
//        }

//        // ★ Web を破壊（捕獲敵も Die される）
//        for (int i = activeWebs.Count - 1; i >= 0; i--)
//        {
//            activeWebs[i]?.ForceDestroy();
//        }

//        activeWebs.Clear();
//    }
//}

using System.Collections.Generic;
using UnityEngine;

public class WebManager : MonoBehaviour
{
    public static WebManager Instance { get; private set; }

    // アクティブなWeb一覧
    private readonly List<WebController> activeWebs = new List<WebController>();

    // 捕獲されている敵一覧（ボーナス計算用）
    private readonly List<EnemyBaseController> capturedEnemies = new List<EnemyBaseController>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            ExplodeAllWithBonus();
    }

    //==============================
    // Web 管理
    //==============================
    public void RegisterWeb(WebController web)
    {
        if (web != null && !activeWebs.Contains(web))
            activeWebs.Add(web);
    }

    public void UnregisterWeb(WebController web)
    {
        if (web != null)
            activeWebs.Remove(web);
    }

    //==============================
    // 捕獲敵 管理 ★これが無かった
    //==============================
    public void RegisterCapturedEnemy(EnemyBaseController enemy)
    {
        if (enemy != null && !capturedEnemies.Contains(enemy))
            capturedEnemies.Add(enemy);
    }

    public void UnregisterCapturedEnemy(EnemyBaseController enemy)
    {
        if (enemy != null)
            capturedEnemies.Remove(enemy);
    }

    //==============================
    // Lキー全爆発
    //==============================
    private void ExplodeAllWithBonus()
    {
        // null掃除
        capturedEnemies.RemoveAll(e => e == null);
        activeWebs.RemoveAll(w => w == null);

        int count = capturedEnemies.Count;
        if (count == 0) return;

        // 捕獲数ボーナス
        long bonus = (count - 1) * 20;
        if (bonus > 0)
            ScoreBoard.Instance?.AddScore(bonus);

        // ★ リストコピー（これがないと例外出る）
        var enemiesCopy = new List<EnemyBaseController>(capturedEnemies);
        var websCopy = new List<WebController>(activeWebs);

        // 先にクリア（Die中のRemove対策）
        capturedEnemies.Clear();
        activeWebs.Clear();

        // 敵を倒す（基本点は敵側）
        foreach (var enemy in enemiesCopy)
            if (enemy != null)
                enemy.Die(true);

        // Web削除
        foreach (var web in websCopy)
            if (web != null)
                web.ForceDestroy();
    }
}

