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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Followplayer()
    {
        // animator.SetBool("Walking", true);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, EnemySpeed * Time.deltaTime);

        ChangedDirectionFollow();
        if (distance < 4.0f)
        {
            // animator.SetBool("Walking", false);
            // animator.SetBool("attack",true);
        }
        else
        {
            // animator.SetBool("attack",false);
        }
    }
    public virtual void TakeDamage(int damage)
    {
        Health = Health - damage;
        if (Health <= 0)
        {
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
