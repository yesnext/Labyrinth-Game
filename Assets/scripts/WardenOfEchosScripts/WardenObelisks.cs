using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WardenObelisks : UniversalEnemyNeeds
{
    public GameObject Projectile;
    public int Damageincreaseby = 3;
    public float AttackCooldown = 1.0f;
    public float LastAttackCooldown = 0;
    public Transform ProjectilePoint;
    public int CurrentNumOfObelisks;
    public static bool state;
    void Start()
    {
        CurrentNumOfObelisks = FindObjectsOfType<WardenObelisks>().Length;
        player = FindObjectOfType<PlayerStats>();
        if(player.GetComponent<BossesDefeated>().warden){
            Destroy(this.gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state){
        NumOfObelisks();
        ChangedDirectionFollow();
        if (Time.time - LastAttackCooldown > AttackCooldown)
        {
            Shoot();
        }
        }
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
    }
    public void Shoot()
    {
        audioSource.PlayOneShot(RangeAttackClip);
        LastAttackCooldown = Time.time;
        GameObject projectile = Instantiate(Projectile, ProjectilePoint.position, ProjectilePoint.rotation);
        EnemyProjectile projectileController = projectile.GetComponent<EnemyProjectile>();
        projectileController.Intialize(RangeAttackDamage,RangeAttackSpeed);
    }
    public override void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void NumOfObelisks()
    {
        if (FindObjectsOfType<WardenObelisks>().Length < CurrentNumOfObelisks)
        {
            CurrentNumOfObelisks = FindObjectsOfType<WardenObelisks>().Length;
            RangeAttackDamage += Damageincreaseby;
        }
    }

}
