using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwareOfEveryMoveSense : MonoBehaviour
{
    public FinalBossController enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void OnTriggerEnter2D(Collider2D other){
        if (enemy.BossPhase== 1){
            if(other.tag == "Projectile"){
                enemy.dodge = true;
            }
        }
     }
}
