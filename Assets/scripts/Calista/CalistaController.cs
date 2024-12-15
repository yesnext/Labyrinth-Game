using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class CalistaController : UniversalEnemyNeeds
{
    public float PointBlank = 2.0f;
    protected float LastRangAttackTime;
    protected float RangAttackCooldown = 2.0f;
    public EnemyProjectile Projectile;
    private Transform ProjectilePoint;

    public float DoubleDamageTime = 0.0f;
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        ProjectilePoint = FindObjectOfType<EnemyProjectilePoint>().transform;
        if(player.GetComponent<BossesDefeated>().Calista){
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (aggro)
        {
            ChangedDirectionFollow();
            if (!MeleeAttacking && !RangAttacking)
            {
                Followplayer();
            }
            if (Time.time - LastRangAttackTime > RangAttackCooldown && distance > PointBlank && !MeleeAttacking && !RangAttacking)
            {
                StartCoroutine(Shoot());
            }
            else if (distance < meleeattackdistance && Time.time - lastMeleeAttackTime > MeleeAttackCooldown && !MeleeAttacking && !RangAttacking)
            {
                StartCoroutine(MeleeAttack());
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
    public IEnumerator MeleeAttack()
    {
        MeleeAttacking = true;
        yield return new WaitForSeconds(MeleeAttackAnimationDuration);
        MeleeAttacking = false;
        lastMeleeAttackTime = Time.time;
    }
    public override void TakeDamage(int damage)
    {
        if (aggro)
        {
            if (UnityEngine.Random.Range(1, 101) > 29)
            {
                Health = Health - damage;
                if (Health <= 0)
                {
                    player.GetComponent<BossesDefeated>().Calista = true;
                    Destroy(this.gameObject);
                }
            }
            else
            {
                Health += damage;
            }
            DoubleDamageTime = Time.time;
        }
    }
    public IEnumerator Shoot()
    {
        RangAttacking = true;
        yield return new WaitForSeconds(RangAttackAnimationDuration);
        EnemyProjectile projectile = Instantiate(Projectile, ProjectilePoint.position, ProjectilePoint.rotation);
        EnemyProjectile projectileController = projectile.GetComponent<EnemyProjectile>();
        if (Time.time < DoubleDamageTime + 10)
        {
            projectileController.Intialize(RangeAttackDamage * 2, RangeAttackSpeed);
        }
        else
        {
            projectileController.Intialize(RangeAttackDamage, RangeAttackSpeed);
        }
        RangAttacking = false;
        LastRangAttackTime = Time.time;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (attackbox.enabled)
            {
                if (Time.time < DoubleDamageTime + 10)
                    player.TakeDamage(MeleeAttackDamage * 2);
                else
                    player.TakeDamage(MeleeAttackDamage);

            }
        }
    }
}
