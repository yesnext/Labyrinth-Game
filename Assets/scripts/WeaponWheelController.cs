
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I)){
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
            
            break;
        case 2://Ice
            Debug.Log ("Ice");
            break;

            case 3://Fire
            Debug.Log ("Fire");
            break;

       

        }

        
    }
}
