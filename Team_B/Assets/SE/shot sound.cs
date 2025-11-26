using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotsound : MonoBehaviour
{

    public AudioClip sound1;
    public AudioClip sound2;

    AudioSource audioSource;

    void Start()
    {
        //ComponentÇéÊìæ
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ç∂
        if (Input.GetKey(KeyCode.K))
        {
            audioSource.PlayOneShot(sound1);
        }
        // âE
        if (Input.GetKey(KeyCode.L))
        {
            audioSource.PlayOneShot(sound2);
        }
    }

}