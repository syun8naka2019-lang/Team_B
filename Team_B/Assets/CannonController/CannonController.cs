using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject objPrefab;      // 発射する弾のPrefab
    public float delayTime = 0.5f;    // 弾を発射する間隔（秒）
    public float yTime = 0.0f;        // 弾発射開始までの待ち時間
    public float fireSpeed = 4.0f;    // 弾の発射速度

    Transform gateTransform;          // 発射口のTransform
    float passedTimes = 0;            // 経過時間の計測用

    void Start()
    {
        // 発射口のTransformを取得
        gateTransform = transform.Find("gate");
    }

    void Update()
    {
        // 発射開始時間の処理
        float remainingTime = yTime - Time.time;
        if (remainingTime > 0)
        {
            return; // まだ発射開始時間前なら何もしない
        }

        // 経過時間を加算
        passedTimes += Time.deltaTime;

        // delayTimeを超えたら弾を発射
        if (passedTimes > delayTime)
        {
            FireCannon();   // 弾の発射
            passedTimes = 0; // 経過時間リセット
        }
    }

    // 弾を発射
    void FireCannon()
    {
        Vector2 pos = gateTransform.position; // 発射口の位置
        GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity); // 弾生成
        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>(); // Rigidbody2D取得
        rbody.AddForce(Vector2.down * fireSpeed, ForceMode2D.Impulse); // 下方向に発射
    }

    // ★ Web に触れても何もしない！(発射を止めない)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Web"))
        {
            Debug.Log("Web に触れたが、発射は停止しません。");
        }
    }
}
