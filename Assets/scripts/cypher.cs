using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cypher : MonoBehaviour
{
    public GameObject CypherScreen;
    public static bool opened;
    public KeyCode opening;
    // Start is called before the first frame update
    void Start()
    {
        opened = false;
        CypherScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(opening) && !opened && this.GetComponent<PuzzleKey>().istriggered == true){
            Open();
        }else if(Input.GetKeyDown(opening) && opened){
            Close();
        }
    }

    void Open(){
        CypherScreen.SetActive(true);
        opened = true;
    }

    public void Close(){
        CypherScreen.SetActive(false);
        opened = false;
    }
}
