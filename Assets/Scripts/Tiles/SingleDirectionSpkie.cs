using UnityEngine;

public class SingleDirectionSpike : MonoBehaviour
{
    public Transform pointB; // The target point
    public float speed = 2.0f; // Movement speed
    public float stopThreshold = 0.1f; // Distance threshold to determine when to stop

    private bool isMoving = true; // Flag to determine if the object should keep moving

    void Update()
    {
        if (isMoving)
        {
            // Move the object toward point B
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);

            // Check if the object is close enough to point B
            if (Vector2.Distance(transform.position, pointB.position) <= stopThreshold)
            {
                isMoving = false; // Stop movement
                Debug.Log("Reached point B, stopped moving.");
            }
        }
    }
}
