using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_con : MonoBehaviour
{
    public float speed = 1.0f; //�ړ����xspeed
    public Vector2 direction = Vector2.left;//�������ɐi��

    //Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Web")
        {
            speed = 0.0f;
        }
    }
    private void OnBecameInvisible()//�ǂ̃J�����ɂ��f��Ȃ��Ƃ�
    {
        Destroy(gameObject); //�I�u�W�F�N�g������
    }
}