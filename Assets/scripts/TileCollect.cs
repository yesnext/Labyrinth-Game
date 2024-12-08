using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollect : MonoBehaviour
{
    public int points = 1;
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
            Destroy(this.gameObject);
            FindObjectOfType<PLayerStats>().totalpointsP1 += points;
            Debug.Log("points " + FindObjectOfType<PLayerStats>().totalpointsP1);
        }
    }
}
