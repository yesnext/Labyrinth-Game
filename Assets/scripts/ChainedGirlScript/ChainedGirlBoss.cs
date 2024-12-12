using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ChainedGirlBoss : UniversalEnemyNeeds
{
    public Chains[] chains;
    private EnemyProjectilePoint Projectilepoint;
    public EnemyProjectile projectile;
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
        GhangedirectionFollow();
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
        Instantiate(projectile, Projectilepoint.transform.position, Projectilepoint.transform.rotation);
        LastRangAttackTime = Time.time;
    }
}
