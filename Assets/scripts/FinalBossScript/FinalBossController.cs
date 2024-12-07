using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public PlayerStates player;
    public bool isFacingRight = true;
    public float EnemySpeed = 0.1f;
    public int Health = 20;
    private Animator animator;
    public float distance;

    public float BossDodgeSpeed = 8f;
    public float dodgeDuration=0.1f;
    public float DodgeDurationCounter = 0.0f;
    public bool dodge;

    public bool IsImmune=true;
    public Vector3 scale;
    public Vector2 direction;

    
    public Transform ProjectilePoint;
    public GameObject Projectile;
    public int BossPhase = 1;
    
    public float RangeAttackDistance=10.0f;
    public float RangAttackCooldown = 5f; 
    public int RangeAttackDamage=6;    //time in seconds for the cooldown
    private float LastRangAttackTime = -5.0f;
    public bool Rangeanim;

    public float LungAttackDistance=10.0f;
    public float LungAttackCooldown = 5f; //time in seconds for the cooldown
    private float LastLungAttackTime = -5.0f;
    public bool lunganim;
    

    public float ShadoAttackDistance=2.0f;
    public int ShadoAttackDamage=6;
    public float ShadoSwordSlashesCooldown = 5f; //time in seconds for the cooldown
    private float lastShadoSwordSlashesTime = -5.0f;
    public bool shadoslashanim;
    
    public float AriseCooldown = 45.0f;
    public float LastArisetime = -45.0f;
    public float spwanduration = 10f;
    public float lastspawnduration = 0;
    public float Timebetweenspawns = 1f;
    public float LastTimebetweenspawns =0f;
    public AriseEnemies[] ariseEnemies = new AriseEnemies[10];
    public Transform SpawnLocation;

    
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStates>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dodge==true){
            StartCoroutine(Dodge());
        }
        if(Time.time -LastArisetime > AriseCooldown){
            StartCoroutine(Arise());
        }
       GhangedirectionFollow();
       Followplayer();
       if(distance>=RangeAttackDistance && Time.time - LastRangAttackTime > RangAttackCooldown){
           RangeAttack();
        }
        else if(distance>=LungAttackDistance && Time.time-lastShadoSwordSlashesTime>ShadoSwordSlashesCooldown&& Time.time - LastLungAttackTime>LungAttackCooldown){
        LungAttack();
        
        }
        else if(distance<ShadoAttackDistance && Time.time-lastShadoSwordSlashesTime>ShadoSwordSlashesCooldown){
        ShadoAttack();
        } 
        
    }
    public void FixedUpdate(){
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position,player.transform.position);
        
    }

    public IEnumerator RangeAttack(){
            // Rangeanim = true;
            // animator.SetBool("RangeAnimation", Rangeanim);
            for (int i = 0;i<3;i++){
            Instantiate(Projectile,ProjectilePoint.position,ProjectilePoint.rotation);
            yield return new WaitForSeconds(0.2f);
            } 
            LastRangAttackTime = Time.time;
            // Rangeanim = false;
            // animator.SetBool("RangeAnimation", Rangeanim);
    }
    public void LungAttack(){
        if (Time.time-LastLungAttackTime > LungAttackCooldown){
            LastLungAttackTime = Time.time;
            EnemySpeed*=1.5f;
            // lunganim = true;
            // animator.SetBool("LungAnimation", lunganim);
        }
    }
    public void ShadoAttack(){
            lastShadoSwordSlashesTime = Time.time;
                // lunganim=false;
                // animator.SetBool("LungAnimation", lunganim);
                // shadoslashanim = true;
                // animator.SetBool("ShadoAnimation", shadoslashanim);
                distance = Vector2.Distance(transform.position,player.transform.position);
                if (distance<ShadoAttackDistance){
                player.TakeDamage(ShadoAttackDamage);
                }
                EnemySpeed=EnemySpeed/1.5f;
                // shadoslashanim=false;
                // animator.SetBool("ShadoAnimation", shadoslashanim);
    }
    public IEnumerator Dodge()
{   Debug.Log("inside Dodge"+dodge);
    dodge=false;
    
    if (PlayerStates.ProjectileCount < 3)
    {
        Debug.Log("inside Dodge"+dodge);
        IsImmune = true;
        Vector3 dodgeDirection = (transform.position - player.transform.position).normalized;
        DodgeDurationCounter = 0f;
        float originalspeed = EnemySpeed;
        EnemySpeed *=6; 
        while (DodgeDurationCounter < dodgeDuration)
        {
            Followplayer();
            DodgeDurationCounter += Time.deltaTime;
            yield return null;
        }
        EnemySpeed = originalspeed;
        transform.position += dodgeDirection * BossDodgeSpeed * Time.deltaTime;
    }
}
    public new void Followplayer()
    {
        // animator.SetBool("Walking", true);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, EnemySpeed * Time.deltaTime);
       
        GhangedirectionFollow();
        if (distance < 4.0f){
            // animator.SetBool("Walking", false);
            // animator.SetBool("attack",true);
        }
        else{
            // animator.SetBool("attack",false);
        }
    }
    public void GhangedirectionFollow(){
        if (direction.x > 0 && !isFacingRight)
        {
            Debug.Log("inchnage direction");
            scale =transform.localScale;
            scale.x *= -1;
            isFacingRight = true;
            transform.localScale=scale;
            
        }
        else if (direction.x < 0 && isFacingRight)
        {
            scale =transform.localScale;
            scale.x *= -1;
            isFacingRight = false;
            transform.localScale=scale;
        }
    }
    public void TakeDamage(int damage){
        Health=Health-damage;
        
        if(!IsBossImmune()){
            if (Health <= 0 && BossPhase>3){
                Destroy(this.gameObject);
            }
            else if(Health<=0){
                Health=100;
                BossPhase++;
            }
        }
    }
    public bool IsBossImmune(){
        if (BossPhase == 1){
            if(PlayerStates.ProjectileCount>=3){
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
    public void OnTriggerEnter2D(Collider2D other){
        if (other.tag=="Player"){
            player= other.GetComponent<PlayerStates>();
            if(Time.time-lastShadoSwordSlashesTime>ShadoSwordSlashesCooldown){
            ShadoAttack();   
            }
        }
    }
    public void Heal(){
        Health+=40;
        if(Health>500){
            Health=500;
        }
    }
    public IEnumerator Arise(){
        Debug.Log("inside");
        IsImmune = true;
    DodgeDurationCounter = 0f;
    float originalSpeed = EnemySpeed;
    EnemySpeed = 0f;
    lastspawnduration = 0f;
        while (lastspawnduration < spwanduration)
        {

            if (Time.time - LastTimebetweenspawns > Timebetweenspawns)
            {
                LastTimebetweenspawns = Time.time;
                int randomIndex = Random.Range(0, ariseEnemies.Length);
                Instantiate(ariseEnemies[randomIndex], SpawnLocation.position, SpawnLocation.rotation);
            }

            lastspawnduration += Time.deltaTime;
            yield return null;

        LastArisetime = Time.time;
        EnemySpeed = originalSpeed;
        Debug.Log("Arise function completed.");
        }
    }
}

