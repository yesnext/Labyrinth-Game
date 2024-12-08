using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    public GameObject[] Inventory = new GameObject[15];  // Array to store inventory items
    public InventoryUI inventoryUI;  // Reference to InventoryUI (this will be set in the Inspector)

    void Awake()
    {
        DontDestroyOnLoad(gameObject);  // Persist UI across scenes
    }
    void Start()
    {
        if (inventoryUI == null)
        {
            Debug.LogError("InventoryUI reference not assigned in the Inspector!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(GameObject item)
    {
        bool itemadded = false;
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == null)
            {
                Inventory[i] = item;
                Debug.Log(item.name + " was added to inventory slot " + i);
                itemadded = true;
                item.SendMessage("DoInteraction");
                inventoryUI.OnItemAdded(item);

                // Update the UI after adding the item
                if (inventoryUI != null)
                {
                    inventoryUI.UpdateUI();  // Refresh the UI to show the new item
                }
                break;
            }
        }

        if (!itemadded)
        {
            Debug.Log("Inventory full - item not added");
        }
    }
    
    public void RemoveItem(GameObject item)
{
    for (int i = 0; i < Inventory.Length; i++)
    {
        if (Inventory[i] == item)
        {
            Inventory[i] = null;
            Debug.Log(item.name + " removed from inventory");
            break;
        }
    }
    // Refresh the UI
    InventoryUI inventoryUI = FindObjectOfType<InventoryUI>();
    if (inventoryUI != null)
    {
        inventoryUI.UpdateUI();
    }
}
}
