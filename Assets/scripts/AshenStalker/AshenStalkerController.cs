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
    public float CurrentSpeed;
    public SpriteRenderer Disappear;
    public float appeardistance = 1;
    public GameObject minions;
    public GameObject Projectile;
    private SummonsSpawnLocation[] SpawnLocation;
    private Transform ProjectilePoint;
    private Rigidbody2D ShadoStep;
    private Animator anim;
    private BossesDefeated bs;

    // Start is called before the first frame update
    void Start()
    {
        bs = FindObjectOfType<BossesDefeated>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerStats>();
        Disappear = GetComponent<SpriteRenderer>();
        ShadoStep = GetComponent<Rigidbody2D>();
        ProjectilePoint = FindObjectOfType<EnemyProjectilePoint>().transform;
        SpawnLocation = FindObjectsOfType<SummonsSpawnLocation>();
        OriginalSpeed=EnemySpeed;
        if(player.GetComponent<BossesDefeated>().AshenStalker){
            Destroy(this.gameObject);
        }
        OriginalSpeed = EnemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentSpeed = ShadoStep.velocity.magnitude;
        anim.SetFloat("Speed",CurrentSpeed);
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
        //anim
        int rnd = Random.Range(0, 3);
        switch(rnd){
            case 0:
                anim.SetInteger("attackSelect",rnd);
                break;
            case 1:
                anim.SetInteger("attackSelect",rnd);
                break;
            case 2:
                anim.SetInteger("attackSelect",rnd);
                break;
        }
        yield return new WaitForSeconds(MeleeAttackAnimationDuration);
        EnemySpeed = OriginalSpeed;
        IsLunging = false;
        lastMeleeAttackTime = Time.time;
        MeleeAttacking = false;
    }
    public IEnumerator Shoot()
    {
        RangAttacking = true;
        //anim
        anim.SetBool("isShoot",RangAttacking);
        yield return new WaitForSeconds(RangAttackAnimationDuration);
        GameObject projectile = Instantiate(Projectile, ProjectilePoint.position, ProjectilePoint.rotation);
        EnemyProjectile projectileController = projectile.GetComponent<EnemyProjectile>();
        projectileController.Intialize(RangeAttackDamage, RangeAttackSpeed);
        LastRangAttackTime = Time.time;
        RangAttacking = false;
        anim.SetBool("isShoot",RangAttacking);
    }
    public override void TakeDamage(int damage)
    {
        if (aggro)
        {
            Health = Health - damage;
            if (Health <= 0)
            {
                player.GetComponent<BossesDefeated>().AshenStalker = true;
                if(bs.AshenStalker){
                    anim.SetBool("IsDead",bs.AshenStalker);

                }
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
