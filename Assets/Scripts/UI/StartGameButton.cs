using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartGameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string LevelScene;

    private Vector3 originalScale; // Store the original scale of the button
    public float hoverScale = 1.1f; // Scale multiplier for hover effect

    void Start()
    {
        // Save the original scale of the button
        originalScale = transform.localScale;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(LevelScene);
    }

    public void ExitGame()
    {
        // Log message for testing in the Unity Editor
        Debug.Log("Exiting game...");

        // Close the application
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Enlarge the button on hover
        transform.localScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Reset the button scale
        transform.localScale = originalScale;
    }
}
