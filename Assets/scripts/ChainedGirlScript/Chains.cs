using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chains : MonoBehaviour
{
    public ChainedGirlBoss TheChainedGirl;
    public int Health;
    public float distance;
    public PlayerStats player;
    public float ChainWhipDistance = 4.0f;
    public int ChainWhipDamage = 0;
    public float ChainWhipCooldown = 2.0f;
    public float LastChainWhipCooldown = 0.0f;
    public Vector2 direction;
    public bool isFacingRight;
    public Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        GhangedirectionFollow();
        if (distance < ChainWhipDistance)
        {
            ChainWhipAttack();
        }
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
    }
    public void TakeDamage(int damage, bool element)
    {
        if (!element)
        {
            Health -= damage;
        }
        if (Health <= 0)
        {
            TheChainedGirl.BegoneThot();
            Destroy(this.gameObject);
        }
    }
    private void ChainWhipAttack()
    {
        player.TakeDamage(ChainWhipDamage);
    }
     public void GhangedirectionFollow()
    {
        if (direction.x > 0 && !isFacingRight)
        {

            scale = transform.localScale;
            scale.x *= -1;
            isFacingRight = true;
            transform.localScale = scale;

        }
        else if (direction.x < 0 && isFacingRight)
        {
            scale = transform.localScale;
            scale.x *= -1;
            isFacingRight = false;
            transform.localScale = scale;
        }
    }
    public void OnTriggerEnter2D(){
        ChainWhipAttack();
    }
}
