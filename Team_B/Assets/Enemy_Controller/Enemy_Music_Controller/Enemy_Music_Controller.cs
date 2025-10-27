using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Music_Controller : MonoBehaviour
{
    public float speed = 7.0f;                // 移動速度
    public Vector2 direction = Vector2.left;  // 初期方向（左）
    private float cout = 0;                    // 経過時間カウント用

    private float originalSpeed;              // 元の速度を保存
    public bool isOnWeb = false;              // Webに触れているかどうか
    private List<GameObject> webObjects = new List<GameObject>(); // 触れている全Web
    [Header("爆発エフェクトのPrefabを登録する")]
    public GameObject explosionPrefab;        // 爆発エフェクトPrefab

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

        // テスト用の方向変更（任意）
        cout += Time.deltaTime;
        if (cout > 1)
        {
            direction = new Vector2(-1, -1);
        }

        // Lキーで敵とWebを同時に消去
        if (isOnWeb && Input.GetKey(KeyCode.L))
        {
            // 捕まっているWebをすべて削除
            List<GameObject> websToDestroy = new List<GameObject>(webObjects);
            foreach (GameObject web in websToDestroy)
            {
                if (web != null)
                    Destroy(web);
            }
            webObjects.Clear();

            // 爆発エフェクトを生成
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Debug.Log("爆発発生！（Music）");
            }

            // 敵自身も削除
            Destroy(gameObject);
            Debug.Log("Lキー押下 → Music敵と全Webを消去");
        }
    }

    // Webに触れたとき
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Tagが "Web_stop" のWebに当たった場合
        if (collision.gameObject.CompareTag("Web_stop"))
        {
            isOnWeb = true;       // Web捕獲状態
            speed = 0.0f;         // 移動を止める

            // webObjectsに追加（重複防止）
            if (!webObjects.Contains(collision.gameObject))
            {
                webObjects.Add(collision.gameObject);
            }

            Debug.Log("Webに接触 → Music敵停止");
        }
    }

    // Webから離れたとき
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Web_stop"))
        {
            webObjects.Remove(collision.gameObject);

            // すべてのWebから離れたら再始動
            if (webObjects.Count == 0)
            {
                isOnWeb = false;
                speed = originalSpeed;
                Debug.Log("Webから離れた → Music敵再始動");
            }
        }
    }

    private void OnBecameInvisible()
    {
        // 画面外に出たら削除
        Destroy(gameObject);
    }
}