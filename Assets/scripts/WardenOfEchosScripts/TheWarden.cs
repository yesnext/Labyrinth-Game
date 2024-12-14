using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TheWarden : MonoBehaviour
{
    public int CurrentNumOfObelisks;
    public WardenObelisks[] Obelisk;
    public float WardenSummonscooldown = 4.0f;
    public float lastWardenSummonscooldown;
    public GameObject minions;
    public Transform spawnlocation;
    public bool state = false;
    // Start is called before the first frame update
    void Start()
    {
            spawnlocation = FindObjectOfType<SummonsSpawnLocation>().transform;
            CurrentNumOfObelisks = FindObjectsOfType<WardenObelisks>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (state)
        {
            CheckObelisks();
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
                Destroy(this.gameObject);
            }
        }
    }
    public void OnTriggerEnter2D()
    {
        state = true;
        WardenObelisks.state=true;
    }
}
