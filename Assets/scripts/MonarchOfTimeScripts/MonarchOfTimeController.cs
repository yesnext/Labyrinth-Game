using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonarchOfTimeController : UniversalEnemyNeeds
{
    private EnemyProjectilePoint projectilePoint;
    public float rangeAttackCooldown = 2.0f;
    private float LastRangeAttackTime;
    public MonarchOfTimeProjectile Projectile;
    public float TimestopInturuptionDuration = 5f;
    private float LastTimeStop;
    public float TimestopChooldown = 20f;
    public HourGlass TimeStopHourGlass;
    public HourGlass ReversingTimeHourGlass;
    private bool TimeStopped;
    private bool Timerevered;
    public float ReversingTimeinturuptionDuration = 5f;
    public float ReversingTimeCooldown = 20.0f;
    private float LastTimeTimeReversal;
    private float freezintimeduration = 2.0f;
    public float SlowRate;
    private int originalhealth; 
    public AudioClip TimeStopClip;
    public AudioClip timereversalClip;
    
    //bob addition
    public HealthBar healthbar;

    //bob addition
    private GameObject enemyCanvas;
    
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        projectilePoint = FindObjectOfType<EnemyProjectilePoint>();
        LastTimeStop -= TimestopChooldown;
        LastTimeTimeReversal -= ReversingTimeCooldown;
        originalhealth = Health;
        if(player.GetComponent<BossesDefeated>().Monarchoftime){
            Destroy(this.gameObject);
        }
        audioSource = GetComponent<AudioSource>();


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
            if (Time.time - LastRangeAttackTime > rangeAttackCooldown)
            {
                StartCoroutine(Shoot());
            }
            if (Time.time - LastTimeStop > TimestopChooldown && !Timerevered)
            {
                StartCoroutine(StopingTime());
            }
            if (Time.time - LastTimeTimeReversal > ReversingTimeCooldown && !TimeStopped)
            {
                StartCoroutine(ReversingTime());
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
    public IEnumerator Shoot()
    {
        audioSource.PlayOneShot(RangeAttackClip);
        RangAttacking = true;
        yield return new WaitForSeconds(RangAttackAnimationDuration);
        MonarchOfTimeProjectile projectile = Instantiate(Projectile, projectilePoint.transform.position, projectilePoint.transform.rotation);
        MonarchOfTimeProjectile projectileController = projectile.GetComponent<MonarchOfTimeProjectile>();
        projectileController.Intialize(RangeAttackDamage, RangeAttackSpeed, SlowRate);
        LastRangeAttackTime = Time.time;
        RangAttacking = false;
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < aggrodistance && !aggro)
        {
            aggro = true;
        }
    }
    public IEnumerator StopingTime()
    {
        TimeStopped = true;
        TimeStopHourGlass.Health = 100; //resetting hour glass health to 100;
        yield return new WaitForSeconds(TimestopInturuptionDuration);
        if (TimeStopHourGlass.Health > 0)
        {
            audioSource.PlayOneShot(TimeStopClip);
            player.GetComponent<controls>().frozen = true;
            Rigidbody2D playerrb = player.GetComponent<Rigidbody2D>();
            RigidbodyConstraints2D originalRB = playerrb.constraints;
            playerrb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            yield return new WaitForSeconds(freezintimeduration);
            playerrb.constraints = originalRB | RigidbodyConstraints2D.FreezeRotation;
            player.GetComponent<controls>().frozen = false;
        }
        TimeStopped = false;
        LastTimeStop = Time.time;

    }
    public override void TakeDamage(int damage)
    {
        if (aggro)
        {
            Health = Health - damage;
            if (Health <= 0)
            {
                player.GetComponent<BossesDefeated>().Monarchoftime = true;
                Destroy(this.gameObject);
            }
        }
    }
    public IEnumerator ReversingTime()
    {
        Timerevered = true;
        ReversingTimeHourGlass.Health = 100; //resetting hour glass health to 100;
        yield return new WaitForSeconds(ReversingTimeinturuptionDuration);
        Timerevered = false;
        if (ReversingTimeHourGlass.Health > 0)
        {
            audioSource.PlayOneShot(timereversalClip);
            Health = originalhealth;
        }
        LastTimeTimeReversal = Time.time;
    }

}
