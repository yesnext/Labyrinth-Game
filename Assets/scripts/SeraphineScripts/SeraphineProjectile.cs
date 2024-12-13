using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeraphineProjectile : MonoBehaviour
{
    private SeraphineControler boss;
    private PlayerStats player;
    private checkpoint1 playerstartposistion;
    private UniversalEnemyNeeds enemy;
    private Vector3 playerpos;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        playerstartposistion = FindObjectOfType<checkpoint1>();
        boss = FindObjectOfType<SeraphineControler>();
        player = FindObjectOfType<PlayerStats>();
        enemy = FindObjectOfType<UniversalEnemyNeeds>();
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
    void Update()
    {
        transform.position += playerpos * speed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("this ain't right");
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            boss.Bossphase = 1;
            boss.Health = boss.originalhealth;
            boss.telepoted =false;
            Vector3 respawnPosition = new Vector3(playerstartposistion.transform.position.x, playerstartposistion.transform.position.y, player.transform.position.z);
            player.transform.position = respawnPosition;
        }
    }
}
