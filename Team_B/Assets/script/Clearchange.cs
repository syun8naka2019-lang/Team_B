<<<<<<< HEAD

using System.Collections;
=======
>>>>>>> 0ef132ce225af06552d0bc802f3ac1f540cd7a66
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearController : MonoBehaviour
{
<<<<<<< HEAD
    int hp = 3;
    public string sceneName;
=======
    public string sceneName;

    int hp = 10;

    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);

            if (Application.CanStreamedLevelBeLoaded(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError($"Scene '{sceneName}' ‚ª Build Settings ‚É“o˜^‚³‚ê‚Ä‚¢‚Ü‚¹‚ñB");
            }
        }
    }

>>>>>>> 0ef132ce225af06552d0bc802f3ac1f540cd7a66
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Web"))
        {
            hp--;
<<<<<<< HEAD

        }
        if(hp <= 0)
        {
            SceneManager.LoadScene(sceneName);
=======
>>>>>>> 0ef132ce225af06552d0bc802f3ac1f540cd7a66
        }
    }
}

