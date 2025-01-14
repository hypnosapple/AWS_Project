using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoMenu : MonoBehaviour
{
    public string MenuScene;
    public void Backtomenu()
    {
        // Stop the BGM before transitioning to the menu
        BGMmanager.instance.StopBGM();
        SceneManager.LoadScene(MenuScene);
    }
}
