using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController_M : MonoBehaviour
{
    public GameObject objPrefab;        //発生させるPrefabデータ
    public float delayTime = 0.5f;      //遅延時間
    public float fireSpeed = 4.0f;      //発射速度

    Transform gateTransform;
    float passedTimes = 0;              //経過時間

    void Start()
    {
        //発射口オブジェクトのTransformを取得
        gateTransform = transform.Find("gate_m");
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
}