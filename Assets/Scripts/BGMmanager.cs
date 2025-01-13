using UnityEngine;
using UnityEngine.Audio;

public class BGMmanager : MonoBehaviour
{
    public AudioSource bgmSource; // Reference to the AudioSource for BGM
    public static BGMmanager instance;

/*    void Start()
    {
        BGMmanager.instance.PlayBGM(bgmSource.clip);
    }
*/

    void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist this GameObject across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip != clip)
        {
            bgmSource.clip = clip;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }
}
