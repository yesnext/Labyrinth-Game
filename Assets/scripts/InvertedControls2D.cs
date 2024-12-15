using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertedControls2D : MonoBehaviour
{
    
    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();      
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = -Input.GetAxisRaw("Horizontal");
        moveInput.y = -Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb2d.velocity = moveInput * moveSpeed;

       animator.SetFloat("Horizontal",moveInput.x);
        animator.SetFloat("Vertical",moveInput.y);
        animator.SetFloat("speed",moveInput.sqrMagnitude);
    }
}
