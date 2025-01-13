using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string levelName; 

    //icon Image
    public Image iconImage; 

    //key Image
    public Image keyImage;

    //number icon
    public Sprite lockedIcon; 
    public Sprite unlockedIcon;

    //keyicon
    public Sprite lockedKey;
    public Sprite unlockedKey;

    public int levelIndex; // Unique index for this level

    //check if level is unlocked
    public bool isUnlocked = false;

    public float hoverScale = 1.1f; 
    private Vector3 originalScale; 
    private static GameObject parentToPersist;

    void Start()
    {
        
        AssignParentToDontDestroyOnLoad();

        
        originalScale = transform.localScale;

        // Load the unlock state for this level
        isUnlocked = PlayerPrefs.GetInt("Level" + levelIndex, 0) == 1;

        // Update the button icon based on the locked/unlocked state
        UpdateIcon();
    }

    private void AssignParentToDontDestroyOnLoad()
    {
        // Find the parent GameObject
        GameObject parent = transform.parent.gameObject;

        // If it's not already set to persist, assign it
        if (parentToPersist == null)
        {
            parentToPersist = parent;
            DontDestroyOnLoad(parentToPersist); // Prevent the parent and all children from being destroyed
        }
        else if (parentToPersist != parent)
        {
            // Prevent duplicate parents from persisting
            Debug.LogWarning("Multiple parent objects detected. Ensure only one parent is used for Level Buttons.");
        }
    }

    // Click the level button
    public void OnClick()
    {
        if (isUnlocked)
        {
            /*// Play the default BGM when entering the level
            if (BGMmanager.instance != null && BGMmanager.instance.defaultBGM != null)
            {
                BGMmanager.instance.PlayBGM(BGMmanager.instance.defaultBGM);
            }*/

            // Load the scene
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.Log("Level is locked!");
        }
    }
    public void UpdateIcon()
    {
        if (isUnlocked)
        {
            iconImage.sprite = unlockedIcon;
        }
        else
        {
            iconImage.sprite = lockedIcon;
        }
    }

    public void UnlockLevel()
    {
        isUnlocked = true;

        // Save the unlock state persistently
        PlayerPrefs.SetInt("Level" + levelIndex, 1);
        PlayerPrefs.Save();

        // Update the button UI
        UpdateIcon();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Enlarge the button when hovering
        transform.localScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Reset the button scale
        transform.localScale = originalScale;
    }
}
