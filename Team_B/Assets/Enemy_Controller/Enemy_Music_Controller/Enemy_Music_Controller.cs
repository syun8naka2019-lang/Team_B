using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Music_Controller : MonoBehaviour
{
    public float speed = 3.0f; //移動速度speed
    public Vector2 direction = Vector2.left;//左方向に進む

    //Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()//どのカメラにも映らないとき
    {
        Destroy(gameObject); //オブジェクトを消去
    }
}
