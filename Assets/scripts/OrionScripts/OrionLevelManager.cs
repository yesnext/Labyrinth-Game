using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrionLevelManager : MonoBehaviour
{
    private Transform respawnpoint;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        respawnpoint = GameObject.FindGameObjectWithTag("PlayerCheckPoint").transform;
        player = FindObjectOfType<PlayerStats>().transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void respawnplayer()
    {
        Vector3 respawnPosition = new Vector3(respawnpoint.position.x, respawnpoint.position.y, player.position.z);
        player.position = respawnPosition;

    }
}
