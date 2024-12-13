using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class SeraphineControler : UniversalEnemyNeeds
{
    public int Bossphase = 1;
    private bool attacking;
    public float attackanimduration;
    private checkpoint2 shadorealmpoint;
    public float Shadowrealmperiod;
    public float shootingcooldown = 100f;
    public float lastshoottime;
    public EnemyProjectilePoint projectilepoint;
    public SeraphineProjectile projectile;
    public int originalhealth;
    private checkpoint1 playerstartposistion;
    public  bool telepoted;
    // Start is called before the first frame update
    void Start()
    {
        playerstartposistion = FindObjectOfType<checkpoint1>();
        shadorealmpoint = FindObjectOfType<checkpoint2>();
        player = FindObjectOfType<PlayerStats>();
        projectilepoint = FindObjectOfType<EnemyProjectilePoint>();
        originalhealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (Bossphase == 1)
        {
            if (!attacking)
            {
                Followplayer();
            }
            ChangedDirectionFollow();
            if (distance < meleeattackdistance && Time.time - lastMeleeAttackTime > MeleeAttackCooldown && !attacking && Bossphase == 1)
            {
                StartCoroutine(MeleeAttack());
            }
        }
        else
        {
            if (!telepoted)
            {
                StartCoroutine(teleport());
            }
            if (Time.time - lastshoottime > shootingcooldown)
            {
                shoot();
            }
        }
    }
    public IEnumerator teleport()
    {
        telepoted=true;
        Vector3 ShadowRealm = new Vector3(shadorealmpoint.transform.position.x, shadorealmpoint.transform.position.y, player.transform.position.z);
        player.transform.position = ShadowRealm;
        yield return new WaitForSeconds(Shadowrealmperiod);
        if (Bossphase == 2)
        {
            player.transform.position = new Vector3(playerstartposistion.transform.position.x, playerstartposistion.transform.position.y, player.transform.position.z);
            Destroy(this.gameObject);
        }
    }
    public void shoot()
    {
        SeraphineProjectile Projectile = Instantiate(projectile, projectilepoint.transform.position, projectilepoint.transform.rotation);
        SeraphineProjectile projectileController = Projectile.GetComponent<SeraphineProjectile>();
        projectileController.Intialize(RangeAttackSpeed);
        lastshoottime = Time.time;
    }
    public IEnumerator MeleeAttack()
    {
        attacking = true;
        //animation trigger
        yield return new WaitForSeconds(attackanimduration);
        lastMeleeAttackTime = Time.time;
        attacking = false;
    }
    public override void TakeDamage(int damage)
    {
        Health = Health - damage;
        if (Health <= 0)
        {
            Bossphase++;
        }
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
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
    }
}
