using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject[] slots;  // UI slots (for each inventory slot)
    private inventory inventoryScript;
    private WallSlot currentWallSlot;

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

                    Button button = slots[i].GetComponent<Button>();
                    if (button != null)
                    {
                        Debug.Log("found button " + button);
                        button.onClick.RemoveAllListeners(); // Clear previous listeners
                        button.onClick.AddListener(() => TransferItemToWallSlot(slotImage.gameObject));
                    }
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


public void OpenInventory(System.Action<GameObject> onItemSelected)
{
    if (inventoryPanel != null)
    {
        if (inventoryPanel.activeSelf)
        {
            // If the inventory is already open, close it
            inventoryPanel.SetActive(false);
            Debug.Log("Closing inventory panel...");
            return; // Exit the function to avoid updating buttons
        }

        // If the inventory is closed, open it
        inventoryPanel.SetActive(true);
        Debug.Log("Opening inventory panel...");

        // Add functionality for selecting an item when the inventory is open
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
                        Debug.Log($"Selected item: {selectedItem.name}");
                        onItemSelected?.Invoke(selectedItem); // Invoke the callback
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


public void SetCurrentWallSlot(WallSlot wallSlot)
    {
        currentWallSlot = wallSlot;
        Debug.Log($"Current wall slot set: {currentWallSlot?.name}");
    }


public void TransferItemToWallSlot(GameObject buttonSlot)
{
    Debug.Log("pressed");
    if (currentWallSlot == null)
        {
            Debug.LogError("No wall slot is currently active!");
            return;
        }

    if (buttonSlot != null)
    {
        Image slotImage = buttonSlot.GetComponent<Image>();
        if (slotImage != null && slotImage.sprite != null)
        {
            SpriteRenderer itemFrameRenderer = currentWallSlot?.GetComponentInChildren<SpriteRenderer>();
            if (itemFrameRenderer != null)
            {
                itemFrameRenderer.sprite = slotImage.sprite;
                Debug.Log($"Transferred item {slotImage.sprite.name} to the wall slot.");

                // Optionally clear the inventory slot
                slotImage.sprite = null;
                slotImage.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("No SpriteRenderer found on the WallSlot's ItemFrame.");
            }
        }
        else
        {
            Debug.LogWarning("The selected inventory slot is empty or missing an image.");
        }
    }
    else
    {
        Debug.LogError("No button slot was clicked.");
    }
}

}

