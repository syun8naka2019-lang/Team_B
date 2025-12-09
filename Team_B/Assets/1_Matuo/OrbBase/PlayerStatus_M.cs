using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatus_M : MonoBehaviour
{
    [Header("ステータス")]
    public int score = 0;               // スコア
    public Text scoreText;              // UI表示用

    [Header("HP設定")]
    public int maxHp = 2;               // 最大HP
    public int currentHp;               // 現在HP

    [Header("自動回復設定")]
    public float healInterval = 20f;    // 自動回復間隔（秒）
    public int healAmount = 1;          // 回復量

    [Header("無敵時間（秒）")]
    public float invincibleTime = 2f;   // ダメージ後の無敵時間
    private bool isInvincible = false;

    [Header("HPごとのスプライト")]
    public Sprite HP_FULL;              // HP2
    public Sprite HP_HALF;              // HP1
    public Sprite HP_ZERO;              // HP0

    [Header("移動設定")]
    public float moveSpeed = 7f;        // 基本移動速度
    private float baseSpeed;

    [Header("ゲームオーバー設定")]
    public GameObject mainImage;        // ゲームオーバー表示用
    public string gameOverSceneName;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            Debug.LogWarning("SpriteRenderer がありません！");
    }

    private void Start()
    {
        currentHp = maxHp;
        baseSpeed = moveSpeed;
        UpdateHpSprite();
        UpdateScoreUI();
        StartCoroutine(AutoHealRoutine());
    }

    private void Update()
    {
        // 無敵処理はTakeDamage側で管理
    }

    #region スコア管理
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
    #endregion

    #region HP管理
    // ダメージ処理
    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHp -= damage;
        if (currentHp < 0) currentHp = 0;

        UpdateHpSprite();

        if (currentHp == 0)
        {
            Die();
            return;
        }

        // 無敵時間開始
        isInvincible = true;
        StartCoroutine(InvincibleCoroutine());
    }

    private IEnumerator InvincibleCoroutine()
    {
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    // 自動回復
    private IEnumerator AutoHealRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(healInterval);

            if (currentHp > 0 && currentHp < maxHp)
            {
                currentHp += healAmount;
                if (currentHp > maxHp) currentHp = maxHp;
                UpdateHpSprite();
                Debug.Log($"HP自動回復: {currentHp}");
            }
        }
    }

    private void UpdateHpSprite()
    {
        if (currentHp == maxHp)
            spriteRenderer.sprite = HP_FULL;
        else if (currentHp == 1)
            spriteRenderer.sprite = HP_HALF;
        else
            spriteRenderer.sprite = HP_ZERO;
    }
    #endregion

    #region 移動管理
    public void AddSpeed(float amount)
    {
        moveSpeed += amount;
    }

    public void AddSpeedTemporary(float amount, float duration)
    {
        StartCoroutine(SpeedUpCoroutine(amount, duration));
    }

    private IEnumerator SpeedUpCoroutine(float amount, float duration)
    {
        moveSpeed += amount;
        yield return new WaitForSeconds(duration);
        moveSpeed -= amount;
    }
    #endregion

    private void Die()
    {
        Debug.Log("プレイヤー死亡");
        if (mainImage != null) mainImage.SetActive(true);
        SceneManager.LoadScene(gameOverSceneName);
    }
}
