using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenFade : MonoBehaviour
{
    public Image fadeImage; // Gradient screen mask
    public TMP_Text subtitleText; // Display captions
    public float fadeDuration = 2f; // Gradient time
    public string endingMessage = "Thank you for playing!"; // Closing title content
    public float subtitleDuration = 3f;

    private bool isFading = false;

    public void TriggerFade()
    {
        if (!isFading)
        {
            StartCoroutine(FadeToWhite());
        }
    }

    private IEnumerator FadeToWhite()
    {
        isFading = true;

        // Fade to white
        float elapsedTime = 0f;
        Color fadeColor = fadeImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeColor.a = alpha; // Increase Alpha
            fadeImage.color = fadeColor; // Applied color
            yield return null;
        }

        // Show subtitles when completely white
        subtitleText.gameObject.SetActive(true);
        subtitleText.text = endingMessage;

        yield return new WaitForSeconds(subtitleDuration); // Caption display time (adjustable)

        // TODO: Load the next scene or end the game here
        Debug.Log("Game Over or Transition Complete.");
        BacktoMenu();
    }
    
    public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}