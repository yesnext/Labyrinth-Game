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

    public void Selected()
{
    selected = true;
    WeaponWheelController.weaponID = ID; // Notify the controller of the selected ID
    WeaponWheelController.Instance.UpdateSelectedWeapon(icon); // Update the displayed image immediately
    Debug.Log("Weapon selected with ID: " + ID);
}


    public void Deselected()
    {
        selected = false;
        WeaponWheelController.weaponID = 0; // Reset weaponID
        WeaponWheelController.Instance.UpdateSelectedWeapon(null); // Reset the displayed image
        Debug.Log("Weapon deselected");
    }

    public void HoverEnter()
    {
        anim.SetBool("Hover", true);
        itemText.text = itemName; // Show weapon name when hovered
    }

    public void HoverExit()
    {
        anim.SetBool("Hover", false);
        itemText.text = ""; // Clear name when not hovered
    }
}
