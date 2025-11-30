using UnityEngine;

public class BossCannonController : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;       // 発射する弾のプレハブ（Inspector で設定）
    public float attackInterval = 2f;     // 弾を発射する間隔
    public float fireSpeed = 4f;          // 弾の速度
    public float bulletAngle = 30f;       // 左右方向への角度（3方向ショット用）

    [Header("Fire Point")]
    public Transform firePoint;           // 発射位置（子オブジェクトを割り当て）

    private float attackTimer = 0f;       // 発射タイミング管理用タイマー
    private bool stopByWeb = false;       // Webに触れている間停止

    void Update()
    {
        // Web中止中は射撃しない
        if (stopByWeb) return;

        // 経過時間加算
        attackTimer += Time.deltaTime;

        // 一定時間経過したら発射
        if (attackTimer >= attackInterval)
        {
            FireBullet3Way();     // 3方向弾を発射
            attackTimer = 0f;     // タイマーリセット
        }
    }

    // 3方向に弾を撃つ処理
    void FireBullet3Way()
    {
        // 中央
        FireBullet(firePoint.rotation);

        // 左 (角度プラス方向)
        Quaternion leftRot = Quaternion.Euler(
            firePoint.eulerAngles.x,
            firePoint.eulerAngles.y,
            firePoint.eulerAngles.z + bulletAngle);
        FireBullet(leftRot);

        // 右 (角度マイナス方向)
        Quaternion rightRot = Quaternion.Euler(
            firePoint.eulerAngles.x,
            firePoint.eulerAngles.y,
            firePoint.eulerAngles.z - bulletAngle);
        FireBullet(rightRot);

        Debug.Log("Boss Cannon 3Way Shot!");
    }

    // 弾を発射する共通関数
    void FireBullet(Quaternion rot)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rot);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(rot * Vector2.down * fireSpeed, ForceMode2D.Impulse);
    }

    // Webに触れたら射撃停止
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Web"))
        {
            stopByWeb = true;
            Debug.Log("Webに触れた → 発射停止");
        }
    }
}
