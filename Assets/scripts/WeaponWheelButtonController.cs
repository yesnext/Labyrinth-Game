using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponWheelButtonController : MonoBehaviour
{
    public int ID; // ID for each weapon
    private Animator anim;
    public string itemName;
    public TextMeshProUGUI itemText;
    public Image selectedItem;
    private bool selected = false;
    public Sprite icon;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update the selected item image and text when the button is active
    void Update()
    {
        if (selected)
        {
            selectedItem.sprite = icon;  // Set the icon of the weapon
            itemText.text = itemName;    // Display weapon name
        }
    }

    public void Selected()
    {
        selected = true;
        WeaponWheelController.weaponID = ID; // Update the weaponID when selected
        Debug.Log("Weapon selected with ID: " + ID);
    }

    public void Deselected()
    {
        selected = false;
        WeaponWheelController.weaponID = 0; // Reset weaponID when deselected
        Debug.Log("Weapon deselected");
    }

    public void HoverEnter()
    {
        anim.SetBool("Hover", true);
        itemText.text = itemName;  // Show weapon name when hovered
    }

    public void HoverExit()
    {
        anim.SetBool("Hover", false);
        itemText.text = ""; // Clear name when not hovered
    }
}
