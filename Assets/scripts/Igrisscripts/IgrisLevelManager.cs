using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgrisLevelManager : MonoBehaviour
{
    public PlayerStats player;
    public IgrisController Igris;
    public GameObject IgrisRespawnPoint;
    public GameObject PlayerResPawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        Igris = FindObjectOfType<IgrisController>();
        IgrisRespawnPoint = GameObject.FindGameObjectWithTag("IgrisCheckPoint");
        PlayerResPawnpoint = GameObject.FindGameObjectWithTag("PlayerCheckPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.firstencounter && player.Health <= 0)
        {
            Igris.aggro = false;
            Igris.BossPhase = 2;
            Igris.Health = 500;
            player.Health = 100;
            player.transform.position = PlayerResPawnpoint.transform.position;
            Igris.transform.position = IgrisRespawnPoint.transform.position;
            Igris.IsImmune = false;
            Igris.attackbox.enabled = false;
            Igris.FistAttackBox.enabled = true;
        }
    }
}
