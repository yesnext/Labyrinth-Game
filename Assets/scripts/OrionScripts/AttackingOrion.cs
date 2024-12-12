using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingOrion : UniversalEnemyNeeds
{
    private HealingOrion healingorion;

    // Start is called before the first frame update
    void Start()
    {
        IsImmune = true;
        healingorion = FindObjectOfType<HealingOrion>();
        player = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        Followplayer();
        ChangedDirectionFollow();
        if (healingorion == null)
        {
            IsImmune = false;
        }
        if(distance<meleeattackdistance && Time.time-lastMeleeAttackTime>MeleeAttackCooldown){
            Meleeattack();
        }
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
    }
    public void Meleeattack(){
        // for animation
        lastMeleeAttackTime=Time.time;
    }
    public override void TakeDamage(int damage)
    {
        if (!IsImmune)
        {
            Health = Health - damage;
            if (Health <= 0)
            {
                Destroy(this.gameObject);
            }
        }

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Time.time - lastMeleeAttackTime > MeleeAttackCooldown)
            {
                other.GetComponent<PlayerStats>().TakeDamage(MeleeAttackDamage);
            }
        }
    }
}
