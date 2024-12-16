using UnityEngine;
using UnityEngine.UI;

public class SelectedItemDisplay : MonoBehaviour
{
    [Header("Weapon Sprites")]
    public Sprite noWeaponSprite;   // Default sprite when no weapon is selected
    public Sprite meleeWeaponSprite; // Sprite for melee weapon
    public Sprite iceWeaponSprite;  // Sprite for ice weapon
    public Sprite fireWeaponSprite; // Sprite for fire weapon

    private Image selectedItemImage; // The Image component attached to this GameObject

    void Start()
    {
        // Get the Image component attached to the GameObject
        selectedItemImage = GetComponent<Image>();

        if (selectedItemImage == null)
        {
            Debug.LogError("SelectedItemDisplay is not attached to an Image component!");
        }
    }

    void Update()
    {
        // Get the current weapon ID from the WeaponWheelController
        int weaponID = WeaponWheelController.weaponID;

        // Update the image based on the weapon ID
        switch (weaponID)
        {
            case 0: // No weapon selected
                selectedItemImage.sprite = noWeaponSprite;
                selectedItemImage.enabled = false; // Hide the image
                break;

            case 1: // Melee weapon
                selectedItemImage.sprite = meleeWeaponSprite;
                selectedItemImage.enabled = true;
                break;

            case 2: // Ice weapon
                selectedItemImage.sprite = iceWeaponSprite;
                selectedItemImage.enabled = true;
                break;

            case 3: // Fire weapon
                selectedItemImage.sprite = fireWeaponSprite;
                selectedItemImage.enabled = true;
                break;

            default:
                Debug.LogWarning("Unknown weapon ID: " + weaponID);
                selectedItemImage.sprite = noWeaponSprite;
                selectedItemImage.enabled = false; // Hide for unknown IDs
                break;
        }
    }
}
