using UnityEngine;
using UnityEngine.SceneManagement;

public class Clearchange : MonoBehaviour
{
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Web"))
        {
            hp--;
        }
    }
}
