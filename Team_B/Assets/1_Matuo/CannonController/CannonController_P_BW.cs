using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController_P_BW : MonoBehaviour
{
    [Header("発射設定")]
    public GameObject bigWebPrefab;    // 発射する BigWeb の Prefab
    public float delayTime = 1f;       // 発射間隔
    public float fireSpeed = 4f;       // 発射速度

    private Transform gateTransform;   // 発射口
    private float passedTime = 0f;     // 経過時間

    void Start()
    {
        // 発射口オブジェクトの Transform を取得
        gateTransform = transform.Find("playergate");
        if (gateTransform == null)
        {
            Debug.LogError("playergate が見つかりません！Hierarchyに配置してください。");
        }
    }

    void Update()
    {
        if (bigWebPrefab == null) return; // Prefab がセットされているか確認

        passedTime += Time.deltaTime;

        if (passedTime >= delayTime)
        {
            // Kキーで発射
            if (Input.GetKey(KeyCode.K))
            {
                FireBigWeb();
                passedTime = 0f;
            }
        }
    }

    private void FireBigWeb()
    {
        if (gateTransform == null) return;

        // 発射位置
        Vector2 pos = gateTransform.position;

        // BigWeb 生成
        GameObject bigWeb = Instantiate(bigWebPrefab, pos, Quaternion.identity);

        // Rigidbody2D に力を加えて前方に発射
        Rigidbody2D rbody = bigWeb.GetComponent<Rigidbody2D>();
        if (rbody != null)
        {
            Vector2 direction = Vector2.up; // 上方向に発射（必要に応じて方向を変えられる）
            rbody.AddForce(direction * fireSpeed, ForceMode2D.Impulse);
        }
    }
}
