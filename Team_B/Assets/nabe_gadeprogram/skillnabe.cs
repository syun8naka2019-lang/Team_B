using UnityEngine;

public class skillnabe    : MonoBehaviour
{
    Animator animator;
    public string stop0 = "gade0";
    public string stop1 = "gade1";
    public string stop2 = "gade2";
    public string stop3 = "gade3";
    public string stop4 = "gade4";
    public string stop10 = "stopstop";
    string nowAnime = "";
    string oldAnime = "";
    int cnt = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nowAnime = stop1;
        animator.Play(nowAnime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
