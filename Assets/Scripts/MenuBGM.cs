using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBGM : MonoBehaviour
{
    public AudioSource menubgm;
    public static MenuBGM instance;

    void Awake()
    {
        // Ensure only one instance of BGMmanager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Subscribe to the SceneManager.sceneLoaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Start playing the BGM if it's not already playing
        if (!menubgm.isPlaying)
        {
            menubgm.Play();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu") 
        {
            
            menubgm.Play();
        }
    }

    public void StopBGM()
    {
        menubgm.Stop();
    }

    private void OnDestroy()
    {
        // Unsubscribe from the SceneManager.sceneLoaded event to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
