using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WardenObelisks : MonoBehaviour
{
    public GameObject Projectile;
    public int Damage = 3;
    public int Damageincreaseby = 3;
    public int Health;
    public float distance;
    public PlayerStats player;
    public float AttackCooldown = 1.0f;
    public float LastAttackCooldown = 0;
    public Vector2 direction;
    public Transform ProjectilePoint;
    public bool isFacingRight;
    public Vector3 scale;
    public int CurrentNumOfObelisks;
    public bool state;
    void Start()
    {
        CurrentNumOfObelisks = FindObjectsOfType<WardenObelisks>().Length;
        player = FindObjectOfType<PlayerStats>();
        ProjectilePoint = GameObject.FindObjectOfType<EnemyProjectilePoint>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (state){
        NumOfObelisks();
        GhangedirectionFollow();
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
        LastAttackCooldown = Time.time;
        GameObject projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
        EnemyProjectile projectileController = projectile.GetComponent<EnemyProjectile>();
        projectileController.Intialize(Damage);
    }
    public void TakeDamage(int damage)
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
            Damage += Damageincreaseby;
        }
    }
    public void GhangedirectionFollow()
    {
        if (direction.x > 0 && !isFacingRight)
        {

            scale = transform.localScale;
            scale.x *= -1;
            isFacingRight = true;
            transform.localScale = scale;

        }
        else if (direction.x < 0 && isFacingRight)
        {
            scale = transform.localScale;
            scale.x *= -1;
            isFacingRight = false;
            transform.localScale = scale;
        }
    }

}
