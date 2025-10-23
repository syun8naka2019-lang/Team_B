using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RPG_Controller : MonoBehaviour
{
    public float speed = 7.0f;                // 移動速度
    public Vector2 direction = Vector2.left;  // 初期方向（左）
    float cout = 0;                           // 経過時間カウント

    private float originalSpeed;              // 元の速度を保存
    public bool isOnWeb = false;              // Webに触れているかどうか
    private List<GameObject> webObjects = new List<GameObject>(); // 触れている全Web
    [Header("爆発エフェクトのPrefabを登録する")]
    public GameObject explosionPrefab;

    void Start()
    {
        originalSpeed = speed; // 最初の速度を保存
    }

    void Update()
    {
        // Webに捕まっていない時だけ移動
        if (!isOnWeb)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        // 方向変更処理（テスト用）
        cout += Time.deltaTime;
        if (cout > 1)
        {
            direction = new Vector2(1, -1);
        }

        // Lキーで敵とWebを消去
        if (isOnWeb && Input.GetKey(KeyCode.L))
        {
            // リストのコピーを作って、それをループする
            List<GameObject> websToDestroy = new List<GameObject>(webObjects);

            foreach (GameObject web in websToDestroy)
            {
                if (web != null)
                    Destroy(web);
            }

            webObjects.Clear();  // すべて削除したあとにリストを空にする

            //爆発を出す（ここを追加！）
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Debug.Log("爆発発生！");
            }

            // 敵を削除
            Destroy(gameObject);
            Debug.Log("Lキー押下 → 敵と全Webを消去");
        }
    }

    // Webに触れたとき
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Web"))
        {
            isOnWeb = true;
            speed = 0.0f; // 動きを止める
            if (!webObjects.Contains(collision.gameObject))
            {
                webObjects.Add(collision.gameObject);
            }

            Debug.Log("Webに接触 → 敵停止");
        }
    }

    // Webから離れたとき
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Web"))
        {
            webObjects.Remove(collision.gameObject);
            if (webObjects.Count == 0)
            {
                isOnWeb = false;
                speed = originalSpeed; // 動きを再開
                Debug.Log("Webから離れた → 再始動");
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
