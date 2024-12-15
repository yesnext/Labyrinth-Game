using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class p2ExitDoor : MonoBehaviour
{
    public string sceneToLoad; 
    public GameObject interactHint;
    public bool canLoad = false;
    // Start is called before the first frame update
    public int requiredCorrectSlots = 4;
    private WallSlot[] allWallSlots;
    private int correctSlotCount = 0;
    void Start()
    {
        allWallSlots = FindObjectsOfType<WallSlot>();
        foreach (WallSlot slot in allWallSlots)
        {
            slot.OnWallSlotChanged += UpdateCorrectSlotCount; // Subscribe to changes
        }
        UpdateCorrectSlotCount();
    }

    // Update is called once per frame
    void Update()
    {
       if (canLoad && Input.GetKeyDown(KeyCode.E))
        {
            if (correctSlotCount >= requiredCorrectSlots)
            {
                SceneManager.LoadScene(sceneToLoad);
                Debug.Log("Door opened. Scene switched!");
            }
            else
            {
                Debug.Log("You need to place all correct items to exit the room!");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            interactHint.SetActive(true);
            canLoad = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        interactHint.SetActive(false);
        canLoad = false;
    }
    private void UpdateCorrectSlotCount()
    {
        correctSlotCount = 0;
        foreach (WallSlot slot in allWallSlots)
        {
            if (slot.CheckIfCorrectItem())
            {
                correctSlotCount++;
            }
        }
        Debug.Log($"Current correct items: {correctSlotCount} out of {allWallSlots.Length}");
    }
}
