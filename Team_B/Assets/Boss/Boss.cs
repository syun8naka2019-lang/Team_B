using UnityEngine;

public class Boss : MonoBehaviour
{
    public int maxHp = 100;       // ボスの最大HP
    private int currentHp;        // 現在のHP（ゲーム中に変動）

    public GameObject bulletPrefab;  // 発射する弾のPrefab
    public Transform firePoint;      // 弾の発射位置（空のオブジェクトを指定）
    public float fireInterval = 2f;  // 弾を撃つ間隔（2秒に1回）

    private float fireTimer = 0f;    // 時間を計測するためのタイマー

    void Start()
    {
        currentHp = maxHp; // 最初にHPを満タンにしておく
    }

    void Update()
    {
        // 時間を加算していく
        fireTimer += Time.deltaTime;

        // 一定時間（fireInterval）経過したら弾を撃つ
        if (fireTimer >= fireInterval)
        {
            Shoot();      // 弾を発射する関数を呼ぶ
            fireTimer = 0f; // タイマーをリセット
        }
    }

    // ───────────────────────────────
    // ■ 弾を撃つ処理
    // ───────────────────────────────
    void Shoot()
    {
        // bulletPrefab を firePoint の位置と向きで生成
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    // ───────────────────────────────
    // ■ ダメージを受けたときに呼ばれる関数
    //    （プレイヤーの弾などから実行される）
    // ───────────────────────────────
    public void TakeDamage(int damage)
    {
        currentHp -= damage; // HPを減らす

        // HP が 0 以下になったら死亡処理へ
        if (currentHp <= 0)
        {
            Die();
        }
    }

    // ───────────────────────────────
    // ■ ボスの死亡処理
    // ───────────────────────────────
    void Die()
    {
        Debug.Log("ボス撃破！"); // コンソールに表示
        Destroy(gameObject);     // ボス本体を消す
    }
}
