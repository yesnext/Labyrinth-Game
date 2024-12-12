using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ChainedGirlBoss : UniversalEnemyNeeds
{
    public GameObject Chain1;
    public GameObject Chain2;
    private EnemyProjectilePoint Projectilepoint;
    public EnemyProjectile projectile;
    public float RangeAttackDistance;
    private float LastRangAttackTime;
    public float RangAttackCooldown;
    // Start is called before the first frame update
    void Start()
    {
        Projectilepoint = GetComponentInChildren<EnemyProjectilePoint>();
        player = FindObjectOfType<PlayerStats>();
        IsImmune = false;
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
        if (Chain1 == null && Chain2 == null)
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
