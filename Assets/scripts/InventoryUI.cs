using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
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
}



