using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class skillgade : MonoBehaviour
{
    public static string gameState = "playing";
    public string sceneName;

    // プレイヤーの移動速度
    private int moveSpeed = 7;

    Animator animator;

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

    private void Update()
    {
        Move();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dead") ||
            collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("ゲームオーバー");
            Destroy(this.gameObject);
            SceneManager.LoadScene(sceneName);
        }

        if (collision.gameObject.CompareTag("item"))
        {
            nowAnime = stop1;
            animator.Play(nowAnime);
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

    // プレイヤーを移動させる
    private void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float y = Input.GetAxis("Vertical") * moveSpeed;

        transform.position += new Vector3(x, y, 0) * Time.deltaTime;
    }
}
