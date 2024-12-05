using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongTileKill : MonoBehaviour
{
    public GameObject beginPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"){
            FindObjectOfType<controls2d>().transform.position = beginPoint.transform.position;
        }
    }
}
