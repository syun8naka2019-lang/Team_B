using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController_R : MonoBehaviour
{
    public float deleteTime = 3.0f;     //�폜���鎞�Ԏw��

    //Update is called once per frame
    void Start()
    {
        Destroy(gameObject, deleteTime);     //�폜�ݒ�
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); //�I�u�W�F�N�g������
    }

}