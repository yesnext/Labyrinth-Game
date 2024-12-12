using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlot : MonoBehaviour
{
    public SpriteRenderer itemFrameRenderer; // The sprite renderer for the wall slot
    public GameObject wallSlotUI; // Reference to the UI panel
    public WallSlotUIManager wallSlotUIManager; // Reference to the WallSlotUIManager
    private bool isPlayerNearby = false;
    public Sprite correctItemPrefab;
    public GameObject interactHintText;

    public Vector2 maxItemSize = new Vector2(1f, 1f); // Maximum allowed width and height for the sprite

    void Start()
    {
        wallSlotUI.SetActive(false); // Hide the UI at the start
        interactHintText.SetActive(false);
        // Ensure that the wallSlotUIManager is assigned (otherwise log an error)
        if (wallSlotUIManager == null)
        {
            Debug.LogError("WallSlotUIManager is not assigned in the WallSlot script.");
        }
    }

    void Update()
    {
        // Check if the player is nearby and presses the interact button (e.g., "E")
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (wallSlotUI != null && !wallSlotUI.activeSelf)
            {
                wallSlotUI.SetActive(true); // Show the UI when the player presses the interact button
                if (wallSlotUIManager != null)
                {
                    wallSlotUIManager.SetCurrentWallSlot(this); // Set current wall slot
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered wall slot trigger");
            isPlayerNearby = true;
            interactHintText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited wall slot trigger");
            isPlayerNearby = false;
            interactHintText.SetActive(false);
            if (wallSlotUI != null && wallSlotUI.activeSelf)
            {
                wallSlotUI.SetActive(false); // Hide the UI when the player leaves the trigger area
            }
        }
    }

    public void PlaceItem(Sprite itemSprite)
    {
        if (itemSprite != null)
        {
            // Scale the item to fit within the max size
            float scaleX = maxItemSize.x / itemSprite.bounds.size.x; // Calculate scale for width
            float scaleY = maxItemSize.y / itemSprite.bounds.size.y; // Calculate scale for height

            // Take the smaller of the two scales to ensure the item fits within the box
            float scale = Mathf.Min(scaleX, scaleY);

            // Apply the scaling to the sprite
            itemFrameRenderer.sprite = itemSprite;
            itemFrameRenderer.transform.localScale = new Vector3(scale, scale, 1);

            Debug.Log($"Placed item with sprite: {itemSprite.name} (scaled to max size)");
            CheckIfCorrectItem();
        }
        else
        {
            Debug.LogError("Trying to place a null item.");
        }
    }

    public bool CheckIfCorrectItem()
    {
        if (itemFrameRenderer.sprite == correctItemPrefab)
        {
            Debug.Log("Correct item placed!");
            return true; // The placed item matches the prefab
        }
        else
        {
            Debug.Log("Incorrect item placed.");
            return false; // The placed item does not match the prefab
        }
    }
}
