using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebController : MonoBehaviour
{
    public float speed = 6.0f; //�ړ����xspeed
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    //Update is called once per frame
    void Update()
    {
     
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag== ("Web_stop"))
        {
            rb.velocity = new Vector2(0, 0);
            Debug.Log("正常でっせー");
        }
    }

    private void OnBecameInvisible()//�ǂ̃J�����ɂ��f��Ȃ��Ƃ�
    {
        Destroy(gameObject); //�I�u�W�F�N�g������
    }
}