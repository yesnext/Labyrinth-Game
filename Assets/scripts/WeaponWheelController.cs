
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class WeaponWheelController : MonoBehaviour
{

    public Animator anim;
    private bool weaponWheelSelected =false ;
    public Image selectedItem;
    public Sprite noImage ;
    public static int weaponID;

     private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats not found in the scene!");
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab)){
            weaponWheelSelected=!weaponWheelSelected;
        }
        if (weaponWheelSelected){
            anim.SetBool("OpenWeaponWheel",true);
        
        }
        else {
             anim.SetBool("OpenWeaponWheel",false);
        }

        switch(weaponID){
            case 0:
            selectedItem.sprite = noImage;
            break;

            case 1://Melee
            Debug.Log ("Melee");
            playerStats.ThrowingHands = true;
             playerStats.element = false;
            break;
        case 2://Ice
            Debug.Log ("Ice");
             playerStats.ThrowingHands = false;
                    playerStats.element = true;
            break;

            case 3://Fire
            Debug.Log ("Fire");
            playerStats.ThrowingHands = false;
                    playerStats.element = false;
            break;

       

        }

        
    }
}
