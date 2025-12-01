using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// プレイヤーのステータス管理
/// HP・スコア・移動速度・無敵・ゲームオーバー処理を統合
/// </summary>
public class PlayerStatus : MonoBehaviour
{
    [Header("ステータス")]
    public int score = 0;       // プレイヤーのスコア
    public int maxHp = 5;       // 最大HP
    public int currentHp;       // 現在のHP

    [Header("無敵時間")]
    public float invincibleTime = 2.0f; // ダメージ後の無敵時間
    private bool isInvincible = false;
    private float invincibleTimer = 0f;

    [Header("HPごとのスプライト")]
    public Sprite HP_MAX;  // HPが3以上
    public Sprite HP_MID;  // HP2
    public Sprite HP_MIN;  // HP1
    public Sprite HP_ZERO; // HP0（死亡時）

    [Header("ゲームオーバー設定")]
    public GameObject mainImage; // ゲームオーバー時に表示するUI
    public string sceneName;     // ゲームオーバー時に遷移するシーン名

    [Header("移動速度")]
    public float moveSpeed = 7f; // 通常移動速度
    private float baseSpeed;     // 元の移動速度保持

    private SpriteRenderer spriteRenderer; // プレイヤーのスプライト表示

    private void Awake()
    {
        // スプライト取得
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer がありません！追加してください。");
        }
    }

    private void Start()
    {
        currentHp = maxHp;   // 初期HPセット
        baseSpeed = moveSpeed;
        UpdateHpSprite();    // HPスプライト更新

        // ゲームオーバー画像がONになっている場合は非表示に
        if (mainImage != null)
            mainImage.SetActive(false);
    }

    private void Update()
    {
        // 無敵時間処理
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0f)
                isInvincible = false;
        }
    }

    // --- スコア加算 ---
    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }

    // --- HP回復 ---
    public void Heal(int value)
    {
        currentHp += value;
        if (currentHp > maxHp) currentHp = maxHp; // 回復上限
        UpdateHpSprite();
        Debug.Log("HP: " + currentHp);
    }

    // --- ダメージ処理 ---
    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHp -= damage;
        if (currentHp <= 0)
        {
            currentHp = 0;
            UpdateHpSprite();
            Die(); // 死亡処理
        }
        else
        {
            UpdateHpSprite();
            isInvincible = true;
            invincibleTimer = invincibleTime;
        }
    }

    // --- HPスプライト更新 ---
    private void UpdateHpSprite()
    {
        if (spriteRenderer == null) return;

        if (currentHp >= 3)
            spriteRenderer.sprite = HP_MAX;
        else if (currentHp == 2)
            spriteRenderer.sprite = HP_MID;
        else if (currentHp == 1)
            spriteRenderer.sprite = HP_MIN;
        else
            spriteRenderer.sprite = HP_ZERO;
    }

    // --- プレイヤー死亡処理 ---
    private void Die()
    {
        Debug.Log("プレイヤー死亡");
        if (mainImage != null)
            mainImage.SetActive(true);  // ゲームオーバー画像表示

        if (!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadScene(sceneName); // シーン遷移
    }

    // --- 永続スピードアップ ---
    public void AddSpeed(float amount)
    {
        moveSpeed += amount;
        Debug.Log("Speed Up! 現在: " + moveSpeed);
    }

    // --- 一時的スピードアップ ---
    public void AddSpeedTemporary(float amount, float duration)
    {
        StartCoroutine(SpeedUpCoroutine(amount, duration));
    }

    private IEnumerator SpeedUpCoroutine(float amount, float duration)
    {
        moveSpeed += amount;
        Debug.Log("Speed Up! 現在: " + moveSpeed);

        yield return new WaitForSeconds(duration);

        moveSpeed -= amount;
        Debug.Log("Speed Down… 現在: " + moveSpeed);
    }

    // --- 外部から現在のHP取得 ---
    public int CurrentHp => currentHp;
}
