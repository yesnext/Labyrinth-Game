using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AriseEnemies : UniversalEnemyNeeds
{
    protected Animator animator;
    public bool Element = false;
    public float playerFolowDistance = 10.0f;
    public short enemystate;
    public int decision;
    public float decisioncooldown = 3f;
    public float lastdecisioncooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!MeleeAttacking){
        Followplayer();
        }
        if (Time.time - lastMeleeAttackTime > MeleeAttackCooldown && distance < meleeattackdistance && !MeleeAttacking)
        {
            StartCoroutine(meleeAttack());
        }
    }
    public override void TakeDamage(int damage)
    {
        Health = Health - damage;
        if (Health <= 0)
        {
            Destroy(this.gameObject);
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
