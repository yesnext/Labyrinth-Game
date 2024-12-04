using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controls2d : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb2d.velocity = moveInput * moveSpeed;

        if (Input.GetKey(KeyCode.W)){
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90f);
        }
        if (Input.GetKey(KeyCode.S)){
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 270f);
        }
        if (Input.GetKey(KeyCode.D)){
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f);
        }
        if (Input.GetKey(KeyCode.A)){
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180f);
        }
    }
}
