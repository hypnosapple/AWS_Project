using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoMenu : MonoBehaviour
{
    public string Menu;
    public void Backtomenu()
    {
        SceneManager.LoadScene(Menu);
    }
}
