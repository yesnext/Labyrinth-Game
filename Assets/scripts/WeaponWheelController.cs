using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public static WeaponWheelController Instance; // Singleton instance for easy access

    public Animator anim;
    private bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage; // Ensure this is your "empty" sprite (e.g., no weapon selected)
    public static int weaponID;

    private PlayerStats playerStats;

    private int currentWeaponID = -1; // Keep track of the currently selected weapon
    private bool isWeaponIDSetManually = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Find the PlayerStats instance in the scene
        playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats not found in the scene!");
        }
    }

    void Update()
    {
        // Toggle weapon wheel visibility
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            weaponWheelSelected = !weaponWheelSelected;
            isWeaponIDSetManually = false; // Reset this flag whenever the wheel is toggled
            Debug.Log("Weapon wheel toggled: " + weaponWheelSelected);
        }

        anim.SetBool("OpenWeaponWheel", weaponWheelSelected);

        // Only allow weapon selection when the wheel is open
        if (weaponWheelSelected)
        {
            // Prevent automatic weapon change and only update the weapon if explicitly selected
            if (currentWeaponID != weaponID)
            {
                currentWeaponID = weaponID; // Update the tracked weaponID
                Debug.Log("WeaponID changed to: " + currentWeaponID);

                UpdateSelectedWeaponByID(currentWeaponID);
            }
        }

        // Disable manual weapon selection when the weapon wheel is closed
        if (!weaponWheelSelected)
        {
            isWeaponIDSetManually = false;
        }
    }

    public void UpdateSelectedWeapon(Sprite weaponIcon)
{
    if (selectedItem == null)
    {
        Debug.LogError("SelectedItem Image reference is missing!");
        return;
    }

    if (weaponIcon == null)
    {
        Debug.Log("No weapon icon selected, using default 'noImage' sprite.");
        selectedItem.sprite = noImage;
    }
    else
    {
        Debug.Log("Setting weapon icon: " + weaponIcon.name);
        selectedItem.sprite = weaponIcon;
    }

    // Ensure the Image component is enabled
    selectedItem.enabled = selectedItem.sprite != null;
}



    private void UpdateSelectedWeaponByID(int id)
    {
        switch (id)
        {
            case 0: // No weapon selected
                selectedItem.sprite = noImage; // Empty sprite
                UpdatePlayerStats(throwingHands: false, element: false);
                break;

            case 1: // Melee
                selectedItem.sprite = noImage; // Replace with melee icon
                UpdatePlayerStats(throwingHands: true, element: false);
                break;

            case 2: // Ice
                selectedItem.sprite = noImage; // Replace with ice icon
                UpdatePlayerStats(throwingHands: false, element: true);
                break;

            case 3: // Fire
                selectedItem.sprite = noImage; // Replace with fire icon
                UpdatePlayerStats(throwingHands: false, element: false);
                break;

            default:
                Debug.LogWarning("Unknown weaponID: " + id);
                break;
        }
    }

    private void UpdatePlayerStats(bool throwingHands, bool element)
    {
        if (playerStats != null)
        {
            playerStats.ThrowingHands = throwingHands;
            playerStats.element = element;
        }
        else
        {
            Debug.LogError("PlayerStats reference is null!");
        }
    }
}
