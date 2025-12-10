using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_con : MonoBehaviour
{
    public float speed = 1.0f; //ˆÚ“®‘¬“xspeed
    public Vector2 direction = Vector2.left;//¶•ûŒü‚Éi‚Ş

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
  
}