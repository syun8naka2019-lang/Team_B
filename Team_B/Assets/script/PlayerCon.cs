using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCon : MonoBehaviour
{
    public static string gameState = "playing";
    public string sceneName;
    // プレイヤーの移動速度
    private int moveSpeed = 7;

    Animator animator;
    public string stop0 = "gade0";
    public string stop1 = "gade1";
    public string stop2 = "gade2";
    public string stop3 = "gade3";
    public string stop4 = "gade4";
    public string stop10 = "stopstop";
    string nowAnime = "";
    string oldAnime = "";
    int cnt = 0;


    private void Start()
    {
        animator = GetComponent<Animator>();
        nowAnime = stop10;
        oldAnime = stop10;
       
    }
    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("ゲームオーバー");

        if (collision.gameObject.tag == "Dead")
        {
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
    }

}

