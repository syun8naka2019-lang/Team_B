using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebController : MonoBehaviour
{
    public float speed = 6.0f; //�ړ����xspeed

    //Update is called once per frame
    void Update()
    {
     
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag== ("Web_stop"))
        {
            speed = 0.0f;  // ���x���[����
            Debug.Log("����ɍ쓮");
        }
    }
    private void OnBecameInvisible()//�ǂ̃J�����ɂ��f��Ȃ��Ƃ�
    {
        Destroy(gameObject); //�I�u�W�F�N�g������
    }
}
