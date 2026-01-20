
/*using UnityEngine;

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
}*/

using UnityEngine;

public class BreakSound : MonoBehaviour
{
    // 再生する効果音
    public AudioClip sound;

    // クールダウン時間（秒）
    public float cooldownTime = 1f;

    AudioSource audioSource;
    bool kPressed = false;
    float lastPlayTime = -999f; // 最後に再生した時間

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

        // KのあとにLキーを押したら再生（クールダウン確認）
        if (kPressed && Input.GetKeyDown(KeyCode.L))
        {
            if (Time.time - lastPlayTime >= cooldownTime)
            {
                audioSource.PlayOneShot(sound);
                lastPlayTime = Time.time;
            }

            kPressed = false; // リセット
        }
    }
}



