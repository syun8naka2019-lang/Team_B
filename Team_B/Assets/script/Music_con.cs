using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_con : MonoBehaviour
{
    public float speed = 1.0f; //移動速度speed
    public Vector2 direction = Vector2.left;//左方向に進む

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
    private void OnBecameInvisible()//どのカメラにも映らないとき
    {
        Destroy(gameObject); //オブジェクトを消去
    }
}