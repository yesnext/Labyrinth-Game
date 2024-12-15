using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwareOfEveryMoveSense : MonoBehaviour
{
    public FinalBossController enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = FindObjectOfType<FinalBossController>();
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
            }
            else
            {
                int rand = Random.Range(1, 101);
                Debug.Log("random number" + rand);
                if (rand < 34)
                {
                    enemy.dodge = true;
                    enemy.IsImmune = true;
                }
                else
                {
                    enemy.IsImmune = false;
                    enemy.dodge = false;
                }
            }
        }
    }
}
