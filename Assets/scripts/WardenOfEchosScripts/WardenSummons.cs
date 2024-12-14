using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenSummons : UniversalEnemyNeeds
{
    // Start is called before the first frame update
    public float strongercooldown = 2.0f;
    public float laststronger;
    public int strongerdamageamount;
    void Start()
    {
        laststronger = Time.time;
        player = FindObjectOfType<PlayerStats>();
    }

    void Update()
    {
        if (!MeleeAttacking)
        {
            Followplayer();
        }
        TheLongerTheyLive();
        if (Time.time - lastMeleeAttackTime > MeleeAttackCooldown && distance < meleeattackdistance && !MeleeAttacking)
        {
            StartCoroutine(meleeAttack());
        }
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
    }
    public IEnumerator meleeAttack()
    {
        MeleeAttacking = true;
        yield return new WaitForSeconds(MeleeAttackAnimationDuration);
        MeleeAttacking = false;
        lastMeleeAttackTime = Time.time;
    }
    public void TheLongerTheyLive()
    {
        if (Time.time - laststronger > strongercooldown)
        {
            laststronger = Time.time;
            MeleeAttackDamage += strongerdamageamount;
        }
    }
    public void getdestroyed()
    {
        Destroy(this.gameObject);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (attackbox.enabled)
            {
                player.TakeDamage(MeleeAttackDamage);
            }
        }
    }

}
