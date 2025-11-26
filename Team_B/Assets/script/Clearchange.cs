
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearController : MonoBehaviour
{


   
    public string sceneName;

  

    int hp = 10;

              // 敵のHP（必要なら変更可能）

    void Update()
    {
        // HP が 0 以下 → 自身を削除してシーン遷移

        if (hp <= 0)
        {
            Destroy(gameObject);

            if (Application.CanStreamedLevelBeLoaded(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError($"Scene '{sceneName}' が Build Settings に登録されていません。");
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        // Web に当たったら HP 減少
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
