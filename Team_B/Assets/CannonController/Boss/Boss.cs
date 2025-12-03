using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Boss Status")]
    public int maxHp = 100;                // ボスの最大HP（Inspector上で変更できる）
    public float attackInterval = 2f;      // 攻撃する間隔（秒）

    [Header("Attack Settings")]
    public GameObject breath_0;            // 発射する Breath_0 のプレハブ
    public Transform firePoint;            // Breath_0 を発射する位置
    public float bulletAngle = 30f;        // 左右方向にズラす角度（3方向ショット用）

    private int currentHp;                 // 現在のHP
    private float attackTimer = 0f;        // 攻撃タイミング管理用のタイマー

    void Start()
    {
        // ゲーム開始時にHPを最大値へ設定
        currentHp = maxHp;
    }

    void Update()
    {
        // 毎フレーム経過時間を加算
        attackTimer += Time.deltaTime;

        // 指定時間経過したら攻撃
        if (attackTimer >= attackInterval)
        {
            Attack_Breath0();              // Breath_0 を撃つ関数を実行
            attackTimer = 0f;              // タイマーをリセット
        }
    }

    // Breath_0 を3方向へ撃つ処理
    void Attack_Breath0()
    {
        // 中央方向に発射
        Instantiate(breath_0, firePoint.position, firePoint.rotation);

        // 左方向へ bulletAngle 度回転して発射
        Quaternion leftRot = Quaternion.Euler(
            firePoint.eulerAngles.x,       // X回転
            firePoint.eulerAngles.y,       // Y回転
            firePoint.eulerAngles.z + bulletAngle);  // Z回転(角度加算)
        Instantiate(breath_0, firePoint.position, leftRot);

        // 右方向へ bulletAngle 度回転して発射
        Quaternion rightRot = Quaternion.Euler(
            firePoint.eulerAngles.x,
            firePoint.eulerAngles.y,
            firePoint.eulerAngles.z - bulletAngle);
        Instantiate(breath_0, firePoint.position, rightRot);

        // デバッグログ
        Debug.Log("Boss Breath_0 Attack!");
    }

    // ダメージを受けた時の処理
    public void TakeDamage(int damage)
    {
        currentHp -= damage;               // HPを減らす

        // HPが0以下になった場合
        if (currentHp <= 0)
        {
            Die();                         // 死亡処理へ
        }
    }

    // ボスが倒れた時の処理
    void Die()
    {
        Destroy(gameObject);               // ボスのゲームオブジェクトを削除
        Debug.Log("Boss Defeated!");       // デバッグログ
    }
}
