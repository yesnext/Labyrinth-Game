using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectInteract : MonoBehaviour
{
    public GameObject CurrentInterObject = null;
    public interactObject currentInterObjectscript = null;
    public inventory i;
    public InventoryUI inventoryUI;
    // Start is called before the first frame update
    void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
        if (inventoryUI == null)
        {
            Debug.LogError("InventoryUI script not found in the scene!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && CurrentInterObject){
            if(currentInterObjectscript.inventory){
                i.AddItem(CurrentInterObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Raycast to detect a WallSlot when interacting
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f);
            if (hit.collider != null)
            {
                WallSlot wallSlot = hit.collider.GetComponent<WallSlot>();
                if (wallSlot != null)
                {
                    Debug.Log("Interacting with wall slot...");
                    OpenInventoryForSlot(wallSlot);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag ("InteractObject")){
            Debug.Log(other.name);
            CurrentInterObject = other.gameObject;
            currentInterObjectscript = CurrentInterObject.GetComponent<interactObject>();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag ("InteractObject")){
            if(other.gameObject == CurrentInterObject){
                CurrentInterObject = null;
            }
        }
    }

    void OpenInventoryForSlot(WallSlot wallSlot)
    {
        // Open the inventory UI and pass a callback for when an item is selected
        inventoryUI.OpenInventory((selectedItem) =>
        {
            // Assign the selected item to the wall slot
            wallSlot.SetItem(selectedItem);

            // Remove the item from the inventory
            inventoryUI.RemoveItem(selectedItem);
        });
    }

    void AssignItemToSlot(WallSlot wallSlot, GameObject selectedItem)
    {
    // Assign the item to the wall slot
    wallSlot.SetItem(selectedItem);

    // Remove the item from the inventory
    inventoryUI.RemoveItem(selectedItem);
    }
}
