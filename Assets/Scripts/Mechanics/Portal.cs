using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform linkedPortal; // Reference to the linked portal's transform
    public float cooldownTime = 1.0f; // Cooldown time to prevent instant re-teleportation

    private bool isOnCooldown = false; // Tracks if the current portal is on cooldown

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the portal
        if (collision.CompareTag("Player") && !isOnCooldown)
        {
            // Teleport the player to the linked portal's position
            collision.transform.position = linkedPortal.position;

            // Start cooldown for both this portal and the linked portal
            StartCoroutine(PortalCooldown());
            if (linkedPortal.GetComponent<Portal>() != null)
            {
                linkedPortal.GetComponent<Portal>().ActivateCooldown();
            }
        }
    }

    private System.Collections.IEnumerator PortalCooldown()
    {
        isOnCooldown = true; // Set this portal's cooldown
        yield return new WaitForSeconds(cooldownTime); // Wait for cooldown duration
        isOnCooldown = false; // Reset cooldown
    }

    public void ActivateCooldown()
    {
        StartCoroutine(PortalCooldown()); // Trigger cooldown externally
    }
}
