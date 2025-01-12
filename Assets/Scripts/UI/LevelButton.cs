using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string levelName; 
    public Image iconImage; 
    public Sprite lockedIcon; 
    public Sprite unlockedIcon; 
    public bool isUnlocked = false; 
    public float hoverScale = 1.1f; 

    private Vector3 originalScale; // To reset scale after hover

    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);

        // Save the original scale for resetting
        originalScale = transform.localScale;

        // Update the button icon based on the locked/unlocked state
        UpdateIcon();
    }

    //click the level button
    public void OnClick()
    {
        if (isUnlocked)
        {
            
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
