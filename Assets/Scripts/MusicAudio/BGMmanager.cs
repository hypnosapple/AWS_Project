using UnityEngine;

public class BGMmanager : MonoBehaviour
{
    public AudioSource bgmSource; 
    public static BGMmanager instance; 
    private string currentSceneName; 
    private bool isContinuingBetweenLevels = false; 

    void Awake()
    {
        // Ensure only one instance of BGMmanager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        // Start playing the BGM if it's not already playing
        if (!bgmSource.isPlaying)
        {
            bgmSource.Play();
        }
    }

    void Update()
    {
        // Detect scene changes
        string newSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (newSceneName != currentSceneName)
        {
            HandleSceneChange(newSceneName);
            currentSceneName = newSceneName;
        } else if (newSceneName == "Level_Five")
        {
            StopBGM();
        }
    }

    private void HandleSceneChange(string newSceneName)
    {
        if (IsLevelScene(newSceneName))
        {
            if (!isContinuingBetweenLevels) 
            {
                RestartBGM(); 
            }
            isContinuingBetweenLevels = true; 
        }
        else
        {

            isContinuingBetweenLevels = false;
        }
    }

    private bool IsLevelScene(string sceneName)
    {
        
        return sceneName == "Level_One" || sceneName == "Level_Two" || sceneName == "Level_Three" || sceneName == "Level_Four";
    }

    private void RestartBGM()
    {
        bgmSource.Stop();
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }
}
