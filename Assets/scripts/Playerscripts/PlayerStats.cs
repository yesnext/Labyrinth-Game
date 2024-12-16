using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerStats : MonoBehaviour
{
    public bool unlockedIce;
    public controls controls;
    public Transform ProjectilePoint;
    public GameObject[] Projectile = new GameObject[2];
    public static int ProjectileCount = 0;
    public KeyCode RangeAttackKey;
    public Vector3 direction;
    public Vector3 mousePosition;
    public int Health = 100;
    public float HealCooldown = 10.0f;
    public float LastHealCooldown = 10.0f;
    protected Animator animator;
    public KeyCode MeleeAttackkey;
    public float MeleeAttackDistance = 5.0f;
    public int MeleeAttackDamage = 6;
    public float MeleeAttackCooldown = 3.0f; //time in seconds for the cooldown
    private float lastMeleeAttackTime = 0f;
    private bool meleeattacking;
    public float ThrowingHandsAnimationDuration;
    public float ElementMeleeAttackAnimationDuration;
    public bool attackanim = false;
    public bool element = false; // if false it will be fire if tru will be ice
    public KeyCode SwitchElement;
    public UniversalEnemyNeeds Boss;
    public BoxCollider2D attackbox;
    public KeyCode FightMode;
    public KeyCode Healing;
    public bool ThrowingHands = false;
    public bool firstencounter = true;
    public float rangeattaccooldown;
    public float lastrangeattack;
    public int RangeAttackDamage;
    public float RangeAttackSpeed;
    private bool Rangeattacking;
    public float FireRangeAttackAnimationDuration;
    public float IceRangeAttackAnimationDuration;
    private static PlayerStats instance;
    public AudioSource audioSource;
    public AudioClip meleeAttackClip1;
    public AudioClip meleeAttackClip2;
    public AudioClip FireRangeAttackClip;
    public AudioClip IceRangeAttackClip;
    public AudioClip healClip;
    //bob addition
    public HealthBar healthbar;

    void Awake()
    {

        if (instance != null && instance != this)
        {
            instance.transform.position = this.transform.position;
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        ProjectilePoint = FindObjectOfType<ProjectilePoint>().transform;
        controls = GetComponent<controls>();
        animator = GetComponent<Animator>();
        lastrangeattack = -rangeattaccooldown;
        audioSource = GetComponent<AudioSource>();
        //bob addition
        //healthbar.SetMaxHealth(Health);


    }

    void Update()
    {
        if (!GetComponent<controls>().frozen)
        {
            if (Input.GetKeyDown(MeleeAttackkey))
            {
                if (Time.timeAsDouble - lastMeleeAttackTime > MeleeAttackCooldown && !meleeattacking && !Rangeattacking)
                {
                    StartCoroutine(MeleeAnimation());
                }
            }
            if (Input.GetKeyDown(SwitchElement) && !Rangeattacking)
            {
                if (!ThrowingHands)
                {
                    ChangeElement();
                }
            }
            if (Input.GetKeyDown(RangeAttackKey) && !meleeattacking && !Rangeattacking)
            {
                if (Time.time - lastrangeattack > rangeattaccooldown)
                {
                    if (!ThrowingHands)
                    {
                        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        mousePosition.z = 0;
                        direction = (mousePosition - ProjectilePoint.position).normalized;
                        bool IsMouseOnRight = mousePosition.x > transform.position.x;
                        if (controls.isFacingRight == IsMouseOnRight)
                        {
                            ProjectileCount++;
                            StartCoroutine(Shoot());
                        }
                    }
                }
            }
            if (Input.GetKeyDown(FightMode) && !meleeattacking && !Rangeattacking)
            {
                ChangeFightMode();
            }
            if (Input.GetKeyDown(Healing) && !meleeattacking && !Rangeattacking)
            {
                Heal();
            }
        }
    }

    private IEnumerator MeleeAnimation()
    {
        meleeattacking = true;
        //decide which animation to run
        if (ThrowingHands)
        {
             
            yield return new WaitForSeconds(ThrowingHandsAnimationDuration);
        }
        else if (!element)// for fire
        {
             audioSource.PlayOneShot(meleeAttackClip1);
            yield return new WaitForSeconds(ElementMeleeAttackAnimationDuration);
        }
        else if (unlockedIce && element)
        {
             audioSource.PlayOneShot(meleeAttackClip2);
            yield return new WaitForSeconds(ElementMeleeAttackAnimationDuration);
        }
        meleeattacking = false;
        lastMeleeAttackTime = Time.time;
    }
    public IEnumerator Shoot()
    {
        Rangeattacking = true;
        if (!element)//for fire
        {
            audioSource.PlayOneShot(FireRangeAttackClip);
            yield return new WaitForSeconds(FireRangeAttackAnimationDuration);
            GameObject projectile = Instantiate(Projectile[0], transform.position, Quaternion.identity);
            PlayerProjectile projectileController = projectile.GetComponent<PlayerProjectile>();
            projectileController.Intialize(ProjectilePoint, RangeAttackDamage, RangeAttackSpeed);
        }
        else if (unlockedIce && element)
        {
            audioSource.PlayOneShot(IceRangeAttackClip);
            yield return new WaitForSeconds(IceRangeAttackAnimationDuration);
            GameObject projectile = Instantiate(Projectile[1], transform.position, Quaternion.identity);
            PlayerProjectile projectileController = projectile.GetComponent<PlayerProjectile>();
            projectileController.Intialize(ProjectilePoint, RangeAttackDamage, RangeAttackSpeed);
        }
        Rangeattacking = false;
        lastrangeattack = Time.time;
    }
    private void ChangeFightMode()
    {
        ThrowingHands = !ThrowingHands;
    }

    private void ChangeElement()
    {
        if (unlockedIce)
        {
            element = !element;
        }
    }
    public void TakeDamage(int damage)
    {
        Health = Health - damage;
        Debug.Log("Health" + Health);
        healthbar.SetHealth(Health);

    }
    public void TakeDamagefromigris(int damage)
    {
        Health = Health - damage;
        Debug.Log("Health" + Health);
        if (Health <= 0 && firstencounter)
        {
            firstencounter = false;
            Health = 100; ;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.time - lastMeleeAttackTime > MeleeAttackCooldown)
        {
            if (attackbox.enabled)
            {
                if (other.tag == "Enemy")
                {
                    other.GetComponent<BasicEnemy>().TakeDamage(MeleeAttackDamage, element);

                }
                else if (other.tag == "Boss")
                {

                    if (GameObject.FindObjectOfType<FinalBossController>() != null)
                    {
                        other.GetComponent<FinalBossController>().TakeDamage(MeleeAttackDamage);

                    }
                    else if (GameObject.FindObjectOfType<AshenStalkerController>() != null)
                    {
                        other.GetComponent<AshenStalkerController>().TakeDamage(MeleeAttackDamage);
                    }
                    else if (GameObject.FindObjectOfType<CalistaController>() != null)
                    {
                        other.GetComponent<CalistaController>().TakeDamage(MeleeAttackDamage);
                    }
                    else if (GameObject.FindObjectOfType<HealingOrion>() != null)
                    {
                        if (other.GetComponent<HealingOrion>().aggro)
                        {
                            other.GetComponent<HealingOrion>().TakeDamage(MeleeAttackDamage);
                        }
                    }
                    else if (GameObject.FindObjectOfType<MonarchOfTimeController>() != null)
                    {
                        other.GetComponent<MonarchOfTimeController>().TakeDamage(MeleeAttackDamage);

                    }
                    else if (GameObject.FindObjectOfType<WyvernControler>() != null)
                    {
                        other.GetComponent<WyvernControler>().TakeDamage(MeleeAttackDamage);

                    }
                    else if (GameObject.FindObjectOfType<SeraphineControler>() != null)
                    {
                        other.GetComponent<SeraphineControler>().TakeDamage(MeleeAttackDamage);
                    }
                }
                else if (GameObject.FindObjectOfType<IgrisController>() != null && other.tag == "Boss")
                {
                    if (ThrowingHands && !Boss.GetComponent<IgrisController>().IsImmune)
                    {
                        other.GetComponent<IgrisController>().TakeDamage(MeleeAttackDamage);
                    }
                }
                else if (other.tag == "Chains")
                {
                    other.GetComponent<Chains>().TakeDamage(MeleeAttackDamage, element);
                }
                else if (other.tag == "Obelisk")
                {
                    if (WardenObelisks.state)
                    {
                        other.GetComponent<WardenObelisks>().TakeDamage(MeleeAttackDamage);
                    }
                }
                else if (GameObject.FindObjectOfType<AttackingOrion>() != null && other.tag == "AttackOrion")
                {
                    other.GetComponent<AttackingOrion>().TakeDamage(MeleeAttackDamage);
                }
                else if (other.tag == "Without element or Phases")
                {
                    other.GetComponent<UniversalEnemyNeeds>().TakeDamage(MeleeAttackDamage);
                }
            }
        }
    }
    public void Heal()
    {
        if (FindObjectOfType<FinalBossController>() == null && FindObjectOfType<FinalBossController>().BossPhase == 1)
        {
            if (Time.time - LastHealCooldown > HealCooldown)
            {
                Health += 40;
                audioSource.PlayOneShot(healClip);
                if (Health > 100)
                {
                    Health = 100;
                }
                LastHealCooldown = Time.time;
            }
        }

    }

}