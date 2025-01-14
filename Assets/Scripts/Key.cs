using UnityEngine;

public class Key : MonoBehaviour
{
    public int levelIndex; // The level this key is associated with

    void Start()
    {
        // Check if the key has already been collected
        if (PlayerPrefs.GetInt("Key" + levelIndex, 0) == 1)
        {
            // Key already collected; destroy this object
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Save the key collection state
            PlayerPrefs.SetInt("Key" + levelIndex, 1);
            PlayerPrefs.Save();

            Debug.Log("Key for Level " + levelIndex + " collected!");

            // Destroy the key GameObject
            Destroy(gameObject);
        }
    }
}
