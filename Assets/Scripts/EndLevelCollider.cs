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
            // Unlock the current level
            PlayerPrefs.SetInt("Level" + currentLevelIndex, 1);
            PlayerPrefs.Save();
            Debug.Log("Level " + currentLevelIndex + " completed and unlocked!");

            // Check if there is a next level to load
            if (!string.IsNullOrEmpty(nextLevelName))
            {
                Debug.Log("Loading next level: " + nextLevelName);
                SceneManager.LoadScene(nextLevelName); // Load the next level
            }
            else
            {
                Debug.Log("No next level specified. Level complete!");
            }
        }
    }
}
