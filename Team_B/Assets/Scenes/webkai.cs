using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class webkai : MonoBehaviour
{
    public float speed = 6.0f; //
    private Rigidbody2D rb;
    private float countdownTime = 0.0f; // カウントダウン開始時間（秒）

    float times = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        times += Time.deltaTime;
        countdownTime = times;
        if (countdownTime < 6) //時間設定
        {
            Debug.Log("残り時間 (秒): " + countdownTime);


        }
        else
        {

            Destroy(this.gameObject); //
            countdownTime = 0.0f;
        }


    }
    //Update is called once per frame
    void Update()
    {


    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Web_stop"))
        {
            rb.linearVelocity = new Vector2(0, 0);
            Debug.Log("正常でっせー");

       


         
            
           


        }



    }

    private void OnBecameInvisible()//
    {
        Destroy(gameObject); //
    }
}