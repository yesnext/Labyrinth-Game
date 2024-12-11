using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AriseEnemies : BasicEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        direction = (player.transform.position - transform.position).normalized;
        Followplayer();
    }
    public override void TakeDamage(int damage){
        Health=Health-damage;
            if (Health <= 0 ){
                Destroy(this.gameObject);
            }
    }
}
