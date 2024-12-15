using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class puzzle3ExitDoor : MonoBehaviour
{
    public string sceneToLoad; 
    public bool canLoad = false;
    public GameObject interactHint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canLoad && Input.GetKeyDown(KeyCode.E) && keyCollected.instance.isCollected()){
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
