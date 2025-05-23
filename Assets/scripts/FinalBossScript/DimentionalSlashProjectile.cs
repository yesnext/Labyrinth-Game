using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimentionalSlashProjectile : MonoBehaviour
{
    public float speed;
    public FinalBossController enemy;
    public PlayerStats player;
    private Vector3 playerpos;
    public int Damage;
    public void Intialize(int damage, float Speed)
    {
        Damage = damage;
        speed = Speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();

        enemy = FindObjectOfType<FinalBossController>();
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
            player.TakeDamage(Damage);
            if (enemy.BossPhase == 1)
            {

                Destroy(this.gameObject);
            }
            else
            {
                enemy.Heal(Damage);
                Destroy(this.gameObject);
            }
        }
    }
}
