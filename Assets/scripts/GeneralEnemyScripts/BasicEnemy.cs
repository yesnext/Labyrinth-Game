                                                   using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicEnemy : MonoBehaviour
{
    protected PlayerStates player;
    public bool isFacingRight = true;
    public float EnemySpeed = 0.1f;
    public int Health = 20;
    protected Animator animator;
    public float MeleeAttackDistance = 5.0f;
    public int MeleeAttackDamage = 6;
    public float MeleeAttackCooldown = 5.0f; //time in seconds for the cooldown
    public float lastMeleeAttackTime = -5.0f;
    public float distance;
    public bool Element = false;
    public float playerFolowDistance = 10.0f;
    public short enemystate;
    public Vector3 scale;
    public Vector2 direction;
    public int decision;
    public float decisioncooldown = 3f;
    public float lastdecisioncooldown = 0;
    public BoxCollider2D attackbox;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStates>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        direction = (player.transform.position - transform.position).normalized;
        GhangedirectionFollow();
        if (enemystate == 0)
        {
            if (this.isFacingRight == true)
            {
                this.GetComponent<Rigidbody2D>().velocity =
                new Vector2(EnemySpeed, this.GetComponent<Rigidbody2D>().velocity.y);

            }
            else
            {
                this.GetComponent<Rigidbody2D>().velocity =
                new Vector2(-EnemySpeed, this.GetComponent<Rigidbody2D>().velocity.y);

            }
            if (distance < playerFolowDistance)
            {
                Followplayer();
            }
            if (Health < 20)
            {
                Followplayer();
            }
            if (Time.time - lastdecisioncooldown>decisioncooldown){
             lastdecisioncooldown = Time.time;
             decision =Random.Range(1,3);
            
             if(decision == 1){
                RandomChangeInDirectionOrIdle();
             }
            }
        }
        else if (enemystate == 1)
        {
            EnemySpeed = 1f;
                Followplayer();
        }
    }
    public void meleeAttack()
    {  
            if(Time.time - lastMeleeAttackTime > MeleeAttackCooldown && distance< 5.0f ){
            player.TakeDamage(MeleeAttackDamage);
            lastMeleeAttackTime = Time.time;
        }
        
    }
   
    public void Followplayer()
    {
        // animator.SetBool("Walking", true);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, EnemySpeed * Time.deltaTime);
        enemystate = 1;
        GhangedirectionFollow();
        if (distance < 4.0f){
            // animator.SetBool("Walking", false);
            // animator.SetBool("attack",true);
        }
        else{
            // animator.SetBool("attack",false);
        }
    }
    public void TakeDamage(int damage,bool element)
    {
        if (element!=Element){
            Health = Health - damage;
            
            if (Health < 0)
            {
                Destroy(this.gameObject);
            }
        }
        else{
           
        }
    }
    public void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            if(attackbox.enabled){
            meleeAttack();
            }
        }
    }
    public void Ghangedirection(){
        
        scale =this.transform.localScale;
            scale.x *= -1;
            isFacingRight = !isFacingRight;
            this.transform.localScale=scale;
       
    }
    public void GhangedirectionFollow(){
        if (direction.x > 0 && !isFacingRight)
        {
            scale =transform.localScale;
            scale.x *= -1;
            isFacingRight = true;
            transform.localScale=scale;
            
        }
        else if (direction.x < 0 && isFacingRight)
        {
            scale =transform.localScale;
            scale.x *= -1;
            isFacingRight = false;
            transform.localScale=scale;
        }
    }
    public void RandomChangeInDirectionOrIdle(){
        decision =Random.Range(1,3);
        if ( decision == 1){
            animator.SetBool("Idle", true);
            EnemySpeed = 0f;
        }
        else if(decision == 2){
            EnemySpeed = 1f;
            Ghangedirection();
        }
    }
}