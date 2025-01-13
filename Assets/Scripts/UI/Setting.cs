using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public GameObject SettingPanel;
    public string MenuScene;

    public void OnSettingOpen()
    {
        SettingPanel.SetActive(true);
    }

    public void OnSettingClose()
    {
        SettingPanel.SetActive(false);
    }

    public void BacktoMenu()
    {
        // Stop the BGM before transitioning to the menu
        BGMmanager.instance.StopBGM();
        SceneManager.LoadScene(MenuScene);
    }
}
