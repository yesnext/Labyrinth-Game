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
        itemFrameRenderer = GetComponent<SpriteRenderer>();
        if (itemFrameRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on the WallSlot object!");
        }
    }

    // Assign an item to this slot
    public void SetItem(GameObject item)
    {
        currentItem = item;

        // Update the sprite on the item frame
        SpriteRenderer itemSpriteRenderer = item?.GetComponent<SpriteRenderer>();
        if (itemSpriteRenderer != null && itemFrameRenderer != null)
        {
            itemFrameRenderer.sprite = itemSpriteRenderer.sprite;
            Debug.Log($"Assigned {item.name} to this slot.");
        }
        else
        {
            Debug.LogWarning("Failed to assign item: Missing SpriteRenderer.");
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
