using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clearchange : MonoBehaviour
{
    public string sceneName;

    int hp = 3;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
   
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(sceneName);

        }


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Web")
        {
            hp--;
        }
      
    }

}
