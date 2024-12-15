using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorToTPUp : MonoBehaviour
{
    public Vector2 newPosition;
    public GameObject objectToMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.CompareTag("Player"))
        {
            objectToMove.transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }
}
