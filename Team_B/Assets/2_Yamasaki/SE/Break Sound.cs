/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lキー専用のブレイク音スクリプト
public class BreakSound : MonoBehaviour
{
    // 再生する効果音（Inspectorから設定）
    public AudioClip sound;

    // AudioSourceを入れる変数
    AudioSource audioSource;

    // 最後に音を鳴らした時間
    float lastPlayTime = -1f;

    // クールタイム（1秒）
    float coolTime = 1.5f;

    void Start()
    {
        // AudioSourceを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Lキーが押されているかチェック
        if (Input.GetKey(KeyCode.L))
        {
            // クールタイムが終わっているか
            if (Time.time - lastPlayTime >= coolTime)
            {
                // 音を再生
                audioSource.PlayOneShot(sound);

                // 現在の時間を保存
                lastPlayTime = Time.time;
            }
        }
    }
}*/

using UnityEngine;

public class BreakSound : MonoBehaviour
{
    // 再生する効果音
    public AudioClip sound;

    AudioSource audioSource;
    bool kPressed = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Kキーを押したらフラグON
        if (Input.GetKeyDown(KeyCode.K))
        {
            kPressed = true;
        }

        // KのあとにLキーを押したら再生
        if (kPressed && Input.GetKeyDown(KeyCode.L))
        {
            audioSource.PlayOneShot(sound);
            kPressed = false; // リセット
        }

       
    }
}



