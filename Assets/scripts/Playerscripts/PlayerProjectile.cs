using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
     public Transform ProjectilePoint;
     public float speed;
     public float speedMultiplier = 0;
     public Vector3 direction;
     public  Vector3 mousePosition;
     public bool element;
     public int Damage=3;
     public PlayerStates Player;
    public void Intialize (Transform Firepoint){
        ProjectilePoint = Firepoint;
        
    }
    void Start()
    {
        Player = FindObjectOfType<PlayerStates>();
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; 
        direction = (mousePosition - ProjectilePoint.position).normalized;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    public void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Enemy"){
            other.GetComponent<BasicEnemy>().TakeDamage(Damage,element);
            PlayerStates.ProjectileCount--;
            Destroy(this.gameObject);
        }
        else if(other.tag == "Boss"){
            if(other.GetComponent<FinalBossController>().BossPhase==1){
                if( !other.GetComponent<FinalBossController>().IsImmune){
                    other.GetComponent<FinalBossController>().TakeDamage(Damage);
                    PlayerStates.ProjectileCount--;
                    Destroy(this.gameObject);
                }
            }
        }
        else if (other.tag == "Environment"){
            PlayerStates.ProjectileCount--;
            Destroy(this.gameObject);
        } else if (other.tag == "Arise Enemy"){
            other.GetComponent<AriseEnemies>().TakeDamage(Damage);
            PlayerStates.ProjectileCount--;
            Destroy(this.gameObject);
        }

        
    }
}
