using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using UnityEngine;

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
    public BasicEnemy enemy;
    public FinalBossController Boss;
    public BoxCollider2D attackbox;
    public BoxCollider2D fistmode;
    public KeyCode FightMode;
    public bool ThrowingHands = false;
    public bool firstencounter = true;
    private static PlayerStats instance;

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
    public void BasicMeleeAttack()
    {
        if (Time.time - lastMeleeAttackTime > MeleeAttackCooldown)
        {
            lastMeleeAttackTime = Time.time;
            enemy.TakeDamage(MeleeAttackDamage, element);
            // animator.SetTrigger("Attack");
        }
    }
    public void BossMeleeAttack()
    {
        if (Time.time - lastMeleeAttackTime > MeleeAttackCooldown)
        {
            lastMeleeAttackTime = Time.time;
            if (GameObject.FindObjectOfType<IgrisController>() == null)
            {
                Boss.GetComponent<FinalBossController>().TakeDamage(MeleeAttackDamage);
                // animator.SetTrigger("Attack");
            }
            else
            {
                Debug.Log(Boss.GetComponent<FinalBossController>().IsImmune);
                if (ThrowingHands && !Boss.GetComponent<FinalBossController>().IsImmune)
                {
                    Boss.GetComponent<FinalBossController>().TakeDamage(MeleeAttackDamage);
                }
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemy = other.GetComponent<BasicEnemy>();
            BasicMeleeAttack();
        }
        else if (other.tag == "Boss")
        {
            if (attackbox.enabled)
            {
                if (GameObject.FindObjectOfType<IgrisController>() == null)
                {
                    Boss = other.GetComponent<FinalBossController>();
                    BossMeleeAttack();
                }
                else
                {
                    Boss = other.GetComponent<IgrisController>();
                    BossMeleeAttack();
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