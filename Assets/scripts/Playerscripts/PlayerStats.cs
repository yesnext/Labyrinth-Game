using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerStats : MonoBehaviour
{
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
    public bool attackanim = false;
    public bool element = false; // if false it will be fire if tru will be ice
    public KeyCode SwitchElement;
    public UniversalEnemyNeeds Boss;
    public BoxCollider2D attackbox;
    public BoxCollider2D fistmode;
    public KeyCode FightMode;
    public bool ThrowingHands = false;
    public bool firstencounter = true;
    private float freezintimeduration = 5f;

    // void Awake()
    // {
    //     // if (instance != null && instance != this)
    //     // {
    //     //     Destroy(gameObject);
    //     // }
    //     // else
    //     // {

    //     //     instance = this;
    //     //     DontDestroyOnLoad(gameObject);
    //     // }
    // }
    void Start()
    {
        ProjectilePoint = FindObjectOfType<ProjectilePoint>().transform;
        controls = GetComponent<controls>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!GetComponent<controls>().frozen)
        {
            if (Input.GetKeyDown(MeleeAttackkey))
            {

                MeleeAnimation();
            }
            if (Input.GetKeyDown(SwitchElement))
            {
                if (!ThrowingHands)
                {
                    ChangeElement();
                }
            }
            if (Input.GetKeyDown(RangeAttackKey))
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
                        Shoot();
                    }
                }
            }
            if (Input.GetKeyDown(FightMode))
            {
                ChangeFightMode();
            }
        }
    }

    private void MeleeAnimation()
    {
        //decide which animation to run
        if (ThrowingHands)
        {

        }
        else
        {

        }
    }

    private void ChangeFightMode()
    {
        ThrowingHands = !ThrowingHands;
    }

    private void ChangeElement()
    {
        element = !element;
    }

    public void Shoot()
    {
        if (!element)
        {
            GameObject projectile = Instantiate(Projectile[0], transform.position, Quaternion.identity);
            PlayerProjectile projectileController = projectile.GetComponent<PlayerProjectile>();
            projectileController.Intialize(ProjectilePoint);
        }
        else
        {
            GameObject projectile = Instantiate(Projectile[1], transform.position, Quaternion.identity);
            PlayerProjectile projectileController = projectile.GetComponent<PlayerProjectile>();
            projectileController.Intialize(ProjectilePoint);
        }
    }
    public void TakeDamage(int damage)
    {
        Health = Health - damage;
        Debug.Log("Health" + Health);
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

    public void MeleeAttack()
    {

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
                    else if (GameObject.FindObjectOfType<IgrisController>() != null)
                    {
                        if (ThrowingHands && !Boss.GetComponent<IgrisController>().IsImmune)
                        {
                            other.GetComponent<IgrisController>().TakeDamage(MeleeAttackDamage);
                        }
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
                        if (other.GetComponent<HealingOrion>().agrue)
                        {
                            other.GetComponent<HealingOrion>().TakeDamage(MeleeAttackDamage);
                        }
                    }
                    else if (GameObject.FindObjectOfType<MonarchOfTimeController>() != null)
                    {
                        FindObjectOfType<MonarchOfTimeController>().TakeDamage(MeleeAttackDamage);
                        Destroy(this.gameObject);
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
                    PlayerStats.ProjectileCount--;
                }

            }

        }
    }
    public void Heal()
    {
        if (Time.time - LastHealCooldown > HealCooldown)
        {
            Health += 40;
            if (Health > 100)
            {
                Health = 100;
            }
            LastHealCooldown = Time.time;
        }

    }

}