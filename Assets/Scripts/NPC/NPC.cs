using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    [TextArea] public string[] dialogues; // Array of dialogue lines
    public AudioClip[] audioClips; // Array of audio clips
    private int currentLine = 0;

    public TMP_Text textDisplay; // Reference to the TextMeshPro component
    private AudioSource audioSource;

    void Start()
    {
        // Ensure the text is initially hidden
        if (textDisplay != null)
        {
            textDisplay.gameObject.SetActive(false);
        }

        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public string GetNextDialogue()
    {
        if (dialogues.Length == 0) return "";
        string line = dialogues[currentLine];

        if (audioClips != null && currentLine < audioClips.Length && audioClips[currentLine] != null)
        {
            audioSource.clip = audioClips[currentLine];
            audioSource.Play();
        }

        currentLine = (currentLine + 1) % dialogues.Length; // Loop through dialogues
        return line;
    }

    public void ShowDialogue()
    {
        if (textDisplay != null)
        {
            textDisplay.text = GetNextDialogue(); // Update the text
            textDisplay.gameObject.SetActive(true); // Show the text
        }
    }

    public void HideDialogue()
    {
        if (textDisplay != null)
        {
            textDisplay.gameObject.SetActive(false); // Hide the text
        }

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}