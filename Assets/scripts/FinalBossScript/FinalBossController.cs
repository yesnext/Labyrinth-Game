using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : BasicEnemy
{
    /*
    added projectile count variable to the script that will
    mainly take the input from the user for projectiles
    increment in the funtion that win instantiate the projectile
    decrement where the projectile will get destroyed

    edited the bullet controller so that when in contact with boss while immune it doesn't do anything
    */
    // Start is called before the first frame update
    public float BossDodgeSpeed = 0.1f;
    public bool IsImmune=true;
    
    public Transform ProjectilePoint;
    public GameObject Projectile;
    public int BossPhase = 1;
    
    public float RangeAttackDistance=10.0f;
    public float RangAttackCooldown = 5f; 
    public int RangeAttackDamage=6;    //time in seconds for the cooldown
    private float LastRangAttackTime = 0f;
    public bool Rangeanim;

    public float LungAttackDistance=5.0f;
    public float LungAttackCooldown = 5f; //time in seconds for the cooldown
    private float LastLungAttackTime = 0f;
    public bool lunganim;
    

    public float ShadoAttackDistance=5.0f;
    public int ShadoAttackDamage=6;
    public float ShadoSwordSlashesCooldown = 5f; //time in seconds for the cooldown
    private float lastRShadoSwordSlashesTime = 0f;
    public bool shadoslashanim;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<controls>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        Move();
        distance = Vector2.Distance(transform.position,player.transform.position);
        if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
                isFacingRight = true;
            }
            else if (direction.x < 0)
            {
                spriteRenderer.flipX = true;  
                 isFacingRight = false;
            }
            animator.GetComponent<Animator>();
    }
    public void FixedUpdate(){
        if(distance>=RangeAttackDistance && Time.time - LastRangAttackTime > RangAttackCooldown){
           RangeAttack();
        }
        else if(distance>=LungAttackDistance && Time.time-lastRShadoSwordSlashesTime>ShadoSwordSlashesCooldown){
        LungAttack();
        
        }
        else if(distance<ShadoAttackDistance && Time.time-lastRShadoSwordSlashesTime>ShadoSwordSlashesCooldown){
            ShadoAttack();
        }
    }

    public IEnumerator RangeAttack(){
            Rangeanim = true;
            animator.SetBool("RangeAnimation", Rangeanim);
            for (int i = 0;i<3;i++){
            Instantiate(Projectile,ProjectilePoint.position,ProjectilePoint.rotation);
            yield return new WaitForSeconds(0.2f);
            } 
            LastRangAttackTime = Time.time;
            Rangeanim = false;
            animator.SetBool("RangeAnimation", Rangeanim);
    }
    public void LungAttack(){
        if (Time.time-LastLungAttackTime > LungAttackCooldown){
            LastRangAttackTime = Time.time;
            lunganim = true;
            animator.SetBool("LungAnimation", lunganim);
            EnemySpeed = EnemySpeed*1.5f;
        }
    }
    public void ShadoAttack(){
            lastRShadoSwordSlashesTime = Time.time;
                lunganim=false;
                animator.SetBool("LungAnimation", lunganim);
                shadoslashanim = true;
                animator.SetBool("ShadoAnimation", shadoslashanim);
                distance = Vector2.Distance(transform.position,player.transform.position);
                if (distance<MeleeAttackDistance){
                player.TakeDamage(MeleeAttackDamage);
                }
                EnemySpeed=EnemySpeed/1.5f;
                shadoslashanim=false;
                animator.SetBool("ShadoAnimation", shadoslashanim);
    }
    public void Dodge(){
        if (controls.projectileCount<3){
        IsImmune=true;
        Vector3 dodgeDirection = new Vector3(3, 0, 0);  // Move right for simplicity
        transform.position += dodgeDirection * BossDodgeSpeed * Time.deltaTime;
        }
    }
    public void Move(){
        transform.position = Vector3.MoveTowards(transform.position,player.transform.position, EnemySpeed *Time.deltaTime);
    }
    public new void TakeDamage(int damage){
        Health=Health-damage;
        if (Health <= 0 && BossPhase>3){
            Destroy(this.gameObject);
        }
        else if(Health<=0){
            Health=100;
            BossPhase++;
        }
    }
    public bool IsBossImmune(){
        if (BossPhase == 1){
            if(controls.projectileCount>=3){
                return IsImmune=false;
            }
            else{
                return IsImmune=true;
            }
        }
        else{
            return IsImmune=false;
        }
    }
}
//code to be added to player controller
// public class PlayerShooting : MonoBehaviour
// {
//     public GameObject projectilePrefab;
//     public Transform shootPoint;
//     public static int projectileCount = 0; // Accessible static count for enemies to check

//     public float shootCooldown = 0.5f;
//     private float nextShootTime = 0f;

//     void Update()
//     {
//         if (Input.GetButtonDown("Fire1") && Time.time >= nextShootTime)
//         {
//             Shoot();
//             nextShootTime = Time.time + shootCooldown;
//         }
//     }

//     void Shoot()
//     {
//         Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
//         projectileCount++; // Increment the projectile count on each shot
//         Debug.Log("Projectile count: " + projectileCount);
//     }
// }
