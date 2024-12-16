using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IgrisController : UniversalEnemyNeeds
{
    public BoxCollider2D FistAttackBox;
    public int BossPhase = 1;
    public bool dodge;
    protected float DodgeDurationCounter = 0.0f;
    protected float dodgeDuration = 0.1f;
    protected float BossDodgeSpeed = 8f;
    public GameObject wall;
    // Start is called before the first frame update

    private Animator anim;
    private Rigidbody2D rb;

    //bob addition
    public HealthBar healthbar;

    //bob addition
    private GameObject enemyCanvas;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        OriginalSpeed = EnemySpeed;
        player = FindObjectOfType<PlayerStats>();
        IsImmune = true;
        FistAttackBox.enabled = false;
        if (player.GetComponent<BossesDefeated>().Igris)
        {
            Destroy(this.gameObject);
        }

        //bob addition
        healthbar.SetMaxHealth(Health);


        //bob addition
       enemyCanvas = GameObject.FindGameObjectWithTag("EnemyCanvas");
        enemyCanvas.SetActive(false);  // Hide health bar initially
    }

    // Update is called once per frame
    void Update()
    {
        if(player.firstencounter){
            anim.SetFloat("speedSword",rb.velocity.magnitude);
        }else{
            anim.SetFloat("speedHand",rb.velocity.magnitude);
        }
        if (aggro)
        {

            ChangedDirectionFollow();
            Followplayer();
            if (BossPhase == 1 && IsImmune && dodge && player.firstencounter)
            {
                StartCoroutine(Awareofeverymovedodge());
            }
            if (distance >= LungAttackDistance && Time.time - lastMeleeAttackTime > MeleeAttackCooldown && Time.time - LastLungAttackTime > LungAttackCooldown)
            {
                LungAttack();

            }
            else if (BossPhase == 1 && distance < meleeattackdistance && Time.time - lastMeleeAttackTime > meleeattackdistance && player.firstencounter && !MeleeAttacking)
            {
                StartCoroutine(SwordSlash());
            }
            else if (BossPhase == 2 && distance < meleeattackdistance && Time.time - lastMeleeAttackTime > meleeattackdistance && !player.firstencounter && !MeleeAttacking)
            {
                Debug.Log("inside fist");
                StartCoroutine(FistSwing());
            }
             //bob addition
             enemyCanvas.SetActive(true);
        }
        
        
    
        
       
    
        else
        {
            if (distance < aggrodistance)
            {
                aggro = true;
            }
        }
        if (aggro)
        {
            wall.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            wall.GetComponent<BoxCollider2D>().enabled = false;
        }

    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < aggrodistance && !aggro)
        {
            aggro = true;
        }
        if (player.Health <= 0)
        {
            aggro = false;
        }
    }
    public IEnumerator Awareofeverymovedodge()
    {
        dodge = false;
        if (IsImmune)
        {
            Vector3 dodgeDirection = (transform.position - player.transform.position).normalized;
            DodgeDurationCounter = 0f;
            EnemySpeed *= 4f;
            while (DodgeDurationCounter < dodgeDuration)
            {
                DodgeDurationCounter += Time.deltaTime;
                yield return null;
            }
            EnemySpeed = OriginalSpeed;
            transform.position += dodgeDirection * BossDodgeSpeed * Time.deltaTime;
        }
    }
    public void LungAttack()
    {
        if (Time.time - LastLungAttackTime > LungAttackCooldown)
        {
            LastLungAttackTime = Time.time;
            if (!IsLunging)
            {
                EnemySpeed *= 1.5f;
                IsLunging = true;
            }
        }
    }
    public IEnumerator SwordSlash()
    {
        //this is to trigger the sword fight animation
        MeleeAttacking = true;
        anim.SetBool("stkSword",MeleeAttacking);
        yield return new WaitForSeconds(MeleeAttackAnimationDuration);
        EnemySpeed = OriginalSpeed;
        IsLunging = false;
        MeleeAttacking = false;
        anim.SetBool("stkSword",MeleeAttacking);
        lastMeleeAttackTime = Time.time;
    }
    public IEnumerator FistSwing()
    {
        //this is to trigger the fist fight animation
        MeleeAttacking = true;
        anim.SetBool("stkHand",MeleeAttacking);
        yield return new WaitForSeconds(MeleeAttackAnimationDuration);
        EnemySpeed = OriginalSpeed;
        IsLunging = false;
        MeleeAttacking = false;
        anim.SetBool("stkHand",MeleeAttacking);
        lastMeleeAttackTime = Time.time;
    }
    public override void TakeDamage(int damage)
    {
        if (!IsImmune)
        {
            Health = Health - damage;
            if (Health <= 0 && BossPhase >= 2)
            {
                player.GetComponent<BossesDefeated>().Igris = true;
                wall.GetComponent<BoxCollider2D>().enabled = false;
                SceneManager.LoadScene("Seraphine");
                Destroy(this.gameObject);
            }


        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (attackbox.enabled || FistAttackBox.enabled)
        {
            if (other.tag == "Player")
            {
                player.TakeDamage(MeleeAttackDamage);
            }
        }
    }
}
