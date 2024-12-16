using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheWarden : MonoBehaviour
{
    public int CurrentNumOfObelisks;
    public WardenObelisks[] Obelisk;
    public float WardenSummonscooldown = 4.0f;
    public float lastWardenSummonscooldown;
    public GameObject minions;
    public Transform spawnlocation;
    public bool state = false;
    private PlayerStats player;
    // Start is called before the first frame update
    void Start()
    {
        player=FindObjectOfType<PlayerStats>();
        spawnlocation = FindObjectOfType<SummonsSpawnLocation>().transform;
        CurrentNumOfObelisks = FindObjectsOfType<WardenObelisks>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        CheckObelisks();
        if (state)
        {
            if (Time.time - lastWardenSummonscooldown > WardenSummonscooldown)
            {
                Summon();
            }
        }
    }
    public void Summon()
    {
        lastWardenSummonscooldown = Time.time;
        Instantiate(minions, spawnlocation.position, spawnlocation.rotation);
    }
    public void CheckObelisks()
    {
        if (FindObjectsOfType<WardenObelisks>().Length < CurrentNumOfObelisks)
        {
            CurrentNumOfObelisks = FindObjectsOfType<WardenObelisks>().Length;
            if (CurrentNumOfObelisks == 0)
            {
                player.GetComponent<BossesDefeated>().warden = true;
                SceneManager.LoadScene("Outside The Labyrinth");
                Destroy(this.gameObject);
            }
        }
    }
    public void OnTriggerEnter2D()
    {
        state = true;
        WardenObelisks.state = true;
    }
}
