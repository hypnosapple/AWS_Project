using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndLevelCollider : MonoBehaviour
{
    public int currentLevelIndex; // The index of the current level
    public string nextLevelName; // The name of the next level's scene
    public TextMeshProUGUI dialogText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Additional handling of level4
            if (currentLevelIndex == 3)
            {
                bool final = true;
                for (int i = 0; i < 4; i++)
                {
                    if (PlayerPrefs.GetInt("Key" + i, 0) != 1)
                    {
                        Debug.Log($"No collect Key {i + 1}");
                        final = false;
                        break;
                    }
                }

                // All keys are not collected
                if (!final)
                {
                    dialogText.gameObject.SetActive(true);
                    return;
                }
            }
            
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogText.gameObject.SetActive(false);
        }
    }
}
