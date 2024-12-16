using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalBossController : UniversalEnemyNeeds
{

    private Animator animator;

    public float BossDodgeSpeed = 8f;
    public float dodgeDuration = 0.1f;
    protected float DodgeDurationCounter = 0.0f;
    public bool dodge;
    protected Transform ProjectilePoint;
    public DimentionalSlashProjectile Projectile;
    public int BossPhase = 1;

    public float RangeAttackDistance = 5.0f;
    public float RangAttackCooldown = 5f;
    protected float LastRangAttackTime = -5.0f;

    public float ShadoAttackDistance = 4.0f;
    public int ShadoAttackDamage = 6;
    public float ShadoSwordSlashesCooldown = 5f; //time in seconds for the cooldown
    protected float lastShadoSwordSlashesTime = -5.0f;
    public float AriseCooldown = 45.0f;
    protected float LastArisetime = -45.0f;
    public float spwanduration = 10f;
    protected float lastspawnduration = 0;
    protected float Timebetweenspawns = 1f;
    protected float LastTimebetweenspawns = 0f;
    public AriseEnemies ariseEnemies;
    public Transform SpawnLocation;
    public BoxCollider2D BodyBox;
    protected bool onetime = true;
    protected bool secondtime = true;
    protected bool arise;
    public AudioClip AriseClip;

    //bob addition
    public HealthBar healthbar;

    //bob addition
    private GameObject enemyCanvas;

    // Start is called before the first frame update
    void Start()
    {
        OriginalSpeed = EnemySpeed;
        player = FindObjectOfType<PlayerStats>();
        ProjectilePoint = FindObjectOfType<EnemyProjectilePoint>().transform;
        SpawnLocation = FindObjectOfType<SummonsSpawnLocation>().transform;
        if (player.GetComponent<BossesDefeated>().FinalBoss)
        {
            Destroy(this.gameObject);
        }
        audioSource = GetComponent<AudioSource>();

        //bob addition
        //healthbar.SetMaxHealth(Health);


        //bob addition
        //enemyCanvas = GameObject.FindGameObjectWithTag("EnemyCanvas");
        //enemyCanvas.SetActive(false);  // Hide health bar initially

    }

    // Update is called once per frame
    void Update()
    {
        if (aggro)
        {
            if (dodge == true && BossPhase == 1 && !MeleeAttacking && !RangAttacking)
            {
                StartCoroutine(Awareofeverymovedodge());
            }
            else if (dodge == true && BossPhase >= 2 && !MeleeAttacking && !RangAttacking && !arise)
            {
                StartCoroutine(Dodge());
            }
            if (Time.time - LastArisetime > AriseCooldown && BossPhase == 3 && !MeleeAttacking && !RangAttacking && !arise)
            {
                StartCoroutine(Arise());
            }
            ChangedDirectionFollow();
            Followplayer();
            if (distance >= RangeAttackDistance && Time.time - LastRangAttackTime > RangAttackCooldown && !MeleeAttacking && !RangAttacking && !arise)
            {
                StartCoroutine(RangeAttack());
            }
            else if (distance >= LungAttackDistance && Time.time - lastShadoSwordSlashesTime > ShadoSwordSlashesCooldown && Time.time - LastLungAttackTime > LungAttackCooldown && !MeleeAttacking && !RangAttacking && !arise)
            {
                LungAttack();

            }
            else if (distance < ShadoAttackDistance && Time.time - lastShadoSwordSlashesTime > ShadoSwordSlashesCooldown && !MeleeAttacking && !RangAttacking && !arise)
            {
                StartCoroutine(ShadoAttack());
            }
            //bob addition
            enemyCanvas.SetActive(true);
        }
        else
        {
            // Hide the health bar when not aggro
            //enemyCanvas.SetActive(false);
        }

    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (BossPhase == 1)
        {
            IsImmune = PlayerStats.ProjectileCount < 3;
        }
        if (distance < aggrodistance && !aggro)
        {
            aggro = true;
        }
    }

    public IEnumerator RangeAttack()
    {
        audioSource.PlayOneShot(RangeAttackClip);
        RangAttacking = true;
        yield return new WaitForSeconds(RangAttackAnimationDuration);
        DimentionalSlashProjectile projectile = Instantiate(Projectile, ProjectilePoint.position, ProjectilePoint.rotation);
        DimentionalSlashProjectile projectileController = projectile.GetComponent<DimentionalSlashProjectile>();
        projectileController.Intialize(RangeAttackDamage, RangeAttackSpeed);
        RangAttacking = false;
        LastRangAttackTime = Time.time;
    }
    public void LungAttack()
    {
        LastLungAttackTime = Time.time;
        EnemySpeed *= 1.5f;
        if (!IsLunging)
        {
            IsLunging = true;
        }

    }
    public IEnumerator ShadoAttack()
    {
        audioSource.PlayOneShot(meleeAttackClip1);
        MeleeAttacking = true;
        yield return new WaitForSeconds(MeleeAttackAnimationDuration);
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (IsLunging)
        {
            EnemySpeed = EnemySpeed / 1.5f;
            IsLunging = false;
        }
        lastShadoSwordSlashesTime = Time.time;
        MeleeAttacking = false;
    }

    public IEnumerator Awareofeverymovedodge()
    {
        dodge = false;
        if (IsImmune)
        {
            Vector3 dodgeDirection = (transform.position - player.transform.position).normalized;
            DodgeDurationCounter = 0f;
            EnemySpeed *= 4f;
            while (DodgeDurationCounter < dodgeDuration)
            {
                DodgeDurationCounter += Time.deltaTime;
                yield return null;
            }
            EnemySpeed = OriginalSpeed;
            transform.position += dodgeDirection * BossDodgeSpeed * Time.deltaTime;
        }
    }
    public IEnumerator Dodge()
    {
        Debug.Log("inside dodge");
        dodge = false;
        Vector3 dodgeDirection = (transform.position - player.transform.position).normalized;
        DodgeDurationCounter = 0f;
        float originalspeed = EnemySpeed;
        EnemySpeed *= 4f;
        while (DodgeDurationCounter < dodgeDuration)
        {
            DodgeDurationCounter += Time.deltaTime;
            yield return null;
        }
        IsImmune = false;
        EnemySpeed = originalspeed;
        transform.position += dodgeDirection * BossDodgeSpeed * Time.deltaTime;

    }

    public override void TakeDamage(int damage)
    {
        if (aggro)
        {
            Health = Health - damage;
            //bob addition
            healthbar.SetHealth(Health);
            if (!IsImmune)
            {
                if (Health <= 0 && BossPhase >= 3)
                {
                    foreach (AriseEnemies minions in FindObjectsOfType<AriseEnemies>())
                    {
                        minions.getdestroyed();
                    }
                    player.GetComponent<BossesDefeated>().FinalBoss = true;
                    SceneManager.LoadScene("FinalCurScene");
                    Destroy(this.gameObject);
                    //bob addition
                    Destroy(GameObject.FindGameObjectWithTag("EnemyCanvas"));
                }
                else if (Health <= 0)
                {
                    Health = 500;
                    BossPhase++;
                    if (BossPhase == 2 && onetime)
                    {
                        OriginalSpeed *= 1.5f;
                        onetime = false;
                        IsImmune = false;
                    }
                    else if (BossPhase == 3 && secondtime)
                    {
                        OriginalSpeed /= 1.5f;
                        onetime = false;
                    }
                }
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (attackbox.enabled)
        {
            if (other.tag == "Player")
            {
                if (attackbox.enabled)
                {
                    if (BossPhase == 1)
                    {
                        Heal(ShadoAttackDamage);
                    }
                    player.TakeDamage(ShadoAttackDamage);
                }
            }
        }
    }
    public void Heal(int heal)
    {
        audioSource.PlayOneShot(healClip);
        Health += heal;
        if (Health > 500)
        {
            Health = 500;
        }
    }
    public IEnumerator Arise()
    {
        
        arise = true;
        Rigidbody2D BossRB = GetComponent<Rigidbody2D>();
        RigidbodyConstraints2D originalRB = BossRB.constraints;
        BossRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        BodyBox.enabled = false;
        IsImmune = true;
        DodgeDurationCounter = 0f;
        EnemySpeed = 0f;
        lastspawnduration = 0f;
        while (lastspawnduration < spwanduration)
        {
            
            if (Time.time - LastTimebetweenspawns > Timebetweenspawns)
            {
                audioSource.PlayOneShot(AriseClip);
                LastTimebetweenspawns = Time.time;
                Instantiate(ariseEnemies, SpawnLocation.position, SpawnLocation.rotation);
                lastspawnduration += Timebetweenspawns;
                yield return new WaitForSeconds(Timebetweenspawns);
            }
            else
            {
                lastspawnduration += Time.deltaTime;
                yield return null;
            }
        }
        BossRB.constraints = originalRB;
        BossRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        BodyBox.enabled = true;
        LastArisetime = Time.time;
        IsImmune = false;
        EnemySpeed = OriginalSpeed;
        arise = false;

    }
}

