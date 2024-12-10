using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalEnemyNeeds : MonoBehaviour
{
    public bool isFacingRight;
    protected Vector2 direction;
    protected PlayerStats player;
    protected float distance;
    protected Vector3 scale;
    public float EnemySpeed;
    public float OriginalSpeed;
    protected int Health;
    protected bool IsImmune;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public new void Followplayer()
    {
        // animator.SetBool("Walking", true);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, EnemySpeed * Time.deltaTime);

        GhangedirectionFollow();
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
    public void TakeDamage(int damage)
    {
        Health = Health - damage;
        if (Health <= 0 )
        {
            Destroy(this.gameObject);
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
