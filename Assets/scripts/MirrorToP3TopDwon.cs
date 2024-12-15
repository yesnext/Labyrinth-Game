using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MirrorToP3TopDwon : MonoBehaviour
{
    public GameObject interactHint;
    public string sceneToLoad; 
    public bool canLoad = false;
    // Start is called before the first frame update
    void Start()
    {
        interactHint.SetActive(false);
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
