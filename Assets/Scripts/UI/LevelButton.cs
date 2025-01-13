using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string levelName;

    // Icon Image
    public Image iconImage;

    // Key Image
    public Image keyImage;

    // Sprites for locked and unlocked states
    public Sprite lockedIcon;
    public Sprite unlockedIcon;

    //KEY
    public Sprite lockedKey;
    public Sprite unlockedKey;

    public int levelIndex; // Unique index for this level

    // Check if the level is unlocked
    public bool isUnlocked = false;

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

        // Load the unlock state
        isUnlocked = PlayerPrefs.GetInt("Level" + levelIndex, 0) == 1;

        // Update the button UI
        UpdateIcon();
    }

    public void OnClick()
    {
        if (isUnlocked)
        {
            MenuBGM.instance?.StopBGM();
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.Log("Level is locked!");
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
}
