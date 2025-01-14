using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string levelName;

    // Icon Image (assigned in the Inspector or dynamically found)
    public Image iconImage;

    // Key Image (assigned in the Inspector or dynamically found)
    public Image keyImage;

    // Sprites for locked and unlocked states
    public Sprite lockedIcon;
    public Sprite unlockedIcon;

    // Key sprites
    public Sprite lockedKey;
    public Sprite unlockedKey;

    public int levelIndex; // Unique index for this level

    // Check if the level is unlocked
    public bool isUnlocked = false;
    public bool hasKey = false; // Check if the key for this level is collected (for key display purposes)

    public float hoverScale = 1.1f;
    private Vector3 originalScale;

    [SerializeField]
    private UnityEngine.UI.Button buttonComponent; // Button component

    void Start()
    {
        originalScale = transform.localScale;

        // Assign the Button component dynamically if not assigned in the Inspector
        if (buttonComponent == null)
        {
            buttonComponent = GetComponent<UnityEngine.UI.Button>();
            if (buttonComponent == null)
            {
                Debug.LogError("Button component is missing from the GameObject!");
                return;
            }
        }

        // Dynamically find child components if not assigned
        if (iconImage == null)
            iconImage = transform.Find("Icon").GetComponent<Image>();

        if (keyImage == null)
            keyImage = transform.Find("KeyImage").GetComponent<Image>();

        // Load the unlock state for this level
        isUnlocked = PlayerPrefs.GetInt("Level" + levelIndex, 0) == 1;

        // Load the key state for this level
        hasKey = PlayerPrefs.GetInt("Key" + levelIndex, 0) == 1;

        // Update the button UI
        UpdateIcon();
        UpdateKeySprite();
    }

    public void OnClick()
    {
        if (levelIndex == 5) // Logic specific to Level 5
        {
            if (isUnlocked && AllKeysCollected())
            {
                MenuBGM.instance?.StopBGM();
                SceneManager.LoadScene(levelName);
            }
            else
            {
                Debug.Log("Level 5 is locked! Ensure all keys are collected and previous levels are completed.");
            }
        }
        else // Logic for other levels
        {
            if (isUnlocked)
            {
                MenuBGM.instance?.StopBGM();
                SceneManager.LoadScene(levelName);
            }
            else
            {
                Debug.Log("Level is locked! Complete the previous level to unlock.");
            }
        }
    }

    public void UpdateIcon()
    {
        if (buttonComponent == null)
        {
            Debug.LogError("Button component not found, cannot update icon!");
            return;
        }

        buttonComponent.interactable = isUnlocked; // Enable or disable button interaction

        if (isUnlocked)
        {
            iconImage.sprite = unlockedIcon;
        }
        else
        {
            iconImage.sprite = lockedIcon;
        }
    }

    public void UpdateKeySprite()
    {
        if (keyImage == null)
        {
            Debug.LogError("Key Image is not assigned!");
            return;
        }

        // Check if the key for this level has been collected
        hasKey = PlayerPrefs.GetInt("Key" + levelIndex, 0) == 1;

        if (hasKey)
        {
            keyImage.sprite = unlockedKey;
        }
        else
        {
            keyImage.sprite = lockedKey;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isUnlocked)
        {
            transform.localScale = originalScale * hoverScale;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }

    private bool AllKeysCollected()
    {
        // Check if keys for all required levels are collected (Level 1 to 4)
        for (int i = 1; i <= 4; i++) // Levels 1 to 4
        {
            if (PlayerPrefs.GetInt("Key" + i, 0) == 0)
            {
                return false;
            }
        }
        return true;
    }
}
