using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chainedgirllevelmanager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<ChainedGirlBoss>() == null){
            SceneManager.LoadScene("the puzzle that is 2");
        }
    }
}
