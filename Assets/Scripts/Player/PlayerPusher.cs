using UnityEngine;

public class PlayerPusher : MonoBehaviour
{
    public float pushForce = 10f; // Adjust the force as needed

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Check if the object is pushable
        if (collision.gameObject.CompareTag("Pushable"))
        {
            Rigidbody2D objectRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (objectRb != null && objectRb.bodyType == RigidbodyType2D.Dynamic)
            {
                // Calculate push direction based on collision normal
                Vector2 pushDirection = collision.contacts[0].normal * -1;

                // Apply force to the object
                objectRb.AddForce(pushDirection * pushForce, ForceMode2D.Force);
            }
        }
    }
}
