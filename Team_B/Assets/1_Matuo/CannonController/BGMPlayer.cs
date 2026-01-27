using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public AudioClip bgmClip;     // ó¨ÇµÇΩÇ¢BGM
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true;      // ÉãÅ[Évçƒê∂ON
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        audioSource.Play();
    }
}
