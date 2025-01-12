using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public string LevelScene;

    public void LoadScene()
    {
        
            SceneManager.LoadScene(LevelScene);
       
    }
}
