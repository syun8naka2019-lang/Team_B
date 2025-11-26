using UnityEngine;
using UnityEngine.SceneManagement;

public class Hp : MonoBehaviour
{
    [Header("プレイヤーの最大HP")]
    public int maxHp = 3; // プレイヤーの最大HP

    [Header("無敵時間（秒）")]
    public float invincibleTime = 2.0f; // ダメージを受けた後、何秒間無敵になるか

    [Header("HPごとのスプライト")]
    public Sprite HP_MAX;  // HPが3のときに表示する画像
    public Sprite HP_MID;  // HPが2のときに表示する画像
    public Sprite HP_MIN;  // HPが1のときに表示する画像
    public Sprite HP_ZERO; // HPが0のときに表示する画像（ゲームオーバー用）

    public GameObject mainImage;
    public string sceneName;

    private int currentHp;               // 現在のHPを保持
    private bool isInvincible = false;   // 無敵状態かどうか
    private float invincibleTimer = 0f;  // 無敵時間の残り時間

    private SpriteRenderer spriteRenderer; // スプライトを切り替えるためのコンポーネント

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        
    }

    // ゲーム開始時に呼ばれる
    void Start()
    {
        currentHp = maxHp; // 初期HPを最大HPに設定
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRendererを取得
        UpdateHpSprite(); // 初期HPに合わせてスプライトを設定
    }

    // 毎フレーム呼ばれる
    void Update()
    {
        // 無敵時間中ならカウントダウン
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            // 無敵時間が終わったらフラグを解除
            if (invincibleTimer <= 0f)
            {
                isInvincible = false;
                // 無敵終了時に色や見た目を戻したい場合はここに記述
                // 例: spriteRenderer.color = Color.white;
            }
        }
    }

    // ダメージを受けた時に呼ぶ関数
    public void TakeDamage(int damage)
    {
        // 無敵状態ならダメージを受けない
        if (isInvincible)
            return;

        // HPを減らす
        currentHp -= damage;

        if (currentHp <= 0)
        {
            // HPが0以下になった場合
            currentHp = 0;
            UpdateHpSprite(); // スプライトをHP0用に変更
            Die(); // 死亡処理を実行
        }
        else
        {
            // HPが残っている場合はスプライト更新
            UpdateHpSprite();

            // 無敵状態にする
            isInvincible = true;
            invincibleTimer = invincibleTime;

            // 無敵中に点滅させたり色変えたりしたい場合はここに記述
            // 例: StartCoroutine(FlashEffect());
        }
    }

    // 現在のHPに応じてスプライトを変更する関数
    private void UpdateHpSprite()
    {
        switch (currentHp)
        {
            case 3:
                spriteRenderer.sprite = HP_MAX;
                break;
            case 2:
                spriteRenderer.sprite = HP_MID;
                break;
            case 1:
                spriteRenderer.sprite = HP_MIN;
                break;
            case 0:
                spriteRenderer.sprite = HP_ZERO;
                break;
            default:
                // 想定外の値の場合はHP0用に設定
                spriteRenderer.sprite = HP_ZERO;
                break;
        }
    }

    // プレイヤー死亡時の処理
    private void Die()
    {
        Debug.Log("プレイヤー死亡");

      
            // ゲームオーバー
            mainImage.SetActive(true);      // 画像を表示する
            SceneManager.LoadScene(sceneName);
        
        // ゲームオーバー画面に移動したり、動きを止める処理をここに書く
        // 例: SceneManager.LoadScene("GameOver");
    }

    // 現在のHPを他のスクリプトから取得するためのプロパティ
    public int CurrentHp => currentHp;
}
