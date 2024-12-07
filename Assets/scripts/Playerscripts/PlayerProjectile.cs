using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
     public Transform ProjectilePoint;
     public float speed;
     public Vector3 direction;
     public  Vector3 mousePosition;
     public bool element;
     public int Damage=3;
    public void Intialize (Transform Firepoint){
        ProjectilePoint = Firepoint;
    }
    void Start()
    {
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
            if( !other.GetComponent<FinalBossController>().IsBossImmune()){
            other.GetComponent<FinalBossController>().TakeDamage(Damage);
            PlayerStates.ProjectileCount--;
            Debug.Log("in projectile damage");
            }
        }
        else if (other.tag == "Environment"){
            PlayerStates.ProjectileCount--;
            Destroy(this.gameObject);
        }

        
    }
}
