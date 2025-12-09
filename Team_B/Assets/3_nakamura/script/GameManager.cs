using System.Collections;

using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;
    public string sceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerCon.gameState == "gameover")
        {
            // ゲームオーバー
            mainImage.SetActive(true);      // 画像を表示する
            SceneManager.LoadScene(sceneName);
        }
    }
}
