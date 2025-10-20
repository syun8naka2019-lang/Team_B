using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RPG_Controller : MonoBehaviour
{
    public float speed = 7.0f; //�ړ����xspeed
    public Vector2 direction = Vector2.left;//�������ɐi��
    float cout = 0;

    //Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        cout += Time.deltaTime;

        if (cout > 1)
        {
            direction =new Vector2(1,-1);
        }
    }

    private void OnBecameInvisible()//�ǂ̃J�����ɂ��f��Ȃ��Ƃ�
    {
        Destroy(gameObject); //�I�u�W�F�N�g������
    }
}
