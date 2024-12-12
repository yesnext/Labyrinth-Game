using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ChainedGirlBoss : FinalBossController
{
    public GameObject Chain1;
    public GameObject Chain2;
    public bool ready;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        IsImmune = false;
    }

    // Update is called once per frame
    void Update()
    {
        GhangedirectionFollow();
        if (distance >= RangeAttackDistance && ready)
        {
            RangeAttack();
        }
        Begone();
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
        ready = Time.time - LastRangAttackTime > RangAttackCooldown;
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
        Instantiate(Projectile, ProjectilePoint.position, ProjectilePoint.rotation);
        LastRangAttackTime = Time.time;
    }
}
