using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Changescene : MonoBehaviour
{
    public string sceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    //ƒV[ƒ“‚ğ“Ç‚İ‚Ş
    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}