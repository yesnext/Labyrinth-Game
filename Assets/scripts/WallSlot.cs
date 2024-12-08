using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlot : MonoBehaviour
{
    [System.Serializable]
    public class SlotData
    {
        public GameObject slotObject;      // The parent wall slot object
        public GameObject itemFrame;      // The child item frame for this slot
        public GameObject currentItem;    // The item currently placed in the frame
    }

    public SlotData[] wallSlots = new SlotData[4];  // Array to manage all wall slots
    private inventory playerInventory;             // Reference to the player's inventory

    void Start()
    {
        // Initialize wall slots and their item frames
        foreach (SlotData slot in wallSlots)
        {
            if (slot.itemFrame != null)
            {
                slot.itemFrame.SetActive(false);  // Hide all item frames initially
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player is near the wall
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<inventory>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = null;
        }
    }

    public void OpenWallSlot(int slotIndex)
    {
        if (playerInventory == null) return;

        // Open inventory to select an item
        InventoryUI inventoryUI = FindObjectOfType<InventoryUI>();
        if (inventoryUI != null)
        {
            inventoryUI.OpenInventory((selectedItem) =>
            {
                if (selectedItem != null && slotIndex >= 0 && slotIndex < wallSlots.Length)
                {
                    PlaceItemInSlot(slotIndex, selectedItem);
                }
            });
        }
    }

    private void PlaceItemInSlot(int slotIndex, GameObject item)
    {
        SlotData slot = wallSlots[slotIndex];

        if (slot.currentItem == null)  // Check if the slot is empty
        {
            slot.currentItem = item;  // Assign the selected item to the slot

            // Remove item from the player's inventory
            playerInventory.RemoveItem(item);

            // Update the item's appearance in the item frame
            SpriteRenderer itemSpriteRenderer = item.GetComponent<SpriteRenderer>();
            SpriteRenderer frameSpriteRenderer = slot.itemFrame.GetComponent<SpriteRenderer>();
            if (itemSpriteRenderer != null && frameSpriteRenderer != null)
            {
                frameSpriteRenderer.sprite = itemSpriteRenderer.sprite;  // Set the sprite
            }

            slot.itemFrame.SetActive(true);  // Make the item frame visible
            Debug.Log($"Item {item.name} placed in Slot {slotIndex}");
        }
        else
        {
            Debug.Log($"Slot {slotIndex} already has an item.");
        }
    }
}
