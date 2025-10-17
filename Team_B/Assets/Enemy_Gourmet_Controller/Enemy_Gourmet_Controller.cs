using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gourmet_Controller : MonoBehaviour
{
    public float speed = 6.0f; //�ړ����xspeed
    public Vector2 direction = new Vector2(-1,-1);//�������ɐi��

    //Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()//�ǂ̃J�����ɂ��f��Ȃ��Ƃ�
    {
        Destroy(gameObject); //�I�u�W�F�N�g������
    }
}
