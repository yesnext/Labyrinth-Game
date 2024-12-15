using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestp2 : MonoBehaviour
{
    public GameObject interactHint;
    public KeyCode interact;
    public GameObject des;
    private bool killchest = false;
    // Start is called before the first frame update
    void Start()
    {
       interactHint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(interact) && killchest){
                Destroy(des);
                Debug.Log("chest taken");
            }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            interactHint.SetActive(true);
            killchest = true;
            Echos.instance.Add(10);
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            interactHint.SetActive(false);
        }
    }
}
