using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controls : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    public Transform ProjectilePoint;
    public GameObject Projectile;
    public static int projectileCount=0;
    public int Health =10;
    protected Animator animator;
    public KeyCode MeleeAttackkey;
    public float MeleeAttackDistance=5.0f;
    public int MeleeAttackDamage=6;
    public float MeleeAttackCooldown = 3.0f; //time in seconds for the cooldown
    public LayerMask enemyLayer;
    private float lastMeleeAttackTime = 0f;
    public bool attackanim=false;
    public bool element = false; // if false it will be fire if tru will be ice
    public KeyCode chosenelement;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
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
        if (Input.GetKeyDown(MeleeAttackkey)){
            MeleeAttack();
        }
        Flip();
        if (Input.GetKeyDown(chosenelement)){
            ChangeElement();
        }
    }

    private void ChangeElement()
    {
        element= !element;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
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
    public void Shoot(){
      projectileCount++;
      Instantiate(Projectile,ProjectilePoint.position,ProjectilePoint.rotation);
    }
    public void TakeDamage(int damage){
        Health=Health-damage;
        Debug.Log("Health = "+Health);
    }
    public void MeleeAttack(){
        if(Time.time - lastMeleeAttackTime>MeleeAttackCooldown){
        attackanim=true;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, MeleeAttackDistance, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<BasicEnemy>().TakeDamage(MeleeAttackDamage,element);
        }
        }
    }
}