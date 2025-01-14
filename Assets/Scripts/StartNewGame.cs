using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewGame : MonoBehaviour
{
    public string firstLevelName = "LevelScene"; // The name of the first level or the starting scene

    public void ResetProgress()
    {
        
        PlayerPrefs.DeleteAll();

        PlayerPrefs.Save();

        Debug.Log("Game progress reset! Starting from the first level.");

        // Optionally load the first level or menu
        SceneManager.LoadScene(firstLevelName);
    }
}
