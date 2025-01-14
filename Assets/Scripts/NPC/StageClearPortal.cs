using UnityEngine;

public class StageClearPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScreenFade screenFade = FindObjectOfType<ScreenFade>();
            if (screenFade != null)
            {
                screenFade.TriggerFade();
            }
        }
    }
}
