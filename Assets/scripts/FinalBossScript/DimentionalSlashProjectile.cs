using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimentionalSlashProjectile : MonoBehaviour
{
    public float speed;
    public FinalBossController enemy;
    public PlayerStates player;
    private Vector3 playerpos;
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {   player = FindObjectOfType<PlayerStates>(); 
        
        enemy=FindObjectOfType<FinalBossController>();
           if(enemy.isFacingRight==false){
            playerpos=(player.transform.position - transform.position).normalized;
        this.transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
        }else{
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed,GetComponent<Rigidbody2D>().velocity.y);}
        this.transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y,transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += playerpos * speed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D other){
       if(other.tag == "Environment"){
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player"){
            player.TakeDamage(Damage);
            if(enemy.BossPhase ==1){
            enemy.Heal(Damage);
            Destroy(this.gameObject);
            }
        }
    }
}
