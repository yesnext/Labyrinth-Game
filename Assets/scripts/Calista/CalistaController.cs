using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalistaController : UniversalEnemyNeeds
{
    public float PointBlank = 2.0f;
    protected float LastRangAttackTime;
    protected float RangAttackCooldown = 2.0f;
    public EnemyProjectile Projectile;
    private Transform ProjectilePoint;

    public float DoubleDamageTime = 0.0f;

    //bob addition
    public HealthBar healthbar;

    //bob addition
    private GameObject enemyCanvas;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerStats>();
        ProjectilePoint = FindObjectOfType<EnemyProjectilePoint>().transform;

        if (player.GetComponent<BossesDefeated>().Calista)
        {
            Destroy(this.gameObject);
        }
        audioSource = GetComponent<AudioSource>();

        //bob addition
        healthbar.SetMaxHealth(Health);

        //bob addition
        enemyCanvas = GameObject.FindGameObjectWithTag("EnemyCanvas");
        enemyCanvas.SetActive(false); // Hide health bar initially
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

            if (Time.time - LastRangAttackTime > RangAttackCooldown && distance > PointBlank && !MeleeAttacking && !RangAttacking)
            {
                StartCoroutine(Shoot());
            }
            else if (distance < meleeattackdistance && Time.time - lastMeleeAttackTime > MeleeAttackCooldown && !MeleeAttacking && !RangAttacking)
            {
                StartCoroutine(MeleeAttack());
            }

            //bob addition: Show the health bar when aggro
            //enemyCanvas.SetActive(true);
        }
        else
        {
            //bob addition: Hide the health bar when not aggro
            //enemyCanvas.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < aggrodistance && !aggro)
        {
            aggro = true;
        }
    }

    public IEnumerator MeleeAttack()
    {
        MeleeAttacking = true;
        audioSource.PlayOneShot(meleeAttackClip1);
        yield return new WaitForSeconds(MeleeAttackAnimationDuration);
        MeleeAttacking = false;
        lastMeleeAttackTime = Time.time;
    }

    public override void TakeDamage(int damage)
    {
        if (aggro)
        {
            if (UnityEngine.Random.Range(1, 101) > 29)
            {
                Health -= damage;
                //bob addition
                healthbar.SetHealth(Health);

                if (Health <= 0)
                {
                    player.GetComponent<BossesDefeated>().Calista = true;
                    SceneManager.LoadScene("Time Monarch");
                    Destroy(this.gameObject);

                    //bob addition
                    Destroy(GameObject.FindGameObjectWithTag("EnemyCanvas"));
                }
            }
            else
            {
                Health += damage; // Heal enemy for missed attacks
            }

            DoubleDamageTime = Time.time;
        }
    }

    public IEnumerator Shoot()
    {
        audioSource.PlayOneShot(RangeAttackClip);
        RangAttacking = true;
        yield return new WaitForSeconds(RangAttackAnimationDuration);

        EnemyProjectile projectile = Instantiate(Projectile, ProjectilePoint.position, ProjectilePoint.rotation);
        EnemyProjectile projectileController = projectile.GetComponent<EnemyProjectile>();

        if (Time.time < DoubleDamageTime + 10)
        {
            projectileController.Intialize(RangeAttackDamage * 2, RangeAttackSpeed);
        }
        else
        {
            projectileController.Intialize(RangeAttackDamage, RangeAttackSpeed);
        }

        RangAttacking = false;
        LastRangAttackTime = Time.time;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (attackbox.enabled)
            {
                if (Time.time < DoubleDamageTime + 10)
                    player.TakeDamage(MeleeAttackDamage * 2);
                else
                    player.TakeDamage(MeleeAttackDamage);
            }
        }
    }
}
