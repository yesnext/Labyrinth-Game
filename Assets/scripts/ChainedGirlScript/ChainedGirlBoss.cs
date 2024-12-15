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
        if(player.GetComponent<BossesDefeated>().chainedgirl){
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (aggro)
        {
            ChangedDirectionFollow();
            if (distance >= RangeAttackDistance && Time.time - LastRangAttackTime > RangAttackCooldown)
            {
                RangeAttack();
            }
            Begone();
        }
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
    public void Begone()
    {
        if (chains[0] == null && chains[1] == null)
        {
            player.GetComponent<BossesDefeated>().chainedgirl = true;
            Destroy(this.gameObject);
        }
    }
    public void RangeAttack()
    {
        EnemyProjectile projectile = Instantiate(Projectile, Projectilepoint.transform.position, Projectilepoint.transform.rotation);
        EnemyProjectile projectileController = projectile.GetComponent<EnemyProjectile>();
        projectileController.Intialize(RangeAttackDamage, RangeAttackSpeed);
        LastRangAttackTime = Time.time;
    }
}
