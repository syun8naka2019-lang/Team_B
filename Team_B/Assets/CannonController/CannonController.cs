/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject objPrefab;        //発生させるPrefabデータ
    public float delayTime = 0.5f;      //遅延時間
    public float fireSpeed = 4.0f;      //発射速度

    Transform gateTransform;
    float passedTimes = 0;              //経過時間
    bool stopByWeb = false;

    void Start()
    {
        //発射口オブジェクトのTransformを取得
        gateTransform = transform.Find("gate");
    }       
    private void Update()
    {
        passedTimes += Time.deltaTime;
        if (passedTimes > delayTime)
        {
            passedTimes = 0;

            Vector2 pos = new Vector2(gateTransform.position.x, gateTransform.position.y);
            //弾生成
            GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);
            //砲弾が向いている方向に発射
            Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
            Vector2 v = new Vector2(0, -1) * fireSpeed;
            rbody.AddForce(v, ForceMode2D.Impulse);
        }
    }
    

}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject objPrefab;        // 発射する弾のPrefab
    public float delayTime = 0.5f; // 弾を発射する間隔（秒）
    public float yTime = 0.0f;        //弾発射
    public float fireSpeed = 4.0f;      // 弾の発射速度

    Transform gateTransform;            // 発射口のTransform
    float passedTimes = 0;    // 経過時間の計測用

 

    bool stopByWeb = false;             // Webに触れている間はtrue
    bool stopBysho = false;
    void Start()
    {
        // 発射口オブジェクトのTransformを取得
        gateTransform = transform.Find("gate");
    }

    void Update()
    {

        // Webに触れていない場合のみ弾を発射
        float remainingTime = yTime - Time.time;
        if (remainingTime > 0)
        {
         
        }
        else
        {
            if (!stopByWeb)
            {
                // 経過時間を加算
                passedTimes += Time.deltaTime;

                // delayTimeを超えたら弾を発射
                if (passedTimes > delayTime)
                {
                    FireCannon();       // 弾の発射
                    passedTimes = 0;    // 経過時間をリセット
                }
            }
        }
      
        
    }

    // 弾を生成して発射する処理
    void FireCannon()
    {
        
            Vector2 pos = gateTransform.position;                        // 発射口の位置
            GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity); // 弾を生成
            Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();         // Rigidbody2Dを取得
            rbody.AddForce(Vector2.down * fireSpeed, ForceMode2D.Impulse);    // 下方向に力を加えて発射

    }

    // Webに触れた瞬間に呼ばれる処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Web"))
        {
            stopByWeb = true;                 // 発射を停止
            Debug.Log("Webに触れた → 発射停止");
        }
    }



  }