using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ChainedGirlBoss : UniversalEnemyNeeds
{
    public Chains[] chains;
    private EnemyProjectilePoint Projectilepoint;
    public EnemyProjectile Projectile;
    public float RangeAttackDistance;
    private float LastRangAttackTime;
    public float RangAttackCooldown;
    // Start is called before the first frame update
    void Start()
    {
        chains = FindObjectsOfType<Chains>();
        Projectilepoint = GetComponentInChildren<EnemyProjectilePoint>();
        player = FindObjectOfType<PlayerStats>();
        IsImmune = false;
        LastRangAttackTime = -RangAttackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        ChangedDirectionFollow();
        if (distance >= RangeAttackDistance && Time.time - LastRangAttackTime > RangAttackCooldown)
        {
            RangeAttack();
        }
        Begone();
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
    }
    public void Begone()
    {
        if (chains[0] == null && chains[1] == null)
        {
            Destroy(this.gameObject);
        }
    }
    public void RangeAttack()
    {
        EnemyProjectile projectile = Instantiate(Projectile, Projectilepoint.transform.position, Projectilepoint.transform.rotation);
        EnemyProjectile projectileController = projectile.GetComponent<EnemyProjectile>();
        projectileController.Intialize(RangeAttackDamage,RangeAttackSpeed);
        LastRangAttackTime = Time.time;
    }
}
