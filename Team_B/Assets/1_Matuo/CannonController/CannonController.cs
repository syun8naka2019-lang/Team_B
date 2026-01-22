//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CannonController_M : MonoBehaviour
//{
//    public GameObject objPrefab;
//    public float delayTime = 0.5f;
//    public float fireSpeed = 4.0f;

//    private float preFireOffset = 16.0f;  // カメラに入る手前の距離（上方向）

//    Transform gateTransform;
//    float passedTimes = 0;

//    Camera mainCam;
//    bool canFire = false;

//    void Start()
//    {
//        gateTransform = transform.Find("gate_m");
//        mainCam = Camera.main;

//        if (gateTransform == null)
//            Debug.LogError("CannonController_M: gate_m が見つかりません！");
//    }

//    void Update()
//    {
//        // 発射条件チェック
//        if (!canFire)
//        {
//            if (IsJustBeforeEnterCamera())
//            {
//                canFire = true; // ここで初めて発射開始
//            }
//            else
//            {
//                return; // まだ撃たない
//            }
//        }

//        // ここから発射処理
//        passedTimes += Time.deltaTime;

//        if (passedTimes > delayTime)
//        {
//            FireCannon();
//            passedTimes = 0;
//        }
//    }

//    // カメラに入る手前の位置をチェック
//    bool IsJustBeforeEnterCamera()
//    {
//        // カメラ上端
//        float camTop = mainCam.transform.position.y + mainCam.orthographicSize;

//        // 敵の位置が "カメラ上端 + preFireOffset" を下回ったら発射開始
//        return transform.position.y <= camTop + preFireOffset;
//    }

//    void FireCannon()
//    {
//        Vector2 pos = gateTransform.position;
//        GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);
//        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
//        rbody.AddForce(Vector2.down * fireSpeed, ForceMode2D.Impulse);
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController_R : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletPrefabA;
    public GameObject bulletPrefabB;

    [Header("Fire Settings")]
    public float delayTime = 0.5f;
    public float fireSpeed = 4.0f;

    [Header("Sound")]
    public AudioClip seA;
    public AudioClip seB;

    private float preFireOffset = 16.0f;

    Transform gateTransform;
    float passedTimes = 0;

    Camera mainCam;
    bool canFire = false;

    AudioSource audioSource;

    void Start()
    {
        gateTransform = transform.Find("gate_r");
        mainCam = Camera.main;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (gateTransform == null)
            Debug.LogError("CannonController_M: gate_m が見つかりません！");
    }

    void Update()
    {
        if (!canFire)
        {
            if (IsJustBeforeEnterCamera())
            {
                canFire = true;
            }
            else
            {
                return;
            }
        }

        passedTimes += Time.deltaTime;

        if (passedTimes > delayTime)
        {
            FireCannon();
            passedTimes = 0;
        }
    }

    bool IsJustBeforeEnterCamera()
    {
        float camTop = mainCam.transform.position.y + mainCam.orthographicSize;
        return transform.position.y <= camTop + preFireOffset;
    }

    void FireCannon()
    {
        Vector2 pos = gateTransform.position;

        // 0 or 1 をランダムで選択
        int rand = Random.Range(0, 2);

        GameObject bullet;
        AudioClip se;

        if (rand == 0)
        {
            bullet = bulletPrefabA;
            se = seA;
        }
        else
        {
            bullet = bulletPrefabB;
            se = seB;
        }

        GameObject obj = Instantiate(bullet, pos, Quaternion.identity);

        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
        rbody.AddForce(Vector2.down * fireSpeed, ForceMode2D.Impulse);

        // SE再生
        if (se != null)
        {
            audioSource.PlayOneShot(se);
        }
    }
}
