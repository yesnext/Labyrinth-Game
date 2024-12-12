using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRun : MonoBehaviour
{
    public bool turnon;
    public Lava lava;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(turnon){
            StartLava();
        }
    }
    public void StartLava(){
        turnon = false;
        Instantiate(lava,transform.position, Quaternion.identity);
    }

}
