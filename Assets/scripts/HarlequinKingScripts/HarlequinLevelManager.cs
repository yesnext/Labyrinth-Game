using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarlequinLevelManager : MonoBehaviour
{
    private PlayerStats player;
    private checkpoint1 respawnpoint;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        respawnpoint =  FindObjectOfType<checkpoint1>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RespawnPlayer()
    {
        Vector3 respawnPosition = new Vector3(respawnpoint.transform.position.x, respawnpoint.transform.position.y, player.transform.position.z);
        player.transform.position = respawnPosition;
        FindObjectOfType<RoomDeathMatch>().onetime = true;
        FindObjectOfType<Parkour>().turnon = false;
    }
}
