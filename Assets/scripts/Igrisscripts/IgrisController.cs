using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IgrisController : UniversalEnemyNeeds
{
    public BoxCollider2D FistAttackBox;
    public float AgrueDistance = 10.0f;
    public bool state;
    public int BossPhase = 1;
    public bool dodge;
    protected float DodgeDurationCounter = 0.0f;
    protected float dodgeDuration = 0.1f;
    protected float BossDodgeSpeed = 8f;
    // Start is called before the first frame update
    void Start()
    {
        OriginalSpeed = EnemySpeed;
        player = FindObjectOfType<PlayerStats>();
        IsImmune = true;
        FistAttackBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (state)
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
        }
        else
        {
            if (distance < AgrueDistance)
            {
                state = true;
            }
        }

    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
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
        yield return new WaitForSeconds(MeleeAttackAnimationDuration);
        EnemySpeed = OriginalSpeed;
        IsLunging = false;
        MeleeAttacking = false;
        lastMeleeAttackTime = Time.time;
    }
    public IEnumerator FistSwing()
    {
        //this is to trigger the fist fight animation
        MeleeAttacking = true;
        yield return new WaitForSeconds(MeleeAttackAnimationDuration);
        EnemySpeed = OriginalSpeed;
        IsLunging = false;
        MeleeAttacking = false;
        lastMeleeAttackTime = Time.time;
    }
    public override void TakeDamage(int damage)
    {
        if (!IsImmune)
        {
            Health = Health - damage;
            if (Health <= 0 && BossPhase >= 2)
            {
                Destroy(this.gameObject);
            }


        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (attackbox.enabled)
        {
            if (other.tag == "Player")
            {
                player.TakeDamage(MeleeAttackDamage);
            }
        }
        if (FistAttackBox.enabled)
        {
            if (other.tag == "Player")
                player = other.GetComponent<PlayerStats>();
            if (Time.time - lastMeleeAttackTime > MeleeAttackCooldown)
            {
                FistSwing();
            }
        }

    }
}
