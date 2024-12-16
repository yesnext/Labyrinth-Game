using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalEnemyNeeds : MonoBehaviour
{
    public bool isFacingRight = true;
    protected Vector2 direction;
    protected PlayerStats player;
    protected float distance;
    protected Vector3 scale;
    public float EnemySpeed;
    protected float OriginalSpeed;
    public int Health;
    public bool IsImmune;
    public int MeleeAttackDamage;
    public float meleeattackdistance;
    protected float lastMeleeAttackTime;
    protected float MeleeAttackCooldown = 3.0f;
    protected float LungAttackDistance = 5.0f;
    protected float LungAttackCooldown = 5f; //time in seconds for the cooldown
    protected float LastLungAttackTime = -5.0f;
    protected bool IsLunging = false;
    public float RangeAttackSpeed;
    public int RangeAttackDamage;
    public BoxCollider2D attackbox;
    protected bool MeleeAttacking;
    public float MeleeAttackAnimationDuration;
    protected bool RangAttacking;
    public float RangAttackAnimationDuration;
    public float aggrodistance;
    public bool aggro;
    public AudioSource audioSource;
    public AudioClip meleeAttackClip1;
    public AudioClip meleeAttackClip2;
    public AudioClip RangeAttackClip;
    public AudioClip healClip;
    public AudioClip walking;
    public Animator animator;
    
     
    void Start()
    {
        


    }


    void Update()
    {

    }

    public void Followplayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, EnemySpeed * Time.deltaTime);
        audioSource.PlayOneShot(walking);
        ChangedDirectionFollow();
    }
    public virtual void TakeDamage(int damage)
    {
        Health = Health - damage;
        
        if (Health <= 0)
        {
            //bob addition
            Destroy(GameObject.FindGameObjectWithTag("EnemyCanvas"));

            Destroy(this.gameObject);

        }
    }
    public void ChangedDirectionFollow()
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
