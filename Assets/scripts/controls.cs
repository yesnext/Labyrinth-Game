using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controls : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    private float jumpingPower = 16f;
    public bool isFacingRight = true;
    public bool frozen;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 12f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        if(isDashing){
            return;
        }

        if(!frozen){
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        Flip();

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash){
            StartCoroutine(Dash());
        }
        }
        
    }

    private void FixedUpdate()
    {
        if(isDashing){
            return;
        }
    
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        
    }

    private IEnumerator Dash(){
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
   
}