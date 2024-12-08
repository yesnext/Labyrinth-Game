using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FinalBossController : MonoBehaviour
{
    public PlayerStates player;
    public bool isFacingRight = true;
    public float EnemySpeed = 0.1f;
    public int Health = 500;
    private Animator animator;
    public float distance;

    public float BossDodgeSpeed = 8f;
    public float dodgeDuration = 0.1f;
    public float DodgeDurationCounter = 0.0f;
    public bool dodge;

    public bool IsImmune = true;
    public Vector3 scale;
    public Vector2 direction;


    public Transform ProjectilePoint;
    public GameObject Projectile;
    public int BossPhase = 1;

    public float RangeAttackDistance = 5.0f;
    public float RangAttackCooldown = 5f;
    private float LastRangAttackTime = -5.0f;
    public bool Rangeanim;

    public float LungAttackDistance = 10.0f;
    public float LungAttackCooldown = 5f; //time in seconds for the cooldown
    private float LastLungAttackTime = -5.0f;
    private bool IsLunging = false;
    public bool lunganim;


    public float ShadoAttackDistance = 4.0f;
    public int ShadoAttackDamage = 6;
    public float ShadoSwordSlashesCooldown = 5f; //time in seconds for the cooldown
    public float lastShadoSwordSlashesTime = -5.0f;
    public bool shadowslashenabled;
    public bool shadoslashanim;

    public float AriseCooldown = 45.0f;
    public float LastArisetime = -45.0f;
    public float spwanduration = 10f;
    public float lastspawnduration = 0;
    public float Timebetweenspawns = 1f;
    public float LastTimebetweenspawns = 0f;
    public AriseEnemies[] ariseEnemies = new AriseEnemies[10];
    public Transform SpawnLocation;
    public bool disable = false;
    public BoxCollider2D attackbox;
    public BoxCollider2D BodyBox;
    public bool onetime = true;
    public float originalSpeed;


    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = EnemySpeed;
        player = FindObjectOfType<PlayerStates>();

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
        if (Time.time - LastArisetime > AriseCooldown && BossPhase == 3 )
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
            IsImmune = PlayerStates.ProjectileCount < 3;
        }
    }

    public void RangeAttack()
    {

        // Rangeanim = true;
        // animator.SetBool("RangeAnimation", Rangeanim);

        Instantiate(Projectile, ProjectilePoint.position, ProjectilePoint.rotation);

        LastRangAttackTime = Time.time;
        // Rangeanim = false;
        // animator.SetBool("RangeAnimation", Rangeanim);
    }
    public void LungAttack()
    {
        if (Time.time - LastLungAttackTime > LungAttackCooldown)
        {
            LastLungAttackTime = Time.time;
            EnemySpeed *= 1.5f;
            IsLunging = true;
            // lunganim = true;
            // animator.SetBool("LungAnimation", lunganim);
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
            EnemySpeed = originalSpeed;
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

    public new void Followplayer()
    {
        // animator.SetBool("Walking", true);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, EnemySpeed * Time.deltaTime);

        GhangedirectionFollow();
        if (distance < 4.0f)
        {
            // animator.SetBool("Walking", false);
            // animator.SetBool("attack",true);
        }
        else
        {
            // animator.SetBool("attack",false);
        }
    }
    public void GhangedirectionFollow()
    {
        if (direction.x > 0 && !isFacingRight)
        {

            scale = transform.localScale;
            scale.x *= -1;
            isFacingRight = true;
            transform.localScale = scale;

        }
        else if (direction.x < 0 && isFacingRight)
        {
            scale = transform.localScale;
            scale.x *= -1;
            isFacingRight = false;
            transform.localScale = scale;
        }
    }
    public void TakeDamage(int damage)
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
                    originalSpeed *= 1.5f;
                    onetime = false;
                    IsImmune = false;
                }
                if (BossPhase == 3 && onetime)
                {
                    originalSpeed /= 1.5f;
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
                    player = other.GetComponent<PlayerStates>();
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
        EnemySpeed = originalSpeed;

    }
}

