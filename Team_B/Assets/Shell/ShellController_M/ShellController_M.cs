using UnityEngine;
using System.Collections;

public class ShellController_M : MonoBehaviour
{
    [Header("※時間経過では壊れません")]
    public float lifeTime;

    [Header("消滅時エフェクト")]
    public GameObject destroyEffectPrefab;

    [Header("エフェクトの寿命")]
    public float effectLifeTime = 0.5f;

    void Start()
    {
        // Rigidbody2D の取得（未使用だが残しておく）
        GetComponent<Rigidbody2D>();

        // ★時間経過で壊す処理は削除（または無効化）
        // StartCoroutine(DestroyBulletAfterTime());
    }

    // ★このコルーチンはもう使わないが、念のため残す（必要なら再利用可能）
    IEnumerator DestroyBulletAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);

        CreateDestroyEffect();
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Web に当たったら破壊
        if (collision.CompareTag("Web"))
        {
            CreateDestroyEffect();
            Destroy(gameObject);
        }

        // 障害物に当たったら破壊
        if (collision.CompareTag("shougaibutu"))
        {
            CreateDestroyEffect();
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        // 画面外に出たら破壊
        CreateDestroyEffect();
        Destroy(gameObject);
    }

    /// <summary>
    /// 消滅時エフェクトの生成処理
    /// </summary>
    private void CreateDestroyEffect()
    {
        if (destroyEffectPrefab != null)
        {
            GameObject effect = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, effectLifeTime);
        }
    }
}
