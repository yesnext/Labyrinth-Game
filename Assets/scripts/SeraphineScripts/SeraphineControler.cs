using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class SeraphineControler : UniversalEnemyNeeds
{
    public int Bossphase;
    
    // Start is called before the first frame update
    void Start()
    {
        player =FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        Followplayer();
        ChangedDirectionFollow();
        if(distance<meleeattackdistance && Time.time -lastMeleeAttackTime>meleeattackdistance){

        }
    }
    public void MeleeAttack()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (Time.time - lastMeleeAttackTime > MeleeAttackCooldown)
        {
            EnemySpeed = OriginalSpeed;
        }
        lastMeleeAttackTime = Time.time;
    }
    public override void TakeDamage(int damage)
    {
        Health = Health - damage;
        if (Health <= 0)
        {
            if (Bossphase == 2)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.TakeDamage(MeleeAttackDamage);
        }
    }
}
