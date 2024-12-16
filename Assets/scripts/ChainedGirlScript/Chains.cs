using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chains : UniversalEnemyNeeds
{
    public ChainedGirlBoss TheChainedGirl;
    public float ChainWhipDistance = 1.0f;
    public int ChainWhipDamage = 0;
    public float ChainWhipCooldown = 2.0f;
    public float LastChainWhipCooldown = 0.0f;


//bob addition
    public HealthBar healthbar;

    //bob addition
    private GameObject enemyCanvas;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerStats>();
        TheChainedGirl = FindObjectOfType<ChainedGirlBoss>();
        if(player.GetComponent<BossesDefeated>().chainedgirl){
            Destroy(this.gameObject);
        }

        //bob addition
        healthbar.SetMaxHealth(Health);


        //bob addition
       enemyCanvas = GameObject.FindGameObjectWithTag("EnemyCanvas");
        enemyCanvas.SetActive(false);  // Hide health bar initially
    }

    // Update is called once per frame
    void Update()
    {
        if (aggro)
        {
            if (distance < ChainWhipDistance && Time.time - LastChainWhipCooldown > ChainWhipCooldown && !MeleeAttacking)
            {
                ChangedDirectionFollow();
                StartCoroutine(ChainWhipAttack());
            }
             //bob addition
             enemyCanvas.SetActive(true);
        }
        else
    {
        // Hide the health bar when not aggro
        enemyCanvas.SetActive(false);
    }
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < aggrodistance && !aggro)
        {
            aggro = true;
        }
    }
    public void TakeDamage(int damage, bool element)
    {
        if (aggro)
        {
            if (!element)
            {
                //bob addition
            healthbar.SetHealth(Health);
                Health -= damage;
            }
            if (Health <= 0)
            {
                TheChainedGirl.Begone();
                Destroy(this.gameObject);
                //bob addition
            Destroy(GameObject.FindGameObjectWithTag("EnemyCanvas"));
            }
        }
    }
    private IEnumerator ChainWhipAttack()
    {
        MeleeAttacking = true;
        yield return new WaitForSeconds(MeleeAttackAnimationDuration);
        MeleeAttacking = false;
        LastChainWhipCooldown = Time.time;
    }
    public void OnTriggerEnter2D()
    {
        if (attackbox.enabled)
        {
            player.TakeDamage(ChainWhipDamage);
        }
    }
}
