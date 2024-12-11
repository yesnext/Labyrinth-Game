using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonarchOfTimeController : UniversalEnemyNeeds
{
    private EnemyProjectilePoint projectilePoint;
    public float rangeAttackCooldown = 2.0f;
    private float LastRangeAttackTime;
    public int RangeAttackDamage;
    public EnemyProjectile Projectile;
    private float TimeStopTimer;
    public float TimestopInturuptionDuration =5;
    private float LastTimeStop;
    public float TimestopChooldown;    
    public HourGlass TimeStopHourGlass;
    public HourGlass ReversingTimeHourGlass;
    private bool TimeStopped;
    private float freezintimeduration = 5f;
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        projectilePoint = FindObjectOfType<EnemyProjectilePoint>();
        LastTimeStop -= TimestopChooldown;
    }

    // Update is called once per frame
    void Update()
    {
        GhangedirectionFollow();
        if (Time.time - LastRangeAttackTime > rangeAttackCooldown && false)
        {
            Shoot();
        }
        if (Time.time - LastTimeStop > TimestopChooldown && !TimeStopped)
        {
            StartCoroutine(StopingTime());
        }
    }
    public void Shoot()
    {
        LastRangeAttackTime = Time.time;
        EnemyProjectile projectile = Instantiate(Projectile, projectilePoint.transform.position, projectilePoint.transform.rotation);
        EnemyProjectile projectileController = projectile.GetComponent<EnemyProjectile>();
        projectileController.Intialize(RangeAttackDamage);
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
    }
    public IEnumerator StopingTime()
    {
        TimeStopped = true;
        TimeStopHourGlass.Health = 100; //resetting hour glass health to 100;
        TimeStopTimer = Time.time;
        while (Time.time - TimeStopTimer < TimestopInturuptionDuration)
        {
            if (TimeStopHourGlass.Health <= 0)
            {
                TimeStopped = false;
                LastTimeStop = Time.time;
                yield break;
            }
        }
        GetComponent<controls>().frozen = true;
        Rigidbody2D playerrb = player.GetComponent<Rigidbody2D>();
        RigidbodyConstraints2D originalRB = playerrb.constraints;
        playerrb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        float frozenintime = Time.time;
        while (Time.time - frozenintime < freezintimeduration)
        {
        }
        playerrb.constraints = originalRB;
        playerrb.constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<controls>().frozen = false;
        yield return null;


    }
    public void ReversingTime()
    {

    }

}
