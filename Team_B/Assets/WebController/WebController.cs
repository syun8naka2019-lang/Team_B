using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebController : MonoBehaviour
{
    public float speed = 6.0f; //移動速度speed

    //Update is called once per frame
    void Update()
    {
     
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag== ("Web_stop"))
        {
            speed = 0.0f;  // 速度をゼロに
            Debug.Log("正常に作動");
        }
    }
    private void OnBecameInvisible()//どのカメラにも映らないとき
    {
        Destroy(gameObject); //オブジェクトを消去
    }
}
