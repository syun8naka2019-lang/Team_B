using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCon : MonoBehaviour
{
    public static string gameState = "playing";
    public string sceneName;

    private PlayerStatus status;
    private Hp playerHp; // ← 追加


    int currentHp = 5;
    private void Start()
    {
        status = GetComponent<PlayerStatus>();
        playerHp = GetComponent<Hp>(); // PlayerHp を取得
        if (playerHp == null)
        {
            Debug.LogError("PlayerHp スクリプトがアタッチされていません！");


        }
    }
        void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal") * status.moveSpeed;
        float y = Input.GetAxis("Vertical") * status.moveSpeed;

        transform.position += new Vector3(x, y, 0) * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 何かに当たったとき
        if (collision.CompareTag("Dead") || collision.CompareTag("Enemy"))
        {
           
                playerHp.TakeDamage(1); // PlayerHp の TakeDamage を呼ぶ
          
        }
    }

    public void Goal()
    {
        gameState = "gameclear";
    }

    public void GameOver()
    {
        gameState = "gameover";
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
