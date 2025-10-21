using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController_R : MonoBehaviour
{
    public float deleteTime = 3.0f;     //削除する時間指定

    //Update is called once per frame
    void Start()
    {
        Destroy(gameObject, deleteTime);     //削除設定
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); //オブジェクトを消去
    }

}