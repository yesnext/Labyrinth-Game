using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerObjectInteract : MonoBehaviour
{
    public GameObject CurrentInterObject = null;
    public interactObject currentInterObjectscript = null;
    public inventory i;
    public InventoryUI inventoryUI;
    private WallSlot currentWallSlot;
    public GameObject interactHint;
    public GameObject submitBox;
    private bool canInteract = false;
    private bool isInventoryOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        interactHint.SetActive(false);
        submitBox.SetActive(false);

        if (inventoryUI == null)
        {
            Debug.LogError("InventoryUI script not found in the scene!");
        }
        if (i == null)
        {
            Debug.LogError("Inventory script not assigned in the inspector!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Interact") && CurrentInterObject)
    {
        if (currentInterObjectscript.inventory)
        {
            i.AddItem(CurrentInterObject);
        }
    }

    // Handle wall slot interaction
    if (canInteract && Input.GetKeyDown(KeyCode.E))
    {
        if (!isInventoryOpen)
        {
            OpenInventoryForWallSlot(currentWallSlot);
            isInventoryOpen = true;
            submitBox?.SetActive(true);  // Show the submit box
        }
    }

    // Ensure submitBox is hidden when inventory is closed
    if (!isInventoryOpen && submitBox.activeSelf)
    {
        submitBox.SetActive(false);
    }

    // Prevent UI buttons from being ignored
    if (isInventoryOpen && EventSystem.current.currentSelectedGameObject == null)
    {
        EventSystem.current.SetSelectedGameObject(null); // Reset selection to prevent stuck UI
    }

        
    } 

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("WallSlot"))
        {
            Debug.Log($"Entered WallSlot trigger: {other.name}");
            canInteract = true;  // Set the flag to true, indicating that the player can interact with the wall slot
            interactHint.SetActive(true);  // Show the interaction hint
            currentWallSlot = other.GetComponent<WallSlot>();  // Cache the WallSlot reference
            
            if (inventoryUI != null)
        {
            inventoryUI.SetCurrentWallSlot(currentWallSlot); // Pass the current wall slot to the InventoryUI
        }
        }

        if(other.CompareTag ("InteractObject")){
            Debug.Log(other.name);
            CurrentInterObject = other.gameObject;
            currentInterObjectscript = CurrentInterObject.GetComponent<interactObject>();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("WallSlot"))
        {
            Debug.Log($"Exited WallSlot trigger: {other.name}");
            interactHint.SetActive(false);  // Hide the interaction hint
            currentWallSlot = null;  // Clear the cached WallSlot reference
            canInteract = false;
        }
        
        if(other.CompareTag ("InteractObject")){
            if(other.gameObject == CurrentInterObject){
                CurrentInterObject = null;
                currentInterObjectscript =null;
            }
        }
    }

    

    void OpenInventoryForWallSlot(WallSlot wallSlot)
{
    Debug.Log("Trying to open inventory for wall slot...");
    if (inventoryUI == null || wallSlot == null)
    {
        Debug.LogError("InventoryUI or WallSlot is not assigned!");
        return;
    }

    isInventoryOpen = true; // Mark inventory as open
    inventoryUI.OpenInventory((selectedItem) =>
    {
        if (selectedItem != null)
        {
            Debug.Log($"Placing {selectedItem.name} into the wall slot.");
            wallSlot.SetItem(selectedItem);  // Assign item to the wall slot
            i.RemoveItem(selectedItem);      // Remove the item from the inventory
        }
        else
        {
            Debug.LogWarning("Selected item is null or wall slot is null!");
        }

        CloseInventory(); // Close the inventory after selection
    });
}

void CloseInventory()
    {
        if (inventoryUI != null)
        {   
            Debug.Log("Closing inventory panel...");
            inventoryUI.inventoryPanel.SetActive(false);  // Hide the inventory UI
            isInventoryOpen = false;
            
        }
        else
        {
            Debug.LogError("InventoryUI is not assigned.");
        }
    }
}
