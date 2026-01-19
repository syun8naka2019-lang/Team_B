using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kキー専用のショット音スクリプト
public class ShotSoundK : MonoBehaviour
{
    // 再生する効果音（Inspectorから設定）
    public AudioClip sound;

    // AudioSourceを入れる変数
    AudioSource audioSource;

    // 最後に音を鳴らした時間
    float lastPlayTime = -1f;

    // クールタイム（1秒）
    float coolTime = 1f;

    void Start()
    {
        // AudioSourceを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Kキーが押されているかチェック
        if (Input.GetKey(KeyCode.K))
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
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//// ショット音を管理するクラス
//public class shotsound : MonoBehaviour
//{
//    // 再生する効果音（Inspectorから設定）
//    public AudioClip sound1; // Kキーで鳴る音
//    public AudioClip sound2; // Lキーで鳴る音

//    // AudioSourceを入れる変数
//    AudioSource audioSource;

//    // 最後に音を鳴らした時間を記録する変数
//    float lastPlayTimeK = -2f; // Kキー用（最初から鳴らせるように-2）
//    float lastPlayTimeL = -2f; // Lキー用（同上）

//    // 音が鳴らなくなる時間（クールタイム）
//    float coolTime = 1f;       // 1秒間は再生できない

//    void Start()
//    {
//        // このオブジェクトに付いているAudioSourceコンポーネントを取得
//        audioSource = GetComponent<AudioSource>();
//    }

//    void Update()
//    {
//        // ===== Kキーが押されているかチェック =====
//        if (Input.GetKey(KeyCode.K))
//        {
//            // 前回Kキーの音を鳴らしてから2秒以上経っているか
//            if (Time.time - lastPlayTimeK >= coolTime)
//            {
//                // sound1を再生
//                audioSource.PlayOneShot(sound1);

//                // 今の時間を「最後に鳴らした時間」として保存
//                lastPlayTimeK = Time.time;
//            }
//        }

//        // ===== Lキーが押されているかチェック =====
//        if (Input.GetKey(KeyCode.L))
//        {
//            // 前回Lキーの音を鳴らしてから2秒以上経っているか
//            if (Time.time - lastPlayTimeL >= coolTime)
//            {
//                // sound2を再生
//                audioSource.PlayOneShot(sound2);

//                // 今の時間を保存
//                lastPlayTimeL = Time.time;
//            }
//        }
//    }
//}
