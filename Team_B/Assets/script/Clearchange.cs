<<<<<<< HEAD

using System.Collections;

=======
>>>>>>> 0fb69526b66a2bb27546f53eda0a77716bff54eb
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearController : MonoBehaviour
{
<<<<<<< HEAD

   
    public string sceneName;

  

    int hp = 10;

    void Update()
    {
=======
    public string sceneName;   // クリア後に読み込むシーン名
    int hp = 10;               // 敵のHP（必要なら変更可能）

    void Update()
    {
        // HP が 0 以下 → 自身を削除してシーン遷移
>>>>>>> 0fb69526b66a2bb27546f53eda0a77716bff54eb
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

<<<<<<< HEAD

=======
>>>>>>> 0fb69526b66a2bb27546f53eda0a77716bff54eb
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Web に当たったら HP 減少
        if (collision.CompareTag("Web"))
        {
            hp--;
<<<<<<< HEAD


        }
        if(hp <= 0)
        {
            SceneManager.LoadScene(sceneName);

=======
>>>>>>> 0fb69526b66a2bb27546f53eda0a77716bff54eb
        }
    }
}
