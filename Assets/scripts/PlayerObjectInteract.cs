using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectInteract : MonoBehaviour
{
    public GameObject CurrentInterObject = null;
    public interactObject currentInterObjectscript = null;
    public inventory i;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && CurrentInterObject){
            if(currentInterObjectscript.inventory){
                i.AddItem(CurrentInterObject);
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
}
