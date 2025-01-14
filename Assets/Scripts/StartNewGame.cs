using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartNewGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string firstLevelName = "LevelScene"; // The name of the first level or the starting scene
    private Vector3 originalScale; // To store the original scale of the button
    public float hoverScale = 1.1f; // Scale multiplier on hover

    void Start()
    {
        // Save the original scale of the button
        originalScale = transform.localScale;
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("Game progress reset! Starting from the first level.");

        // Load the first level or menu
        SceneManager.LoadScene(firstLevelName);
    }

    // Triggered when the mouse enters the button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Enlarge the button
        transform.localScale = originalScale * hoverScale;
    }

    // Triggered when the mouse exits the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        // Reset to the original scale
        transform.localScale = originalScale;
    }
}
