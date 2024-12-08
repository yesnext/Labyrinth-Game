using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject[] slots;  // UI slots (for each inventory slot)
    private inventory inventoryScript;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);  // Persist UI across scenes
    }

    void Start()
    {
        inventoryScript = FindObjectOfType<inventory>();  // Find the inventory script
        if (inventoryScript == null)
        {
            Debug.LogError("Inventory script not found in the scene!");
        }
    }

    // This method updates the UI whenever an item is added to the inventory
    public void UpdateUI()
{
    if (inventoryScript != null)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventoryScript.Inventory.Length && inventoryScript.Inventory[i] != null)
            {
                Image slotImage = slots[i].GetComponent<Image>();
                Sprite itemSprite = inventoryScript.Inventory[i].GetComponent<SpriteRenderer>()?.sprite;

                if (itemSprite != null)
                {
                    slotImage.sprite = itemSprite;
                    slots[i].SetActive(true);  // Make the slot visible
                    Debug.Log("Item sprite set for slot " + i);  // Debug log
                }
                else
                {
                    Debug.LogWarning("No sprite found for item in slot " + i);
                    slotImage.sprite = null;
                    slots[i].SetActive(false);
                }
            }
            else
            {
                slots[i].GetComponent<Image>().sprite = null;
                slots[i].SetActive(false);
            }
        }
    }
}

    public void OnItemAdded(GameObject item)
    {
        UpdateUI();  // Update the UI when an item is added from the inventrory script
    }


public void OpenInventory(System.Action<GameObject> onItemSelected)
{
    if (inventoryPanel != null)
    {
        inventoryPanel.SetActive(true);  // Enable the inventory UI panel

        // Add functionality for selecting an item
        foreach (GameObject slot in slots)
        {
            Button button = slot.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();  // Clear previous listeners
                button.onClick.AddListener(() =>
                {
                    GameObject selectedItem = slot.GetComponent<ItemSlot>()?.GetItem();
                    if (selectedItem != null)
                    {
                        inventoryPanel.SetActive(false);  // Close the inventory
                        onItemSelected?.Invoke(selectedItem);
                    }
                });
            }
        }
    }
    else
    {
        Debug.LogError("Inventory panel is not assigned in the InventoryUI script!");
    }
}

public void RemoveItem(GameObject item)
    {
        if (inventoryScript != null && item != null)
        {
            for (int i = 0; i < inventoryScript.Inventory.Length; i++)
            {
                if (inventoryScript.Inventory[i] == item)
                {
                    // Remove the item from the backend array
                    inventoryScript.Inventory[i] = null;

                    // Clear the corresponding UI slot
                    slots[i].GetComponent<Image>().sprite = null;
                    slots[i].SetActive(false);

                    Debug.Log($"{item.name} removed from inventory.");
                    UpdateUI(); // Refresh the inventory UI
                    return;
                }
            }

            Debug.LogWarning("Item not found in inventory!");
        }
    }
}

