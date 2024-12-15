using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle1ExitDoor : MonoBehaviour
{
    public string sceneToLoad; 
    public GameObject interactHint;
    public bool canLoad = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canLoad && Input.GetKeyDown(KeyCode.E)){
            SceneManager.LoadScene(sceneToLoad);
            Debug.Log("switched");
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        interactHint.SetActive(true);
        if(collider.CompareTag("Player")){
            canLoad = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        interactHint.SetActive(false);
        canLoad = false;
    }
}
