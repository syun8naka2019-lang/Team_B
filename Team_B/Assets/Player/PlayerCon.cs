using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCon : MonoBehaviour
{
    public static string gameState = "playing";
    public string sceneName;

    private PlayerStatus status;
    private Hp playerHp;

    void Start()
    {
        // PlayerStatus の取得
        status = GetComponent<PlayerStatus>();
        if (status == null)
        {
            Debug.LogError("PlayerStatus が Player にアタッチされていません！");
        }

        // HP管理の取得
      /*  playerHp = GetComponent<Hp>();
        if (playerHp == null)
        {
            Debug.LogError("Hp スクリプトが Player にアタッチされていません！");
        }*/
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (status == null) return;

        float x = Input.GetAxis("Horizontal") * status.moveSpeed;
        float y = Input.GetAxis("Vertical") * status.moveSpeed;

        transform.position += new Vector3(x, y, 0) * Time.deltaTime;
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
