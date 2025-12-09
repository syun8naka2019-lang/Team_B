using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nabe : MonoBehaviour
{
    public GameObject objPrefab;        //発生させるPrefabデータ
    public float delayTime = 1f;      //遅延時間
    public float fireSpeed = 4.0f;      //発射速度

    Transform gateTransform;
    float passedTimes = 0;              //経過時間

    void Start()
    {
        //発射口オブジェクトのTransformを取得
        gateTransform = transform.Find("playergate");
    }

    private void Update()
    {
        // Prefabが失われていないか確認
        if (objPrefab == null)
        {
            Debug.LogError("objPrefab が破壊されています！InspectorでPrefabを再設定してください。");
            return;
        }

        passedTimes += Time.deltaTime;
        if (passedTimes >= 1f)
        {
            if (Input.GetKey(KeyCode.I))
            {
                Vector2 pos = new Vector2(gateTransform.position.x, gateTransform.position.y);
                //弾生成
                GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);
                //砲弾が向いている方向に発射
                Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
                Vector2 v = new Vector2(0, 1) * fireSpeed;
                rbody.AddForce(v, ForceMode2D.Impulse);
                passedTimes = 0;
            }
        }

    }
}