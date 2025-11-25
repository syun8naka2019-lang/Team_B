
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearController : MonoBehaviour
{
    int hp = 3;
    public string sceneName;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Web"))
        {
            hp--;

        }
        if(hp <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

