using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCon : MonoBehaviour
{
    public static string gameState = "playing";
    public string sceneName;

    private PlayerStatus status;

    private void Start()
    {
        status = GetComponent<PlayerStatus>();
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
        if (collision.CompareTag("Dead") || collision.CompareTag("Enemy"))
        {
<<<<<<< HEAD
            Debug.Log("ゲームオーバー");
            Destroy(this.gameObject);
           
            SceneManager.LoadScene(sceneName);

        }


        if (collision.gameObject.tag == "item")
        {

            nowAnime = stop1;
            animator.Play(nowAnime);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("ゲームオーバー");
            Destroy(this.gameObject);

            SceneManager.LoadScene(sceneName);
        }
            if (collision.gameObject.tag == "item")
            {
                cnt++;
                if (cnt == 0)
                    nowAnime = stop3;
                else if (cnt == 1)
                    nowAnime = stop3;
                else if (cnt == 2)
                    nowAnime = stop1;
                else if (cnt == 3)
                    nowAnime = stop2;
                else if (cnt == 4)
                    nowAnime = stop3;
                else if (cnt == 5)
                {
                    nowAnime = stop4;
                    cnt = 0;
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
            //シーンを読み込む
  

    
    // rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

     }
 
    // プレイヤーを移動させる
    private void Move()
    {
        // キーの入力値を取得
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float y = Input.GetAxis("Vertical") * moveSpeed;

        // 取得した入力値をプレイヤーの位置に反映させる
        transform.position += new Vector3(x, y, 0) * Time.deltaTime;
=======
            Destroy(gameObject);
            SceneManager.LoadScene(sceneName);
        }
>>>>>>> 5cd53f27439e8d2c9edca2dfd05cc662b0127680
    }

}
