using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicEnemy : MonoBehaviour
{
    protected controls player;
    public bool isFacingRight = true;
    public float EnemySpeed = 0.1f;
    public int Health = 20;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    public float MeleeAttackDistance = 5.0f;
    public int MeleeAttackDamage = 6;
    public float MeleeAttackCooldown = 5.0f; //time in seconds for the cooldown
    public float lastMeleeAttackTime = -5.0f;
    public bool Meleeanim;
    public float distance;
    public bool Element = false;
    public float playerFolowDistance = 10.0f;
    public short enemystate;
    public bool AttackReady;
    public BoxCollider2D triggerbox;
    public Vector3 scale;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<controls>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        direction = (player.transform.position - transform.position).normalized;
        Ghangedirection();
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
        }
        else if (enemystate == 1)
        {
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
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, EnemySpeed * Time.deltaTime);
        enemystate = 1;
        if (distance < 4.0f){
            animator.SetBool("attack",true);
        }
        else{
            animator.SetBool("attack",false);
        }
    }
    public void TakeDamage(int damage,bool element)
    {
        if (element!=Element){
            Health = Health - damage;
            Debug.Log("enemy Helath" + Health);
            if (Health < 0)
            {
                Destroy(this.gameObject);
            }
        }
        else{
            Debug.Log("wrong element");
        }
    }
    public void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            Invoke(nameof(meleeAttack),Random.Range(0.01f,0.09f));
        }else if(other.tag == "Environment"){
            Ghangedirection();
        }
    }
    public void Ghangedirection(){
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
}
