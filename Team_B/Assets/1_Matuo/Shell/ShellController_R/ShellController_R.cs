/*
using UnityEngine;
using System.Collections;
public class ShellController_R : MonoBehaviour
{
    public float lifeTime = 3f; // 弾が存在する時間
    public GameObject destroyEffectPrefab; // 消えるときのエフェクトプレハブ

    private bool isDestroyed = false;

    void Start()
    {
        // コルーチンを呼ぶ
        StartCoroutine(DestroyWithEffect());
    }

    IEnumerator DestroyWithEffect()
    {
        if (destroyEffectPrefab != null)
            Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);

        GetComponent<SpriteRenderer>().enabled = false; // 弾の見た目を消す
        yield return new WaitForSeconds(0.2f);          // 少し待ってから破棄
        Destroy(gameObject);
    }

}
*/
using UnityEngine;
using System.Collections;

public class ShellController_R : MonoBehaviour
{
    [Header("弾の寿命")]
    public float lifeTime = 3f;

    [Header("消滅時エフェクト")]
    public GameObject destroyEffectPrefab;

    [Header("エフェクトの寿命")]
    public float effectLifeTime = 0.5f;

    void Start()
    {
        // 弾が一定時間後に消える処理をコルーチンで実行
        StartCoroutine(DestroyBulletAfterTime());
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