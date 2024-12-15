using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallSlotUIManager : MonoBehaviour
{
    public Button[] itemButtons; // These will be filled dynamically
    public WallSlot currentWallSlot;
    public GameObject wallSlotUI; // Reference to the UI panel (parent of the slots)
    public Image[] itemImages; // Manually assign the images in the inspector

    void Start()
    {
        // Dynamically find all Button components inside the 'selection' UI panel
        itemButtons = wallSlotUI.GetComponentsInChildren<Button>();

        // Ensure the UI panel is initially inactive
        wallSlotUI.SetActive(false);

        // Add listeners to each button
        foreach (Button button in itemButtons)
        {
            // Attach the button click event
            button.onClick.AddListener(() => OnItemButtonClicked(button));
        }
    }

    // This function is called to assign the current wall slot when the UI is shown
    public void SetCurrentWallSlot(WallSlot wallSlot)
    {
        currentWallSlot = wallSlot; 
        Debug.Log("Current Wall Slot set: " + wallSlot.name); // Optional debug log
    }

    // Called when an item button is clicked
    private void OnItemButtonClicked(Button clickedButton)
    {
        Debug.Log("Button clicked: " + clickedButton.name); // Debug log to check if the button is clicked
        if (currentWallSlot != null)
        {
            // Find the corresponding image in the manually assigned array
            int buttonIndex = System.Array.IndexOf(itemButtons, clickedButton);

            if (buttonIndex >= 0 && buttonIndex < itemImages.Length)
            {
                Image itemImage = itemImages[buttonIndex];

                if (itemImage != null && itemImage.sprite != null)
                {
                    // Pass the sprite to the wall slot
                    currentWallSlot.PlaceItem(itemImage.sprite); 
                    wallSlotUI.SetActive(false); // Hide the UI after item is selected
                }
                else
                {
                    Debug.LogError("The assigned image for the button does not have a sprite.");
                }
            }
        }
        else
        {
            Debug.Log("No wall slot assigned in currentWallSlot.");
        }
    }
}