using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyvernControler : UniversalEnemyNeeds
{
    public PatrolPoints[] PatrolPoints; 
    public float speed = 3.0f; 
    public float attackRange = 10.0f; 
    public float attackCooldown = 2.0f;
    public float attackDuration = 1.0f; 

    private int CurrentPatrolPoint = 0;
    private float lastAttackTime = 0.0f;
    private bool isAttacking = false;
    private float Patroldistance;
    private Vector2 PatrolDirection;
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        PatrolPoints = FindObjectsOfType<PatrolPoints>();
    }

    // Update is called once per frame
    
    void Update()
    {
        Followplayer();
        GhangedirectionFollow();

    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
        Patroldistance = Vector2.Distance(transform.position,PatrolPoints[CurrentPatrolPoint].transform.position);
        PatrolDirection = (PatrolPoints[CurrentPatrolPoint].transform.position - transform.position).normalized;
    }
    void Patrol()
    {
        // Move towards the current waypoint
        Transform targetWaypoint = PatrolPoints[CurrentPatrolPoint].transform;
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Check if we reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Move to the next waypoint
            CurrentPatrolPoint = (CurrentPatrolPoint + 1) % PatrolPoints.Length;
        }
    }

    void AttackPlayer()
    {
        // Move towards the player
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Check if close enough to attack
        if (Vector3.Distance(transform.position, player.transform.position) < 1.0f)
        {
            // Perform attack (you can replace this with your attack logic)
            Debug.Log("Wyvern attacks the player!");

            // Set the last attack time
            lastAttackTime = Time.time;

            // Start the attack animation (if any)
            StartCoroutine(PerformAttack());
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


