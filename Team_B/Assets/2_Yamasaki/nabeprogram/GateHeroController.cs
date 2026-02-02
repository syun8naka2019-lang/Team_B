using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateHeroController : MonoBehaviour
{
    public GameObject objPrefab;     // 弾Prefab
    public float delayTime = 1f;     // 発射間隔
    public float fireSpeed = 4.0f;   // 発射速度
    public float angle = 30f;        // 左右の角度

    Transform gateTransform;
    float passedTimes = 0;

    void Start()
    {
        // 発射口（gate_hero）を取得
        gateTransform = transform.Find("gate_hero");
    }

    void Update()
    {
        if (objPrefab == null)
        {
            Debug.LogError("objPrefab が設定されていません！");
            return;
        }

        passedTimes += Time.deltaTime;

        if (passedTimes >= delayTime)
        {
            Fire3Way();
            passedTimes = 0f;
        }
    }

    void Fire3Way()
    {
        Vector2 pos = gateTransform.position;

        // 正面（下）
        Fire(Vector2.down, pos);

        // 左下
        Fire(Quaternion.Euler(0, 0, angle) * Vector2.down, pos);

        // 右下
        Fire(Quaternion.Euler(0, 0, -angle) * Vector2.down, pos);
    }

    void Fire(Vector2 direction, Vector2 pos)
    {
        GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);

        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
        if (rbody != null)
        {
            rbody.AddForce(direction.normalized * fireSpeed, ForceMode2D.Impulse);
        }
    }
}
