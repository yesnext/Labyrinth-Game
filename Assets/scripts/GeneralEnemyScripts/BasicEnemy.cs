using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicEnemy : UniversalEnemyNeeds
{
<<<<<<< Updated upstream
    protected Animator animator;
=======
    private Rigidbody2D rb;
>>>>>>> Stashed changes
    public float MeleeAttackDistance = 5.0f;
    public bool Element = false;
    public float playerFolowDistance = 10.0f;
    public short enemystate;
    public int decision;
    public float decisioncooldown = 3f;
    public float lastdecisioncooldown = 0;
    private int originalhealth;
    private float WanderingWalkingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        animator = GetComponent<Animator>();
        originalhealth = Health;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
=======
        animator.SetFloat("speed", rb.velocity.magnitude);
>>>>>>> Stashed changes
        ChangedDirectionFollow();
        if (enemystate == 0)
        {
            if (this.isFacingRight == true)
            {
                this.GetComponent<Rigidbody2D>().velocity =
                new Vector2(WanderingWalkingSpeed, this.GetComponent<Rigidbody2D>().velocity.y);

            }
            else
            {
                this.GetComponent<Rigidbody2D>().velocity =
                new Vector2(-WanderingWalkingSpeed, this.GetComponent<Rigidbody2D>().velocity.y);

            }
            if (distance < playerFolowDistance)
            {
                enemystate = 1;
                Followplayer();
            }
            if (Health < originalhealth)
            {
                enemystate = 1;
                Followplayer();
            }
            if (Time.time - lastdecisioncooldown > decisioncooldown)
            {
                lastdecisioncooldown = Time.time;
                decision = Random.Range(1, 3);

                if (decision == 1)
                {
                    RandomChangeInDirectionOrIdle();
                }
            }
        }
        else if (enemystate == 1)
        {
            if (!MeleeAttacking)
            {
                Followplayer();
            }
            if (Time.time - lastMeleeAttackTime > MeleeAttackCooldown && distance < MeleeAttackDistance && !MeleeAttacking)
            {
                StartCoroutine(meleeAttack());
            }
        }
    }
    public IEnumerator meleeAttack()
    {
        audioSource.PlayOneShot(meleeAttackClip1);
        MeleeAttacking = true;
<<<<<<< Updated upstream
        yield return new WaitForSeconds(MeleeAttackAnimationDuration);
        MeleeAttacking = false;
        lastMeleeAttackTime = Time.time;
=======
        animator.SetBool("isAttacking", MeleeAttacking);
        yield return new WaitForSeconds(MeleeAttackAnimationDuration);
        MeleeAttacking = false;
        lastMeleeAttackTime = Time.time;
        animator.SetBool("isAttacking", MeleeAttacking);
>>>>>>> Stashed changes
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
    }
    public void TakeDamage(int damage, bool element)
    {
        if (element != Element)
        {
            Health = Health - damage;

            if (Health < 0)
            {
<<<<<<< Updated upstream
=======
                animator.SetBool("IsDead", true);
>>>>>>> Stashed changes
                Destroy(this.gameObject);
            }
        }
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (attackbox.enabled)
            {
                player.TakeDamage(MeleeAttackDamage);
            }
        }
    }
    public void Ghangedirection()
    {

        scale = this.transform.localScale;
        scale.x *= -1;
        isFacingRight = !isFacingRight;
        this.transform.localScale = scale;

    }
    public void RandomChangeInDirectionOrIdle()
    {
        decision = Random.Range(1, 3);
        if (decision == 1)
        {
            animator.SetBool("Idle", true);
            EnemySpeed = 0f;
        }
        else if (decision == 2)
        {
            EnemySpeed = 1f;
            Ghangedirection();
        }
    }
}