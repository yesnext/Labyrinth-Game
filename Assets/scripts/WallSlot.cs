using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlot : MonoBehaviour
{
    private GameObject currentItem; // The item assigned to this slot
    private SpriteRenderer itemFrameRenderer; // SpriteRenderer for the item frame

    void Start()
    {
        // Get the SpriteRenderer from the GameObject this script is attached to
        itemFrameRenderer = GetComponentInChildren<SpriteRenderer>();
        if (itemFrameRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on the WallSlot object!");
        }
    }

    // Assign an item to this slot
    public void SetItem(GameObject item)
{
    if (item == null)
    {
        Debug.LogWarning("Tried to assign a null item to the wall slot.");
        return;
    }

    currentItem = item;

    // Get the sprite from the item and assign it to the frame
    Sprite itemSprite = item.GetComponent<SpriteRenderer>()?.sprite;
    if (itemFrameRenderer == null)
    {
        Debug.LogError("Item frame renderer is not assigned!");
        return;
    }

    if (itemSprite != null)
    {
        itemFrameRenderer.sprite = itemSprite; // Display the item's sprite
        Debug.Log($"Item {item.name} added to the wall slot.");
    }
    else
    {
        Debug.LogWarning($"Item {item.name} does not have a SpriteRenderer or the sprite is missing.");
    }
}

    // Get the item currently assigned to this slot
    public GameObject GetItem()
    {
        return currentItem;
    }

    // Clear the item from this slot
    public void ClearSlot()
    {
        currentItem = null;
        if (itemFrameRenderer != null)
        {
            itemFrameRenderer.sprite = null; // Clear the sprite
        }
    }
}
