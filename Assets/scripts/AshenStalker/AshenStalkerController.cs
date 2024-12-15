using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AshenStalkerController : UniversalEnemyNeeds
{
    public float Summonscooldown = 100;
    protected float summondistance = 15;
    protected float lastSummonscooldown = -100;

    public float SurpriseMeleeAttackCooldown = 3.0f;
    protected float LastSurpriseMeleeAttackTime;
    protected float SurpriseAttackDistance = 7.0f;

    protected float LastRangAttackTime;
    public float RangAttackCooldown = 2.0f;
    public float RangeAttackDistance = 5;
    public SpriteRenderer Disappear;
    public float appeardistance = 1;
    public GameObject minions;
    public GameObject Projectile;
    private SummonsSpawnLocation[] SpawnLocation;
    private Transform ProjectilePoint;
    private Rigidbody2D ShadoStep;

    // Start is called before the first frame update
    void Start()
    {

        player = FindObjectOfType<PlayerStats>();
        Disappear = GetComponent<SpriteRenderer>();
        ShadoStep = GetComponent<Rigidbody2D>();
        ProjectilePoint = FindObjectOfType<EnemyProjectilePoint>().transform;
        SpawnLocation = FindObjectsOfType<SummonsSpawnLocation>();
        if(player.GetComponent<BossesDefeated>().AshenStalker){
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
            if (distance < SurpriseAttackDistance && Time.time - LastSurpriseMeleeAttackTime > SurpriseMeleeAttackCooldown &&
            Time.time - lastMeleeAttackTime > MeleeAttackCooldown && !IsLunging && !MeleeAttacking && !RangAttacking)
        {
            SurpriseAttack();
        }
            else if (distance < meleeattackdistance && Time.time - lastMeleeAttackTime > MeleeAttackCooldown && !MeleeAttacking && !RangAttacking)
        {
                StartCoroutine(MeleeAttack());
        }
            else if (Time.time - LastRangAttackTime > RangAttackCooldown && distance > SurpriseAttackDistance && !IsLunging && !MeleeAttacking && !RangAttacking)
        {
                StartCoroutine(Shoot());
        }
        if (distance < appeardistance)
        {
            Disappear.enabled = true;
            ShadoStep.gravityScale = 1;
        }
        if (Time.time - lastSummonscooldown > Summonscooldown)
        {
            lastSummonscooldown = Time.time;
            Summon();
            }
        }
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (EnemySpeed > OriginalSpeed)
        {
            IsImmune = true;
        }
        else
        {
            IsImmune = false;
        }
        if (distance < aggrodistance && !aggro)
        {
            aggro = true;
        }
    }
    public void Summon()
    {
        lastSummonscooldown = Time.time;
        foreach (SummonsSpawnLocation spawnloc in SpawnLocation)
        {
            if (!spawnloc.ocupied)
            {
                GameObject minion = Instantiate(minions, spawnloc.transform.position, spawnloc.transform.rotation);
                RangedAttackEnemies minioncontroller = minion.GetComponent<RangedAttackEnemies>();
                minioncontroller.Intialize(spawnloc);
            }
        }
    }

    public void SurpriseAttack()
    {
        IsLunging = true;
        Disappear.enabled = false;
        ShadoStep.gravityScale = 0;
            LastSurpriseMeleeAttackTime = Time.time;
            EnemySpeed *= 1.5f;

    }
    public IEnumerator MeleeAttack()
    {
        MeleeAttacking = true;
        Disappear.enabled = true;
        yield return new WaitForSeconds(MeleeAttackAnimationDuration);
            EnemySpeed = OriginalSpeed;
            IsLunging = false;
        lastMeleeAttackTime = Time.time;
        MeleeAttacking = false;
    }
    public IEnumerator Shoot()
    {
        RangAttacking = true;
        yield return new WaitForSeconds(RangAttackAnimationDuration);
        GameObject projectile = Instantiate(Projectile, ProjectilePoint.position, ProjectilePoint.rotation);
        EnemyProjectile projectileController = projectile.GetComponent<EnemyProjectile>();
        projectileController.Intialize(RangeAttackDamage, RangeAttackSpeed);
        LastRangAttackTime = Time.time;
        RangAttacking = false;
    }
    public override void TakeDamage(int damage)
    {
        if (aggro)
        {
            Health = Health - damage;
            if (Health <= 0)
            {
                player.GetComponent<BossesDefeated>().AshenStalker = true;
                Destroy(this.gameObject);
            }
        }
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
