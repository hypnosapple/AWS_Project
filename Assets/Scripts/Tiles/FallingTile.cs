using CheckPoints;
using UnityEngine;

public class FallingTile : MonoBehaviour, IRespawnable
{
    private Rigidbody2D rb;
    private bool isActivated = false; 
    public float fallDelay;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isActivated)
        {
            isActivated = true; 
            Debug.Log("Player stepped on the tile: " + gameObject.name);
            Invoke("ActivatePhysics", fallDelay); 
        }
    }

    private void ActivatePhysics()
    {
        Debug.Log("Tile is falling: " + gameObject.name);
        rb.constraints = RigidbodyConstraints2D.None;
        Invoke("SetInactive", 2.0f);
        // Destroy(gameObject, 2.0f);
    }

    private void SetInactive()
    {
        // If the value of isActivated is false, the respawn logic is reached. Therefore, this function is not executed
        if (isActivated)
        {
            gameObject.SetActive(false); 
        }
    }

    public void ResetState()
    {
        isActivated = false;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll; 
    }
}
