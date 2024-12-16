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
    public Collider2D parent;
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
            parent.enabled = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            parent.enabled = OnTop;
        }
        
    }
}
