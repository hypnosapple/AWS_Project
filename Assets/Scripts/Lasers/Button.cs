using UnityEngine;

public class Button : MonoBehaviour
{
    [Header("Laser Group")]
    public GameObject[] lasers; // Array to hold all lasers controlled by this button

    public GameObject FtoInteract;

    private bool playerNearby = false; // Track if the player is near the button

    private void Update()
    {
        // Check if the player is nearby and presses the F key
        if (playerNearby && Input.GetKeyDown(KeyCode.F))
        {
            ToggleLasers(); // Toggle the state of all lasers
        }
    }

    private void ToggleLasers()
    {
        // Iterate through the laser array and toggle each laser's state
        foreach (GameObject laser in lasers)
        {
            if (laser != null) // Ensure the laser reference is valid
            {
                bool isActive = laser.activeSelf;
                laser.SetActive(!isActive); // Toggle the laser's active state
                Debug.Log(laser.name + (isActive ? " deactivated." : " activated."));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detect if the player enters the button's trigger area
        if (collision.CompareTag("Player"))
        {
            playerNearby = true;
            FtoInteract.SetActive(true);
            Debug.Log("Player is near the button.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Detect if the player leaves the button's trigger area
        if (collision.CompareTag("Player"))
        {
            playerNearby = false;
            FtoInteract.SetActive(false);
            Debug.Log("Player left the button area.");
        }
    }
}
