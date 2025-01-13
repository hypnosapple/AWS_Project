using UnityEngine;

public class Key : MonoBehaviour
{
    public int levelIndex; // The level this key is associated with
    public LevelButton levelButton; // Reference to the corresponding LevelButton

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Save the key collection state
            PlayerPrefs.SetInt("Key" + levelIndex, 1);
            PlayerPrefs.Save();

            // Update the LevelButton key sprite
            if (levelButton != null)
            {
                levelButton.UpdateKeySprite();
            }

            // Destroy the key GameObject
            Destroy(gameObject);
        }
    }
}
