using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelCollider : MonoBehaviour
{
    public int currentLevelIndex; // The index of the current level
    public string nextLevelName; // The name of the next level's scene

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Unlock the next level
            int nextLevelIndex = currentLevelIndex + 1;
            PlayerPrefs.SetInt("Level" + nextLevelIndex, 1);
            PlayerPrefs.Save();
            Debug.Log("Level " + nextLevelIndex + " unlocked!");

            // Mark current level as completed (optional, in case needed for tracking)
            PlayerPrefs.SetInt("Level" + currentLevelIndex + "_Completed", 1);
            PlayerPrefs.Save();
            Debug.Log("Level " + currentLevelIndex + " marked as completed!");

            // Check if there is a next level to load
            if (!string.IsNullOrEmpty(nextLevelName))
            {
                Debug.Log("Loading next level: " + nextLevelName);
                SceneManager.LoadScene(nextLevelName); // Load the next level
            }
            else
            {
                Debug.Log("No next level specified. Returning to menu.");
                SceneManager.LoadScene("Menu"); // Replace "Menu" with your menu scene name
            }
        }
    }
}
