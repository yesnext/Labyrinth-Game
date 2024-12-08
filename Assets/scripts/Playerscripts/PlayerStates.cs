using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{

    public Transform ProjectilePoint;
    public GameObject Projectile;
    public static int ProjectileCount=0;
    public KeyCode RangeAttackKey;
    public int Health =100;
    public float HealCooldown=10.0f;
    public float LastHealCooldown=10.0f;
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
    public BoxCollider2D attackbox;
 void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
       if (Input.GetKeyDown(MeleeAttackkey)){
            // animator.SetBool("iceattack",true);
        }
        if (Input.GetKeyDown(SwitchElement)){
            ChangeElement();
        }
        if(Input.GetKeyDown(RangeAttackKey)){
            ProjectileCount++;
            Shoot();
        }
    }

    private void ChangeElement()
    {
        element= !element;
    }
    public int projectileController(){
        return ProjectileCount;
    }
   
    public void Shoot(){
        GameObject projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
        PlayerProjectile projectileController = projectile.GetComponent<PlayerProjectile>();
        projectileController.Intialize(ProjectilePoint);
    }
    public void TakeDamage(int damage){
        Health=Health-damage;
        Debug.Log("Health"+ Health);
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
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag=="Enemy"){
            enemy = other.GetComponent<BasicEnemy>();
            BasicMeleeAttack();
        }
        else if (other.tag=="Boss"){
            if(attackbox.enabled){
            Boss = other.GetComponent<FinalBossController>();
            BossMeleeAttack();
            }
        }
    }
    public bool CheckIfHealIsAvailable(){
        if (Time.time - LastHealCooldown>HealCooldown){
            return true;
            LastHealCooldown = Time.time;
        }else{
            return false;
        }

    }
    public void Heal (){
        Health+=40;
        if (Health>100){
            Health = 100;
        }
        LastHealCooldown = Time.time;
        
    }

}