using UnityEngine;

public class MovingTile : MonoBehaviour
{
    public Transform pointA; 
    public Transform pointB; 
    public float speed = 2.0f;

    private Transform target; // current target point

    void Start()
    {
        target = pointB;
    }

    void Update()
    {
        // Move the tile toward the target point
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Switch target when reaching the current one
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            target = target == pointA ? pointB : pointA;
        }
    }
}
