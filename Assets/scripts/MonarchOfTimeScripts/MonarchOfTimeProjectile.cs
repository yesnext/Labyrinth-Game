using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonarchOfTimeProjectile : MonoBehaviour
{
    public float speed;
    public UniversalEnemyNeeds enemy;
    public PlayerStats player;
    private Vector3 playerpos;
    public int Damage;
    public float SlowRate;
    public void Intialize(int damage)
    {
        Damage = damage;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();

        enemy = FindObjectOfType<UniversalEnemyNeeds>();
        if (enemy.isFacingRight == false)
        {
             
            this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            
            this.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (player.transform.position - transform.position).normalized * speed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Environment")
        {
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            player.TakeDamage(Damage);
            player.GetComponent<controls>().speed /= SlowRate;
        }
    }
}
