using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VitalityTheaft : MonoBehaviour
{
    public FinalBossController Boss;
    public PlayerStats player;
    public KeyCode healing;
    public int heal = 40;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Boss.BossPhase >= 2)
        {
            if (Input.GetKeyDown(healing))
            {
                if (Random.Range(1, 3) == 2)
                {
                    Boss.Heal(heal);
                }
                else
                {
                    player.Heal();
                }
            }

        }
    }
}
