using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AshenStalkerController : UniversalEnemyNeeds
{
    protected float Summonscooldown = 100;
    protected float summondistance = 15;
    protected float lastSummonscooldown = -100;

    protected float meleeattackdistance;
    protected float lastMeleeAttackTime;
    protected float MeleeAttackCooldown = 3.0f;
    public int MeleeAttackDamage;

    protected float SurpriseMeleeAttackCooldown = 3.0f;
    protected float LastSurpriseMeleeAttackTime;
    protected float SurpriseAttackDistance = 7.0f;

    protected float LastRangAttackTime;
    protected float RangAttackCooldown = 2.0f;
    public int RangeAttackDamage = 3;
    public float RangeAttackDistance = 5;
    protected bool IsLunging;

    public SpriteRenderer Disappear;
    public float appeardistance = 1;
    public GameObject minions;
    public GameObject Projectile;
    public BoxCollider2D attackbox;
    private SummonsSpawnLocatiopn[] SpawnLocation;
    private Transform ProjectilePoint;
    private Rigidbody2D ShadoStep;
    // Start is called before the first frame update
    void Start()
    {

        player = FindObjectOfType<PlayerStats>();
        Disappear = GetComponent<SpriteRenderer>();
        ShadoStep = GetComponent<Rigidbody2D>();
        ProjectilePoint = FindObjectOfType<EnemyProjectilePoint>().transform;
        SpawnLocation = FindObjectsOfType<SummonsSpawnLocatiopn>();
    }

    // Update is called once per frame
    void Update()
    {

        GhangedirectionFollow();
        Followplayer();
        if (distance > SurpriseAttackDistance && Time.time - LastSurpriseMeleeAttackTime > SurpriseMeleeAttackCooldown && 
        Time.time - lastMeleeAttackTime > MeleeAttackCooldown)
        {
            SurpriseAttack();
        }
        else if (distance < meleeattackdistance && Time.time - lastMeleeAttackTime > MeleeAttackCooldown && player.firstencounter)
        {
            MeleeAttack();
        }
        else if (Time.time - LastRangAttackTime > RangAttackCooldown&& distance>SurpriseAttackDistance)
        {
            Shoot();
        }
        if (distance < appeardistance)
        {
            Disappear.enabled = true;
            ShadoStep.gravityScale = 1;
        }
        if(Time.time - lastSummonscooldown>Summonscooldown){
            lastSummonscooldown = Time.time;
            Summon();
        }
    }
public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
        if(EnemySpeed > OriginalSpeed){
            IsImmune = true;
        }
        else{
            IsImmune = false;
        }
    }
    public void Summon()
    {
        lastSummonscooldown = Time.time;
        foreach (SummonsSpawnLocatiopn spawnloc in SpawnLocation){
        Instantiate(minions, spawnloc.transform.position, spawnloc.transform.rotation);
        }
    }
    
    public void SurpriseAttack()
    {
        Disappear.enabled = false;
        ShadoStep.gravityScale = 0;
        if (Time.time - LastSurpriseMeleeAttackTime > SurpriseMeleeAttackCooldown)
        {
            LastSurpriseMeleeAttackTime = Time.time;
            EnemySpeed *= 1.5f;
            Debug.Log(EnemySpeed);
            if (!IsLunging)
            {
                IsLunging = true;
            }
        }
    }
    public void MeleeAttack()
    {
        Disappear.enabled = true;
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (Time.time - lastMeleeAttackTime > MeleeAttackCooldown)
        {
            EnemySpeed = OriginalSpeed;
            IsLunging = false;
            player.TakeDamage(MeleeAttackDamage);
        }
        lastMeleeAttackTime = Time.time;
    }
    public void Shoot()
    {
        LastRangAttackTime = Time.time;
        GameObject projectile = Instantiate(Projectile, ProjectilePoint.position, ProjectilePoint.rotation);
        EnemyProjectile projectileController = projectile.GetComponent<EnemyProjectile>();
        projectileController.Intialize(RangeAttackDamage);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
            if (other.tag == "Player")
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
