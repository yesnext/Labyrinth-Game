using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenSummons : AriseEnemies
{
    // Start is called before the first frame update
    public float strongercooldown =2.0f;
    public float laststronger;
    public int strongerdamageamount;
    void Start()
    {
        laststronger = Time.time;
        player = FindObjectOfType<PlayerStats>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        direction = (player.transform.position - transform.position).normalized;
        Followplayer();
        TheLongerTheyLive();
    }
    public void TheLongerTheyLive(){
        if(Time.time - laststronger>strongercooldown){
            laststronger = Time.time;
            MeleeAttackDamage+=strongerdamageamount;
        }
    }

}
