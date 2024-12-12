using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class CalistaController : UniversalEnemyNeeds
{
    public float PointBlank = 2.0f;
    protected float LastRangAttackTime;
    protected float RangAttackCooldown = 2.0f;
    public GameObject Projectile;
    private Transform ProjectilePoint;
    public int RangeAttackDamage = 3;
    public float DoubleDamageTime=0.0f;
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        ProjectilePoint = FindObjectOfType<EnemyProjectilePoint>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        GhangedirectionFollow();
        Followplayer();
        if (Time.time - LastRangAttackTime > RangAttackCooldown && distance > PointBlank && false)
        {
            Shoot();
        }
        else if (distance < meleeattackdistance && Time.time - lastMeleeAttackTime > MeleeAttackCooldown)
        {
            MeleeAttack();//location to trigger animation
        }

    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
        
    }
    public void MeleeAttack()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (Time.time - lastMeleeAttackTime > MeleeAttackCooldown)
        {
            EnemySpeed = OriginalSpeed;
            if(Time.time<DoubleDamageTime+10)
            player.TakeDamage(MeleeAttackDamage*2);
            else
            player.TakeDamage(MeleeAttackDamage);
        }
        lastMeleeAttackTime = Time.time;
    }
    public override void TakeDamage(int damage)
    {
        if (Random.Range(1, 101) > 29)
        {
            Health = Health - damage;
            if (Health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Health += damage;
        }
        DoubleDamageTime=Time.time;
    }
    public void Shoot()
    {
        LastRangAttackTime = Time.time;
        GameObject projectile = Instantiate(Projectile, ProjectilePoint.position, ProjectilePoint.rotation);
        EnemyProjectile projectileController = projectile.GetComponent<EnemyProjectile>();
        if(DoubleDamageTime<DoubleDamageTime+10){
        projectileController.Intialize(RangeAttackDamage*2);
        }else{
        projectileController.Intialize(RangeAttackDamage);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (attackbox.enabled)
            {
                player = other.GetComponent<PlayerStats>();
                if (Time.time - lastMeleeAttackTime > MeleeAttackCooldown)
                {
                    MeleeAttack();
                }
            }
        }
    }
}
