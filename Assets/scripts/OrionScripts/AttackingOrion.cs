using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingOrion : UniversalEnemyNeeds
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Followplayer ();
        GhangedirectionFollow();
    }
    public void TakeDamage(int damage)
    {
        
        Health = Health - damage;
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
        

    }
}
