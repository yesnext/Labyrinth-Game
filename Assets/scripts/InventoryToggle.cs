using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryUI; // Assign your inventory UI GameObject in the Inspector

    private bool isInventoryOpen = false;

    void Start(){
        // Set the inventory UI to inactive by default
        inventoryUI.SetActive(false);
    }

    void Update()
    {
        // Check if the Tab key is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.SetActive(isInventoryOpen);

        // pause time when inventory is open
        Time.timeScale = isInventoryOpen ? 0 : 1;
    }
}

