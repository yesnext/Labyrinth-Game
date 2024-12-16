using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedAttackEnemies : UniversalEnemyNeeds
{
    protected float LastRangAttackTime;
    public float RangAttackCooldown = 1.0f;
    public float RangeAttackRange = 10f;
    public GameObject Projectile;
    private Transform ProjectilePoint;
    private SummonsSpawnLocation spawnlocation;
    private bool shooting;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerStats>();
        ProjectilePoint = GetComponentInChildren<EnemyProjectilePoint>().transform;
        animator = GetComponent<Animator>();
    }
    public void Intialize(SummonsSpawnLocation spawnloc)
    {
        spawnloc.ocupied = true;
        spawnlocation = spawnloc;
    }

    // Update is called once per frame
    void Update()
    {
        ChangedDirectionFollow();
        if (Time.time - LastRangAttackTime > RangAttackCooldown && distance < RangeAttackRange && !shooting)
        {
            StartCoroutine(Shoot());
        }
    }
    public IEnumerator Shoot()
    {
        shooting = true;
        anim.SetBool("isShoot",shooting);
        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        yield return new WaitForSeconds(RangAttackAnimationDuration);
        GameObject projectile = Instantiate(Projectile, ProjectilePoint.position, ProjectilePoint.rotation);
        EnemyProjectile projectileController = projectile.GetComponent<EnemyProjectile>();
        projectileController.Intialize(RangeAttackDamage,RangeAttackSpeed);
        shooting =false;
        anim.SetBool("isShoot",shooting);
        LastRangAttackTime = Time.time;
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
    }
    public override void TakeDamage(int damage)
    {
        Health = Health - damage;
        if (Health <= 0)
        {
            spawnlocation.ocupied = false;
            Destroy(this.gameObject);
        }
    }
}
