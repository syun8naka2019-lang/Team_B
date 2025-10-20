using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RPG_Controller : MonoBehaviour
{
    public float speed = 7.0f; //移動速度speed
    public Vector2 direction = Vector2.left;//左方向に進む
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

    private void OnBecameInvisible()//どのカメラにも映らないとき
    {
        Destroy(gameObject); //オブジェクトを消去
    }
}
