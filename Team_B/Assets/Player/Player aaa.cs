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
    float playerSpeed = 0.05f;  // �v���C���[���x�̕ϐ�


    void Update()
    {
        if (Input.GetKey(KeyCode.W))    // W�L�[�������Ă����
        {
            transform.Translate(0, playerSpeed, 2);     // Y���W���v���X��(��)�ɐi��
        }
        if (Input.GetKey(KeyCode.A))    // A�L�[�������Ă����
        {
            transform.Translate(-playerSpeed, 1, 1);    // X���W���}�C�i�X��(��)�ɐi��
        }
        if (Input.GetKey(KeyCode.S))    // S�L�[�������Ă����
        {
            transform.Translate(0, -playerSpeed, 1);    // Y���W���}�C�i�X��(��)�ɐi��
        }  
        if (Input.GetKey(KeyCode.D))    // D�L�[�������Ă����
        {
            transform.Translate(playerSpeed, 1, 1);     // X���W���v���X��(�E)�ɐi��
        }
    }
}

