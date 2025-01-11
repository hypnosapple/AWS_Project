using UnityEngine;

public class FallingTile : MonoBehaviour
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
        Destroy(gameObject, 2.0f); 
    }
}
