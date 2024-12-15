using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controls2d : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
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
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * moveSpeed;
        animator.SetFloat("Horizontal",moveInput.x);
        animator.SetFloat("Vertical",moveInput.y);
        animator.SetFloat("speed",moveInput.sqrMagnitude);

    }
}
