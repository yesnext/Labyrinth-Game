using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : MonoBehaviour
{
    /*
    added projectile count variable to the script that will
    mainly take the input from the user for projectiles
    increment in the funtion that win instantiate the projectile
    decrement where the projectile will get destroyed

    edited the bullet controller so that when in contact with boss while immune it doesn't do anything
    */
    // Start is called before the first frame update
    private controls player;
    public bool isFacingRight = false;
    public float BossSpeed = 0.1f;
    public float BossDodgeSpeed = 0.1f;
    public bool IsImmune=true;
    public int Health;
    public Transform ProjectilePoint;
    public GameObject Projectile;
    public int BossPhase = 1;
    private SpriteRenderer spriteRenderer;
    
    public float RangeAttackDistance=10.0f;
    public float RangAttackCooldown = 5f; //time in seconds for the cooldown
    private float LastRangAttackTime = 0f;
    public bool Rangeanim;

    public float LungAttackDistance=5.0f;
    public float LungAttackCooldown = 5f; //time in seconds for the cooldown
    private float LastLungAttackTime = 0f;
    public bool lunganim;
    

    public float ShadoAttackDistance=5.0f;
    public float ShadoSwordSlashesCooldown = 5f; //time in seconds for the cooldown
    private float lastRShadoSwordSlashesTime = 0f;
    public bool shadoslashanim;
    public int ShadoSlashDamage = 0;
  
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<controls>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        Move();
        float distance = Vector2.Distance(transform.position,player.transform.position);
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
        
        
        if(distance>=RangeAttackDistance && Time.time - LastRangAttackTime > RangAttackCooldown){
           RangeAttack();
        }
        else if(distance>=LungAttackDistance){
        LungAttack();
        }
        else if(distance<ShadoAttackDistance){
            ShadoAttack();
            shadoslashanim = false;
        }
        
    }

    public IEnumerator RangeAttack(){
            Rangeanim = true;
            for (int i = 0;i<3;i++){
            Instantiate(Projectile,ProjectilePoint.position,ProjectilePoint.rotation);
            yield return new WaitForSeconds(0.2f);
            } 
            LastRangAttackTime = Time.time;
            Rangeanim = false;
    }
    public void LungAttack(){
        if (Time.time-LastLungAttackTime > LungAttackCooldown){
            LastRangAttackTime = Time.time;
            lunganim = true;
            BossSpeed = BossSpeed*1.5f;
            ShadoAttack();
        }
    }
    public void ShadoAttack(){
        if (Time.time-lastRShadoSwordSlashesTime>ShadoSwordSlashesCooldown){
            lastRShadoSwordSlashesTime = Time.time;
            lunganim=false;
            shadoslashanim = true;
            BossSpeed=BossSpeed/1.5f;
        }
    }
    public void Dodge(){
        if (controls.projectileCount<3){
        IsImmune=true;
        Vector3 dodgeDirection = new Vector3(1, 0, 0);  // Move right for simplicity
        transform.position += dodgeDirection * BossDodgeSpeed * Time.deltaTime;
        }
    }
    public void Move(){
        transform.position = Vector3.MoveTowards(transform.position,player.transform.position, BossSpeed *Time.deltaTime);
    }
    public void TakeDamage(int damage){
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
