using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartGameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string LevelScene;

    private Vector3 originalPosition; // Store the original position of the button
    public float hoverOffset = 10f; // Offset to move the button up on hover

    void Start()
    {
        // Save the original position of the button
        originalPosition = transform.position;
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
        // Move the button up slightly when hovered over
        transform.position = originalPosition + new Vector3(0, hoverOffset, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Reset the button's position when the pointer exits
        transform.position = originalPosition;
    }
}
