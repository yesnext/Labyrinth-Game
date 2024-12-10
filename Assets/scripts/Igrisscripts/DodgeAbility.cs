using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeAbility : MonoBehaviour
{
    public IgrisController enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = FindObjectOfType<IgrisController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            
            if (enemy.BossPhase == 1)
            {
                enemy.dodge = true;
                enemy.IsImmune = true;
            }
            
        }
    }
}
