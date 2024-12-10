using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    public WardenObelisks enemy;
    public PlayerStats player;
    private Vector3 playerpos;
    public int Damage;
    public void Intialize(int damage)
    {
        Damage = damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();

        enemy = FindObjectOfType<WardenObelisks>();
        if (enemy.isFacingRight == false)
        {
            playerpos = (player.transform.position - transform.position).normalized;
            this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            playerpos = (player.transform.position - transform.position).normalized;
            this.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += playerpos * speed * Time.deltaTime;
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
        }
    }
}
