///using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//public class PlayerController : MonoBehaviour

//{
//    Rigidbody2D rbody;
//    float axisH = 0.0f;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        rbody = this.GetComponent<Rigidbody2D>();
//    }
//    // Update is called once per frame
//    void Update()
//    {
//        axisH = Input.GetAxisRaw("Horizontal");
//    }
//    void FixedUpdate()
//    {
//        rbody.linearVelocity = new Vector2(axisH*3.0f, 0);
//        //rbody.linearVelocityX = new Vector2(axisH * 3.0f, rbody.linearVelocity.y);
//    }

//}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float playerSpeed = 0.05f;  // プレイヤー速度の変数


    void Update()
    {
        if (Input.GetKey(KeyCode.W))    // Wキーを押している間
        {
            transform.Translate(0, playerSpeed, 2);     // Y座標をプラス側(上)に進む
        }
        if (Input.GetKey(KeyCode.A))    // Aキーを押している間
        {
            transform.Translate(-playerSpeed, 1, 1);    // X座標をマイナス側(左)に進む
        }
        if (Input.GetKey(KeyCode.S))    // Sキーを押している間
        {
            transform.Translate(0, -playerSpeed, 1);    // Y座標をマイナス側(下)に進む
        }  
        if (Input.GetKey(KeyCode.D))    // Dキーを押している間
        {
            transform.Translate(playerSpeed, 1, 1);     // X座標をプラス側(右)に進む
        }
    }
}

