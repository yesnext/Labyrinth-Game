using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FinalBossController : UniversalEnemyNeeds
{
    
    private Animator animator;

    protected float BossDodgeSpeed = 8f;
    protected float dodgeDuration = 0.1f;
    protected float DodgeDurationCounter = 0.0f;
    public bool dodge;
    protected Transform ProjectilePoint;
    public GameObject Projectile;
    public int BossPhase = 1;

    protected float RangeAttackDistance = 5.0f;
    protected float RangAttackCooldown = 5f;
    protected float LastRangAttackTime = -5.0f;
    protected bool Rangeanim;
    protected bool lunganim;


    protected float ShadoAttackDistance = 4.0f;
    protected int ShadoAttackDamage = 6;
    protected float ShadoSwordSlashesCooldown = 5f; //time in seconds for the cooldown
    protected float lastShadoSwordSlashesTime = -5.0f;
    protected float AriseCooldown = 45.0f;
    protected float LastArisetime = -45.0f;
    protected float spwanduration = 10f;
    protected float lastspawnduration = 0;
    protected float Timebetweenspawns = 1f;
    protected float LastTimebetweenspawns = 0f;
    public AriseEnemies[] ariseEnemies = new AriseEnemies[10];
    public Transform SpawnLocation;
    public BoxCollider2D BodyBox;
    protected bool onetime = true;
    protected bool secondtime = true;


    // Start is called before the first frame update
    void Start()
    {
        OriginalSpeed = EnemySpeed;
        player = FindObjectOfType<PlayerStats>();
        ProjectilePoint = FindObjectOfType<EnemyProjectilePoint>().transform;
        SpawnLocation =FindObjectOfType<SummonsSpawnLocation>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (dodge == true && BossPhase == 1)
        {
            StartCoroutine(Awareofeverymovedodge());
        }
        else if (dodge == true && BossPhase >= 2)
        {
            StartCoroutine(Dodge());
        }
        if (Time.time - LastArisetime > AriseCooldown && BossPhase == 3)
        {
            StartCoroutine(Arise());
        }
        GhangedirectionFollow();
        Followplayer();
        if (distance >= RangeAttackDistance && Time.time - LastRangAttackTime > RangAttackCooldown)
        {
            RangeAttack();
        }
        else if (distance >= LungAttackDistance && Time.time - lastShadoSwordSlashesTime > ShadoSwordSlashesCooldown && Time.time - LastLungAttackTime > LungAttackCooldown)
        {
            LungAttack();

        }
        else if (distance < ShadoAttackDistance && Time.time - lastShadoSwordSlashesTime > ShadoSwordSlashesCooldown)
        {
            ShadoAttack();
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
    }

    public virtual void RangeAttack()
    {
        Instantiate(Projectile, ProjectilePoint.position, ProjectilePoint.rotation);
        LastRangAttackTime = Time.time;
    }
    public void LungAttack()
    {
        if (Time.time - LastLungAttackTime > LungAttackCooldown)
        {
            LastLungAttackTime = Time.time;
            EnemySpeed *= 1.5f;
            if (!IsLunging)
            {
                IsLunging = true;
            }
        }
    }
    public void ShadoAttack()
    {
        // lunganim=false;
        // animator.SetBool("LungAnimation", lunganim);
        // shadoslashanim = true;
        // animator.SetBool("ShadoAnimation", shadoslashanim);
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (Time.time - lastShadoSwordSlashesTime > ShadoSwordSlashesCooldown)
        {
            player.TakeDamage(ShadoAttackDamage);

            if (BossPhase == 1)
            {
                Heal(ShadoAttackDamage);
            }
        }
        lastShadoSwordSlashesTime = Time.time;
        if (IsLunging)
        {
            EnemySpeed = EnemySpeed / 1.5f;
            IsLunging = false;
        }
        // shadoslashanim=false;
        // animator.SetBool("ShadoAnimation", shadoslashanim);
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
        Health = Health - damage;

        if (!IsImmune)
        {
            if (Health <= 0 && BossPhase >= 3)
            {
                Destroy(this.gameObject);
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
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (attackbox.enabled)
        {
            if (other.tag == "Player")
                if (attackbox.enabled)
                {
                    player = other.GetComponent<PlayerStats>();
                    if (Time.time - lastShadoSwordSlashesTime > ShadoSwordSlashesCooldown)
                    {
                        ShadoAttack();
                    }
                }
        }

    }
    public void Heal(int heal)
    {
        Health += heal;
        if (Health > 500)
        {
            Health = 500;
        }
    }
    public IEnumerator Arise()
    {
        LastArisetime = Time.time;
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
                LastTimebetweenspawns = Time.time;
                int randomIndex = Random.Range(0, ariseEnemies.Length);
                Instantiate(ariseEnemies[randomIndex], SpawnLocation.position, SpawnLocation.rotation);
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

    }
}

