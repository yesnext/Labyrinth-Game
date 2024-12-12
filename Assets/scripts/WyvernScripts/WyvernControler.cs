using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class WyvernControler : UniversalEnemyNeeds
{
    public PatrolPoints[] PatrolPoints;
    public float speed = 3.0f;
    public float attackRange = 10.0f;
    public float attackCooldown = 2.0f;
    public float attackDuration = 1.0f;
    private EnemyProjectilePoint projectilePoint;
    public EnemyProjectile projectile;
    private int CurrentPatrolPoint = 0;
    private float lastAttackTime = 0.0f;
    private bool isAttacking = false;
    private float Patroldistance;
    private Vector2 PatrolDirection;
    private int numberofpatrolpoints;
    private int nextpatrolpoint;
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        PatrolPoints = FindObjectsOfType<PatrolPoints>();
        numberofpatrolpoints = PatrolPoints.Length;
        projectilePoint = FindObjectOfType<EnemyProjectilePoint>();
        Debug.Log(FindObjectsOfType<EnemyProjectilePoint>().Length);
    }

    // Update is called once per frame

    void Update()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            Followplayer();
            ChangedDirectionFollow();
            Attack();
        }
        else
        {
            Patrol();
            GhangedirectionPatrol();

        }
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
        Patroldistance = Vector2.Distance(transform.position, PatrolPoints[CurrentPatrolPoint].transform.position);
        PatrolDirection = (PatrolPoints[CurrentPatrolPoint].transform.position - transform.position).normalized;
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

    void Attack()
    {
        if (distance < 6.0f)
        {
            Instantiate(projectile, projectilePoint.transform.position, projectilePoint.transform.rotation);
            lastAttackTime = Time.time;
        }
    }

    System.Collections.IEnumerator PerformAttack()
    {
        isAttacking = true;

        // Here you can add your attack animation logic
        yield return new WaitForSeconds(attackDuration);

        // After the attack, return to patrolling
        isAttacking = false;
    }
}


