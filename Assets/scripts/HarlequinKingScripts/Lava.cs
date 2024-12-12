using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public float speed = 2.0f;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    public void OnTriggerEnter2D(Collider2D other){
        FindObjectOfType<HarlequinLevelManager>().RespawnPlayer();
        Destroy(this.gameObject);
    }
}
