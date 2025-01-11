using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    [TextArea] public string[] dialogues; // Array of dialogue lines
    private int currentLine = 0;

    public TMP_Text textDisplay; // Reference to the TextMeshPro component

    void Start()
    {
        // Ensure the text is initially hidden
        if (textDisplay != null)
        {
            textDisplay.gameObject.SetActive(false);
        }
    }

    public string GetNextDialogue()
    {
        if (dialogues.Length == 0) return "";
        string line = dialogues[currentLine];
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
    }
}