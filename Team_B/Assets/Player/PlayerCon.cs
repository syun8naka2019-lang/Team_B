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

    void Update()
    {

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
