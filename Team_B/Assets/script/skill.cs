using UnityEngine;

public class skill : MonoBehaviour
{
    private Animator anim = null;


    private float countdownTime = 10.0f; // カウントダウン開始時間（秒）
    private void Start()
    {
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        float remainingTime = countdownTime - Time.time;
        if (remainingTime > 0)
        {
            Debug.Log("残り時間 (秒): " + remainingTime);
        }
        else
        {
            Debug.Log("タイムアップ!");
            anim.SetBool("New Bool", true);
        }
    }



}