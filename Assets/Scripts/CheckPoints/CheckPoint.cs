using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isFirstCheckpoint = false;
    private bool isActivated = false; // To track if this checkpoint has been marked

    private void Start()
    {
        if (isFirstCheckpoint)
        {
            CheckpointManager.Instance.RegisterFirstCheckpoint(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isActivated)
            {
                Debug.Log("Checkpoint activated: " + gameObject.name);
                isActivated = true;

                // Notify the checkpoint manager
                CheckpointManager.Instance.SetCheckpoint(this);
            }
        }
    }
}
