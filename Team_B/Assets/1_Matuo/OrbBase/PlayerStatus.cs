using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;   // ← Text を使うために必要

/// <summary>
/// プレイヤーの HP / スコア / 無敵 / 死亡処理などを管理するクラス
/// </summary>
public class PlayerStatus : MonoBehaviour
{
    // ================================
    // プレイヤー基本ステータス
    // ================================
    [Header("ステータス")]
    public int score = 0;     // 現在のスコア
    public int maxHp = 5;     // HPの最大値
    public int currentHp;     // 現在のHP

    // ================================
    // スコア UI
    // ================================
    [Header("スコアUI")]
    public Text scoreText;    // 画面に表示するスコア用 Text

    // ================================
    // 無敵時間
    // ================================
    [Header("無敵時間")]
    public float invincibleTime = 2.0f; // ダメージ後の無敵時間
    private bool isInvincible = false;  // 無敵中かどうか
    private float invincibleTimer = 0f; // 無敵残り時間カウンタ

    // ================================
    // HP によって変わるプレイヤー画像
    // ================================
    [Header("HPごとのスプライト")]
    public Sprite HP_MAX;   // HP3〜5 のとき
    public Sprite HP_MID;   // HP2 のとき
    public Sprite HP_MIN;   // HP1 のとき
    public Sprite HP_ZERO;  // HP0（死亡時）

    // ================================
    // ゲームオーバー
    // ================================
    [Header("ゲームオーバー設定")]
    public GameObject mainImage; // ゲームオーバー時に出すイメージ
    public string sceneName;     // ゲームオーバー後の遷移先シーン

    // ================================
    // 移動速度
    // ================================
    [Header("移動速度")]
    public float moveSpeed = 7f; // 通常移動速度
    private float baseSpeed;     // 元の移動速度を保持

    // プレイヤーの表示に使う SpriteRenderer
    private SpriteRenderer spriteRenderer;


    // ==========================================
    // 初期化処理
    // ==========================================
    private void Awake()
    {
        // プレイヤーの SpriteRenderer を取得
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            Debug.LogError("SpriteRenderer がありません！");
    }

    private void Start()
    {
        currentHp = maxHp;    // HP を最大にセット
        baseSpeed = moveSpeed;

        UpdateHpSprite();     // HPに応じたスプライトに設定

        // ゲームオーバー画像が ON になっている場合は消す
        if (mainImage != null)
            mainImage.SetActive(false);

        // スコア表示を初期化
        if (scoreText != null)
            scoreText.text = "Score : " + score;
    }


    // ==========================================
    // 毎フレーム呼ばれる（無敵時間管理）
    // ==========================================
    private void Update()
    {
        // ダメージ後の無敵時間処理
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            // 時間が過ぎたら無敵解除
            if (invincibleTimer <= 0f)
                isInvincible = false;
        }
    }


    // ==========================================
    // スコア加算（UI も更新する）
    // ==========================================
    public void AddScore(int value)
    {
        score += value; // スコア更新
        Debug.Log("Score: " + score);

        // UI更新（忘れると画面に反映されない！）
        if (scoreText != null)
            scoreText.text = "Score : " + score;
    }


    // ==========================================
    // HP 回復
    // ==========================================
    public void Heal(int value)
    {
        currentHp += value;

        // HP は maxHp を超えない
        if (currentHp > maxHp)
            currentHp = maxHp;

        UpdateHpSprite();
        Debug.Log("HP: " + currentHp);
    }


    // ==========================================
    // ダメージ処理
    // ==========================================
    public void TakeDamage(int damage)
    {
        // 無敵中はダメージを受けない
        if (isInvincible) return;

        currentHp -= damage;

        if (currentHp <= 0)
        {
            currentHp = 0;
            UpdateHpSprite();
            Die(); // HP が 0 になったら死亡処理
        }
        else
        {
            UpdateHpSprite();

            // ダメージ後、少しの間だけ無敵になる
            isInvincible = true;
            invincibleTimer = invincibleTime;
        }
    }


    // ==========================================
    // HPによってスプライトを変更
    // ==========================================
    private void UpdateHpSprite()
    {
        if (currentHp >= 3)
            spriteRenderer.sprite = HP_MAX;
        else if (currentHp == 2)
            spriteRenderer.sprite = HP_MID;
        else if (currentHp == 1)
            spriteRenderer.sprite = HP_MIN;
        else
            spriteRenderer.sprite = HP_ZERO;
    }


    // ==========================================
    // プレイヤー死亡処理
    // ==========================================
    private void Die()
    {
        Debug.Log("プレイヤー死亡");

        // ゲームオーバー画像表示
        if (mainImage != null)
            mainImage.SetActive(true);

        // 指定のシーンに遷移
        if (!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadScene(sceneName);
    }


    // ==========================================
    // 永続スピードアップ
    // ==========================================
    public void AddSpeed(float amount)
    {
        moveSpeed += amount;
        Debug.Log("Speed Up! 現在: " + moveSpeed);
    }


    // ==========================================
    // 一時的スピードアップ
    // ==========================================
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

    // HP を外部から参照できるプロパティ
    public int CurrentHp => currentHp;
}
