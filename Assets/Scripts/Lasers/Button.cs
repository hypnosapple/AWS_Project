using UnityEngine;

public class Button : MonoBehaviour
{
    [Header("Laser Group")]
    public GameObject[] lasers; // Array to hold all lasers controlled by this button

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player lands on the button
        if (collision.collider.CompareTag("Player"))
        {
            DeactivateButtonAndLasers(); // Deactivate button and lasers
        }
    }

    private void DeactivateButtonAndLasers()
    {
        // Deactivate the button (make it visually disappear)
        gameObject.SetActive(false);

        // Iterate through the laser array and deactivate each laser
        foreach (GameObject laser in lasers)
        {
            if (laser != null) // Ensure the laser reference is valid
            {
                laser.SetActive(false); // Deactivate the laser
                Debug.Log(laser.name + " deactivated.");
            }
        }
    }
}
