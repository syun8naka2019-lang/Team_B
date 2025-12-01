using UnityEngine;
using System.Collections;

public class ShellController_M : MonoBehaviour
{
    [Header("弾の寿命")]
    public float lifeTime;

    [Header("消滅時エフェクト")]
    public GameObject destroyEffectPrefab;

    [Header("エフェクトの寿命")]
    public float effectLifeTime = 0.5f;

    void Start()
    {
        // 弾が一定時間後に消える処理をコルーチンで実行
        StartCoroutine(DestroyBulletAfterTime());
       GetComponent<Rigidbody2D>();
    }

    IEnumerator DestroyBulletAfterTime()
    {
        // 弾の寿命待機
        yield return new WaitForSeconds(lifeTime);

        // エフェクト生成
        if (destroyEffectPrefab != null)
        {
            GameObject effect = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);

            // エフェクトも一定時間後に消す
            Destroy(effect, effectLifeTime);
        }

        // 弾を削除
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Web")
        {
            Destroy(this.gameObject);
           
        }
        if (collision.gameObject.tag == "shougaibutu")
        {
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()//
    {
        Destroy(gameObject); //
    }

}