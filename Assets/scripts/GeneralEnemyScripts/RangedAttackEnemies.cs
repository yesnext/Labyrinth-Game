using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackEnemies : UniversalEnemyNeeds
{
    protected float LastRangAttackTime;
    protected float RangAttackCooldown = 2.0f;
    protected int RangeAttackDamage = 3;
    public GameObject Projectile;
    private Transform ProjectilePoint;
    private SummonsSpawnLocation spawnlocation;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        ProjectilePoint = GetComponentInChildren<EnemyProjectilePoint>().transform;
    }
    public void Intialize(SummonsSpawnLocation spawnloc)
    {
        spawnloc.ocupied = true;
        spawnlocation = spawnloc;
    }

    // Update is called once per frame
    void Update()
    {
        GhangedirectionFollow();
        if (Time.time - LastRangAttackTime > RangAttackCooldown)
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        LastRangAttackTime = Time.time;
        GameObject projectile = Instantiate(Projectile, ProjectilePoint.position, ProjectilePoint.rotation);
        EnemyProjectile projectileController = projectile.GetComponent<EnemyProjectile>();
        projectileController.Intialize(RangeAttackDamage);
    }
    public void TakeDamage(int damage)
    {
        Health = Health - damage;
        if (Health <= 0)
        {
            spawnlocation.ocupied = false;
            Destroy(this.gameObject);
        }

    }
}
