using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifetime = 0.5f;  // 爆発が表示されている時間（秒）
    public float radius = 2.0f;    // 爆風が届く範囲（半径）
    private SpriteRenderer sr;     // 爆発画像（Sprite）の描画コンポーネントを保持する変数

    void Start()
    {
        // 爆発画像（Sprite Renderer）を取得
        sr = GetComponent<SpriteRenderer>();

        // ===============================
        // 爆風の範囲内にいる敵を探して破壊する
        // ===============================
        // 爆発地点を中心に半径 radius の円を作り、
        // その範囲にあるすべてのCollider2Dを取得する
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D hit in hits)
        {
            // もし「Enemy」タグのついたオブジェクトが範囲内にあれば破壊
            if (hit.CompareTag("Enemy"))
            {
                Destroy(hit.gameObject);
                Debug.Log("爆風で敵を破壊！: " + hit.name);
            }
        }

        // ===============================
        // 見た目の爆発エフェクト（拡大とフェードアウト）を開始
        // ===============================
        StartCoroutine(ExplosionEffect());
    }

    // ========================================================
    // 爆発の見た目を演出するコルーチン
    // 拡大しながら徐々に透明にして、最後に消す
    // ========================================================
    IEnumerator ExplosionEffect()
    {
        // 初期状態の大きさと色を記録しておく
        Vector3 startScale = transform.localScale;
        Color startColor = sr.color;

        float elapsed = 0;  // 経過時間
        while (elapsed < lifetime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / lifetime; // 0 → 1 に進む割合

            //時間が経つにつれて爆発を1.5倍に拡大
            transform.localScale = startScale * (1 + t * 1.5f);

            //徐々に透明（アルファ値を減らす）
            sr.color = new Color(startColor.r, startColor.g, startColor.b, 1 - t);

            yield return null; // 次のフレームまで待機
        }

        //最後に爆発オブジェクトを削除
        Destroy(gameObject);
    }

    // ========================================================
    //  Sceneビューで爆風範囲を可視化するための補助線
    // （ゲーム中には表示されない）
    // ========================================================
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
