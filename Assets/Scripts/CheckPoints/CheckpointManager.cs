using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance; // Singleton instance
    private Checkpoint firstCheckpoint; // The first checkpoint in the level
    private Checkpoint currentCheckpoint; // The most recent checkpoint

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterFirstCheckpoint(Checkpoint checkpoint)
    {
        firstCheckpoint = checkpoint;
        Debug.Log("First checkpoint registered: " + checkpoint.gameObject.name);
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        currentCheckpoint = checkpoint;
        Debug.Log("Current checkpoint updated: " + checkpoint.gameObject.name);
    }

    public Vector2 GetRespawnPoint(bool isHeartZero)
    {
        if (isHeartZero || currentCheckpoint == null)
        {
            return firstCheckpoint.transform.position; // Respawn at the first checkpoint
        }
        return currentCheckpoint.transform.position; // Respawn at the most recent checkpoint
    }
}
