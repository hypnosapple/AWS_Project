using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    public bool isFirstCheckpoint = false;
    private bool isActivated = false; // To track if this checkpoint has been marked
    
    public float flashDuration = 0.5f;
    public float minAlpha = 0.3f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        if (isFirstCheckpoint)
        {
            CheckpointManager.Instance.RegisterFirstCheckpoint(this);
        }
        
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
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
            
            if (spriteRenderer != null)
            {
                StartCoroutine(FlashCheckpoint());
            }
        }
    }
    
    // 闪烁协程
    private IEnumerator FlashCheckpoint()
    {
        float halfDuration = flashDuration / 2f;
        
        for (float t = 0; t < halfDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(originalColor.a, minAlpha, t / halfDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        
        for (float t = 0; t < halfDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(minAlpha, originalColor.a, t / halfDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        
        spriteRenderer.color = originalColor;
    }
    
}
