using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IgrisController : FinalBossController
{
    public BoxCollider2D FistAttackBox;
    public float AgrueDistance = 10.0f;
    public bool state;
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
            GhangedirectionFollow();
            Followplayer();
            if (BossPhase == 1 && IsImmune && dodge && player.firstencounter)
            {
                StartCoroutine(Awareofeverymovedodge());
            }
            if (distance >= LungAttackDistance && Time.time - lastShadoSwordSlashesTime > ShadoSwordSlashesCooldown && Time.time - LastLungAttackTime > LungAttackCooldown)
            {
                LungAttack();

            }
            else if (BossPhase == 1 && distance < ShadoAttackDistance && Time.time - lastShadoSwordSlashesTime > ShadoSwordSlashesCooldown && player.firstencounter)
            {
                SwordSlash();
            }
            else if (BossPhase == 2 && distance < ShadoAttackDistance && Time.time - lastShadoSwordSlashesTime > ShadoSwordSlashesCooldown && !player.firstencounter)
            {
                Debug.Log("inside fist");
                FistSwing();
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

    public void LungAttack()
    {
        if (Time.time - LastLungAttackTime > LungAttackCooldown)
        {
            LastLungAttackTime = Time.time;
            EnemySpeed *= 1.5f;
            if (!IsLunging)
            {
                IsLunging = true;
            }
        }
    }
    public void SwordSlash()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (Time.time - lastShadoSwordSlashesTime > ShadoSwordSlashesCooldown)
        {
            EnemySpeed = OriginalSpeed;
            IsLunging = false;
            player.TakeDamage(ShadoAttackDamage);
        }
        lastShadoSwordSlashesTime = Time.time;
    }
    public void FistSwing()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (Time.time - lastShadoSwordSlashesTime > ShadoSwordSlashesCooldown)
        {
            EnemySpeed = OriginalSpeed;
            IsLunging = false;
            player.TakeDamage(ShadoAttackDamage);
        }
        lastShadoSwordSlashesTime = Time.time;
    }
    public new void TakeDamage(int damage)
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

                player = other.GetComponent<PlayerStats>();
            if (Time.time - lastShadoSwordSlashesTime > ShadoSwordSlashesCooldown)
            {
                SwordSlash();
            }

        }
        if (FistAttackBox.enabled)
        {
            if (other.tag == "Player")
                player = other.GetComponent<PlayerStats>();
            if (Time.time - lastShadoSwordSlashesTime > ShadoSwordSlashesCooldown)
            {
                FistSwing();
            }
        }

    }
}
