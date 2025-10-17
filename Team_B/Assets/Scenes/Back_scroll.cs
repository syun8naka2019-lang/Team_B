using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back_scroll : MonoBehaviour
{
    private float speed = 1;

    void Update()
    {
        transform.position -= new Vector3(0, Time.deltaTime * speed);

        if (transform.position.y <= -11)
        {
            transform.position = new Vector3(0, 21.1f);
        }
    }
}