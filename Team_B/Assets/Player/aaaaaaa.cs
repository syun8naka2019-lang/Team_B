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

public class Player : MonoBehaviour
{
    // プレイヤーの移動速度
    private int moveSpeed = 5;

    void Update()
    {
        Move();
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