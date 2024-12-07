using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalityTheaft : MonoBehaviour
{
    public FinalBossController Boss;
    public PlayerStates player;
    public KeyCode healing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss.BossPhase == 1){
        if(Input.GetKeyDown(healing)){
            if(Random.Range(1,3) ==2){
                Boss.Heal();
            }
            else{
                player.Heal();
            }
        }
        }
    }
}
