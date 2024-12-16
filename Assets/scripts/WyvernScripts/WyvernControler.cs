using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class WyvernControler : UniversalEnemyNeeds
{
    public PatrolPoints[] PatrolPoints;
    public float speed = 3.0f;
    public float attackRange = 10.0f;
    public float attackCooldown = 2.0f;
    public float attackDuration = 1.0f;
    private EnemyProjectilePoint projectilePoint;
    public EnemyProjectile Projectile;
    private int CurrentPatrolPoint = 0;
    private float lastAttackTime = 0.0f;
    private float Patroldistance;
    private Vector2 PatrolDirection;
    private int numberofpatrolpoints;
    private int nextpatrolpoint;
    private Animator anim;

    //bob addition
    public HealthBar healthbar;

    //bob addition
    private GameObject enemyCanvas;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerStats>();
        PatrolPoints = FindObjectsOfType<PatrolPoints>();
        numberofpatrolpoints = PatrolPoints.Length;
        projectilePoint = FindObjectOfType<EnemyProjectilePoint>();
        if(player.GetComponent<BossesDefeated>().wyvern){
            Destroy(this.gameObject);
        }

        //bob addition
        healthbar.SetMaxHealth(Health);


        //bob addition
       enemyCanvas = GameObject.FindGameObjectWithTag("EnemyCanvas");
        enemyCanvas.SetActive(false);  // Hide health bar initially
    }

    // Update is called once per frame

    void Update()
    {
        if (aggro)
        {
            ChangedDirectionFollow();
            if (Time.time - lastAttackTime > attackCooldown && !RangAttacking)
            {
                Followplayer();
                if (distance < attackRange)
                    StartCoroutine(Attack());

            }
            else if (!RangAttacking)
            {
                Patrol();
                GhangedirectionPatrol();

            }
             //bob addition
             enemyCanvas.SetActive(true);
        }
        else
    {
        // Hide the health bar when not aggro
        enemyCanvas.SetActive(false);
    }
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
        Patroldistance = Vector2.Distance(transform.position, PatrolPoints[CurrentPatrolPoint].transform.position);
        PatrolDirection = (PatrolPoints[CurrentPatrolPoint].transform.position - transform.position).normalized;
        if (distance < aggrodistance && !aggro)
        {
            aggro = true;
        }
    }
    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPatrolPoint].transform.position, EnemySpeed * Time.deltaTime);

        // Check if we reached the waypoint
        if (Patroldistance < 0.1f)
        {
            while (nextpatrolpoint == CurrentPatrolPoint)
                nextpatrolpoint = Random.Range(0, numberofpatrolpoints);
            CurrentPatrolPoint = nextpatrolpoint;
        }
    }
    public void GhangedirectionPatrol()
    {
        if (PatrolDirection.x > 0 && !isFacingRight)
        {

            scale = transform.localScale;
            scale.x *= -1;
            isFacingRight = true;
            transform.localScale = scale;

        }
        else if (PatrolDirection.x < 0 && isFacingRight)
        {
            scale = transform.localScale;
            scale.x *= -1;
            isFacingRight = false;
            transform.localScale = scale;
        }
    }
    public override void TakeDamage(int damage)
    {
        if (aggro)
        {
            Health = Health - damage;
             //bob addition
            healthbar.SetHealth(Health);
            if (Health <= 0)
            {
                player.GetComponent<BossesDefeated>().wyvern = true;
                Destroy(this.gameObject);
                //bob addition
            Destroy(GameObject.FindGameObjectWithTag("EnemyCanvas"));
            }
        }
    }

    public IEnumerator Attack()
    {
        RangAttacking = true;
        yield return new WaitForSeconds(RangAttackAnimationDuration);
        EnemyProjectile projectile = Instantiate(Projectile, projectilePoint.transform.position, projectilePoint.transform.rotation);
        EnemyProjectile projectileController = projectile.GetComponent<EnemyProjectile>();
        projectileController.Intialize(RangeAttackDamage, RangeAttackSpeed);
        lastAttackTime = Time.time;
        RangAttacking = false;
    }
}


