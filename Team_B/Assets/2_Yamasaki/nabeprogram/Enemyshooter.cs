using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float angle = 30f;      // 左右の角度
    public float shootInterval = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        // 正面（下）
        Fire(Vector2.down);

        // 左下
        Fire(Quaternion.Euler(0, 0, angle) * Vector2.down);

        // 右下
        Fire(Quaternion.Euler(0, 0, -angle) * Vector2.down);
    }

    void Fire(Vector2 dir)
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position,
            Quaternion.identity
        );

        bullet.GetComponent<EnemyBullet>().SetDirection(dir);
    }
}
