using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{

    public Transform ProjectilePoint;
    public GameObject Projectile;
    public static int ProjectileCount=0;
     public int ProjectileCountVis;
    public KeyCode RangeAttackKey;
    public int Health =100;
    protected Animator animator;
    public KeyCode MeleeAttackkey;
    public float MeleeAttackDistance=5.0f;
    public int MeleeAttackDamage=6;
    public float MeleeAttackCooldown = 3.0f; //time in seconds for the cooldown
    private float lastMeleeAttackTime = 0f;
    public bool attackanim=false;
    public bool element = false; // if false it will be fire if tru will be ice
    public KeyCode SwitchElement;
    public BasicEnemy enemy;
    public FinalBossController Boss;

    public Vector3 direction;
     public  Vector3 mousePosition;
 void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ProjectileCountVis=ProjectileCount;
       if (Input.GetKeyDown(MeleeAttackkey)){
            // animator.SetBool("iceattack",true);
        }
        if (Input.GetKeyDown(SwitchElement)){
            ChangeElement();
        }
        if(Input.GetKeyDown(RangeAttackKey)){
            Shoot();
        }
    }

    private void ChangeElement()
    {
        element= !element;
    }

   
    public void Shoot(){
        ProjectileCount++;
        GameObject projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
        PlayerProjectile projectileController = projectile.GetComponent<PlayerProjectile>();
        projectileController.Intialize(ProjectilePoint);
    }
    public void TakeDamage(int damage){
        Health=Health-damage;
        Debug.Log("Health = "+Health);
    }
    public void BasicMeleeAttack(){
        if(Time.time - lastMeleeAttackTime>MeleeAttackCooldown){
            lastMeleeAttackTime = Time.time;
            enemy.GetComponent<BasicEnemy>().TakeDamage(MeleeAttackDamage,element);
        // animator.SetTrigger("Attack");
        }
    }
    public void BossMeleeAttack(){
        if(Time.time - lastMeleeAttackTime>MeleeAttackCooldown){
            lastMeleeAttackTime = Time.time;
            Boss.GetComponent<FinalBossController>().TakeDamage(MeleeAttackDamage);
            // animator.SetTrigger("Attack");
        }
    }
    public void OnTriggerEnter2D(Collider2D other){
        if (other.tag=="Enemy"){
            enemy = other.GetComponent<BasicEnemy>();
            BasicMeleeAttack();
        }
        else if (other.tag=="Boss"){
            Boss = other.GetComponent<FinalBossController>();
            BossMeleeAttack();
        }
    }
    public void Heal (){
        Health+=40;
        if (Health>100){
            Health = 100;
        }
    }

}