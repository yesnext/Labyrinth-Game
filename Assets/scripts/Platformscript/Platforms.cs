using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Platforms : MonoBehaviour
{
    public bool OnTop;
    public KeyCode DropDown;
    private PlayerStats player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(DropDown) && player.GetComponent<controls>().IsGrounded())
        {
            transform.parent.GetComponent<Collider2D>().enabled = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            transform.parent.GetComponent<Collider2D>().enabled = OnTop;
        }
        
    }
}
