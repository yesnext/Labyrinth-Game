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
    public AudioClip healClip;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Boss = FindObjectOfType<FinalBossController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Boss != null)
        {
            if (Boss.BossPhase >= 2)
            {
                if (Input.GetKeyDown(healing))
                {
                    audioSource.PlayOneShot(healClip);
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
}
