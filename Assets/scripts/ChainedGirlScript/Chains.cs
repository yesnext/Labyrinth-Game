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

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        TheChainedGirl = FindObjectOfType<ChainedGirlBoss>();
        if(player.GetComponent<BossesDefeated>().chainedgirl){
            Destroy(this.gameObject);
        }
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
                Health -= damage;
            }
            if (Health <= 0)
            {
                TheChainedGirl.Begone();
                Destroy(this.gameObject);
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
