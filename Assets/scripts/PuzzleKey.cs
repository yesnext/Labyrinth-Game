using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleKey : MonoBehaviour
{
    public bool istriggered = false;
    public GameObject interactHint;
    // Start is called before the first frame update
    void Start()
    {
        interactHint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"){
            istriggered = true;
            interactHint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
            istriggered = false;
            interactHint.SetActive(false);
    }
}
