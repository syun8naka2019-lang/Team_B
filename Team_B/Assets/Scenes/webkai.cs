using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class webkai : MonoBehaviour
{
    public float speed = 6.0f; //
    private Rigidbody2D rb;
    private float countdownTime = 0.0f; // �J�E���g�_�E���J�n���ԁi�b�j

    float times = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        times += Time.deltaTime;
        countdownTime = times;
        if (countdownTime < 6) //���Ԑݒ�
        {
            Debug.Log("�c�莞�� (�b): " + countdownTime);


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
            Debug.Log("����ł����[");

       


         
            
           


        }



    }

    private void OnBecameInvisible()//
    {
        Destroy(gameObject); //
    }
}