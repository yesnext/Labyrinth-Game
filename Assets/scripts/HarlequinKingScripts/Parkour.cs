using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parkour : UniversalEnemyNeeds
{
    public bool turnon;
    public EnemyProjectile projectile;
    public EnemyProjectilePoint projectilePoint;
    public checkpoint3 nextpuzzel;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        nextpuzzel = FindObjectOfType<checkpoint3>();
        projectilePoint = FindObjectOfType<EnemyProjectilePoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (turnon)
        {
            if (Time.time - lastMeleeAttackTime > MeleeAttackCooldown)
                Shoot();
        }

    }
    public void Shoot()
    {
        lastMeleeAttackTime = Time.time;
        Instantiate(projectile, projectilePoint.transform.position, projectilePoint.transform.rotation);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        turnon = false;
        player.transform.position = new Vector3(nextpuzzel.transform.position.x, nextpuzzel.transform.position.y, player.transform.position.z);
        FindObjectOfType<LavaRun>().turnon = true;
    }
}
