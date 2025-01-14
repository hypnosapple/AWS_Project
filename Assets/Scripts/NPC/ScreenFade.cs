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

    public AudioSource bgmAudio; // Reference to the background music AudioSource
    public float audioFadeDuration = 3f; // Separate audio fade duration

    private bool isFading = false;

    public void TriggerFade()
    {
        if (!isFading)
        {
            StartCoroutine(FadeToWhite());
            StartCoroutine(FadeAudioOut());
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

        // Call BacktoMenu after fade
        Debug.Log("Game Over or Transition Complete.");
        BacktoMenu();
    }


    private IEnumerator FadeAudioOut()
    {
        if (bgmAudio == null)
        {
            Debug.LogWarning("No AudioSource assigned for background music.");
            yield break;
        }

        float elapsedTime = 0f;
        float initialVolume = bgmAudio.volume;

        while (elapsedTime < audioFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            bgmAudio.volume = Mathf.Lerp(initialVolume, 0f, elapsedTime / audioFadeDuration);
            yield return null;
        }

        bgmAudio.volume = 0f;
        bgmAudio.Stop(); // Stop the audio completely after fade
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
