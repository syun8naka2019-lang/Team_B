using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillGaugeManager : MonoBehaviour
{
    // シングルトン（シーンをまたいで1つだけ存在）
    public static SkillGaugeManager Instance;

    // ===============================
    // ゲージ関連
    // ===============================
    [Header("Skill Gauge Settings")]
    [SerializeField] private int maxGauge = 4;
    public int skillGauge = 0; // 現在のゲージ値

    // ===============================
    // アニメーション関連
    // ===============================
    private Animator animator;
    private string nowAnime = "";
    private string oldAnime = "";

    [Header("Animation States")]
    public string gage0 = "gade0";
    public string gage1 = "gade1";
    public string gage2 = "gade2";
    public string gage3 = "gade3";
    public string gage4 = "gade4";

    // ===============================
    // Unity Lifecycle
    // ===============================
    private void Awake()
    {
        // シングルトンパターン
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン切り替えで保持
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        UpdateGaugeAnimation();
    }

    // ===============================
    // 公開メソッド
    // ===============================

    /// <summary>
    /// ゲージを増やす
    /// </summary>
    public void AddGauge(int amount)
    {
        skillGauge = Mathf.Min(skillGauge + amount, maxGauge);
        UpdateGaugeAnimation();
        Debug.Log($"[SkillGauge] ゲージ上昇 → {skillGauge}");
    }

    /// <summary>
    /// ゲージをリセット
    /// </summary>
    public void ResetGauge()
    {
        skillGauge = 0;
        UpdateGaugeAnimation();
    }

    /// <summary>
    /// 現在のゲージ値を取得
    /// </summary>
    public int GetGauge()
    {
        return skillGauge;
    }

    // ===============================
    // 内部処理
    // ===============================

    private void UpdateGaugeAnimation()
    {
        if (animator == null) return;

        switch (skillGauge)
        {
            case 0: nowAnime = gage0; break;
            case 1: nowAnime = gage1; break;
            case 2: nowAnime = gage2; break;
            case 3: nowAnime = gage3; break;
            case 4: nowAnime = gage4; break;
        }

        if (nowAnime != oldAnime)
        {
            animator.Play(nowAnime);
            oldAnime = nowAnime;
        }
    }
}
