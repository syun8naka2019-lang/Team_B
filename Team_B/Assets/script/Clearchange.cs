using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clearscene : MonoBehaviour
{
    public string sceneName;
    private float countdownTime = 0.0f; // カウントダウン開始時間（秒）

    float times = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
   
    void Update()
    {
        times += Time.deltaTime;
        countdownTime = times;
    
        if (countdownTime < 27 ) //時間設定
        {
            Debug.Log("残り時間 (秒): " + countdownTime);
        }
        else
        {
            countdownTime = 60.0f;
            SceneManager.LoadScene(sceneName);
            Debug.Log("タイムアップ!");

        }

    }

    
}
