using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackingOrion : UniversalEnemyNeeds
{
    private HealingOrion healingorion;

    // Start is called before the first frame update
    void Start()
    {
        IsImmune = true;
        healingorion = FindObjectOfType<HealingOrion>();
        player = FindObjectOfType<PlayerStats>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Followplayer();
        ChangedDirectionFollow();
        if (healingorion == null)
        {
            IsImmune = false;
        }
        if(distance<meleeattackdistance && Time.time-lastMeleeAttackTime>MeleeAttackCooldown && !MeleeAttacking){
            StartCoroutine(MeleeAttack());
        }
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
    }
    public IEnumerator MeleeAttack()
    {
        audioSource.PlayOneShot(meleeAttackClip1);
        MeleeAttacking = true;
        yield return new WaitForSeconds(MeleeAttackAnimationDuration);
        MeleeAttacking = false;
        lastMeleeAttackTime = Time.time;
    }    public override void TakeDamage(int damage)
    {
        if (!IsImmune)
        {
            Health = Health - damage;
            if (Health <= 0)
            {
                Destroy(this.gameObject);
            }
        }

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Time.time - lastMeleeAttackTime > MeleeAttackCooldown)
            {
                SceneManager.LoadScene("Warden");
                player.TakeDamage(MeleeAttackDamage);
            }
        }
    }
}
