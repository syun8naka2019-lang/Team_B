using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCon : MonoBehaviour
{
    public static string gameState = "playing";
    public string sceneName;

    private PlayerStatus status;      // PlayerStatus
    private PlayerStatus_M statusM;   // PlayerStatus_M
    private Hp playerHp;

    void Start()
    {
        // PlayerStatus の取得
        status = GetComponent<PlayerStatus>();
        statusM = GetComponent<PlayerStatus_M>();

        if (status == null && statusM == null)
        {
            Debug.LogError("PlayerStatus も PlayerStatus_M も Player にアタッチされていません！");
        }

        //HP管理の取得（コメントアウト部分を必要なら有効化）
        playerHp = GetComponent<Hp>();
        if (playerHp == null)
        {
            Debug.LogError("Hp スクリプトが Player にアタッチされていません！");
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dead") || collision.CompareTag("Enemy"))
        {
            if (playerHp != null)
            {
                playerHp.TakeDamage(1);
            }
        }
    }
}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//
//public class Player_pic : MonoBehaviour
//{
//    public static string gameState = "playing";
//    public string sceneName;
//
//    private PlayerStatus status;
//    private Hp playerHp;  // ←ここは実際のクラス名に合わせて！
//
//    void Start()
//    {
//        // PlayerStatus の取得チェック
//        status = GetComponent<PlayerStatus>();
//        if (status == null)
//        {
//            Debug.LogError("PlayerStatus が Player にアタッチされていません！");
//        }
//
//        // HP管理の取得チェック
//        playerHp = GetComponent<Hp>(); // ←または PlayerHp
//        if (playerHp == null)
//        {
//            Debug.LogError("Hp（または PlayerHp） スクリプトが Player にアタッチされていません！");
//        }
//    }
//
 //   void Update()
 //   {
 //       Move();
 //   }
 //
 //   private void Move()
 //   {
 //       if (status == null) return;  // ← Null 防止
 //
 //       float x = Input.GetAxis("Horizontal") * status.moveSpeed;
 //       float y = Input.GetAxis("Vertical") * status.moveSpeed;
 //
 //       transform.position += new Vector3(x, y, 0) * Time.deltaTime;
 //   }
 //
 //   void OnTriggerEnter2D(Collider2D collision)
 //   {
 //
 //   }
//}
