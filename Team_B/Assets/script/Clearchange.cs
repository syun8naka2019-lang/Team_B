using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clearscene : MonoBehaviour
{
    public string sceneName;
    private float countdownTime = 0.0f; // �J�E���g�_�E���J�n���ԁi�b�j

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
    
        if (countdownTime < 27 ) //���Ԑݒ�
        {
            Debug.Log("�c�莞�� (�b): " + countdownTime);
        }
        else
        {
            countdownTime = 60.0f;
            SceneManager.LoadScene(sceneName);
            Debug.Log("�^�C���A�b�v!");

        }

    }

    
}
