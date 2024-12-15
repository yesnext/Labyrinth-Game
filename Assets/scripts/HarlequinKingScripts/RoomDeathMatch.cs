using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDeathMatch : UniversalEnemyNeeds
{
    public bool onetime = true;
    private SummonsSpawnLocation[] summonsSpawnLocations;
    public float spwanduration = 6f;
    public float Timebetweenspawns = 3f;
    private float lastspawnduration = 0f;
    public AriseEnemies SpawnEnemy;
    public checkpoint2 nextpuzzel;
    // Start is called before the first frame update
    void Start()
    {
        player =FindObjectOfType<PlayerStats>();
        summonsSpawnLocations = FindObjectsOfType<SummonsSpawnLocation>();
        nextpuzzel = FindObjectOfType<checkpoint2>();
        if(player.GetComponent<BossesDefeated>().Harlequinking){
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator StartDeathMatch()
    {
        while (Time.time - lastspawnduration < spwanduration)
        {
            foreach (SummonsSpawnLocation summonsSpawnLocation in summonsSpawnLocations)
                Instantiate(SpawnEnemy, summonsSpawnLocation.transform.position, summonsSpawnLocation.transform.rotation);
            yield return new WaitForSeconds(Timebetweenspawns);
        }
        foreach (AriseEnemies spawn in FindObjectsOfType<AriseEnemies>())
        {
            spawn.getdestroyed();
        }
        player.transform.position = new Vector3(nextpuzzel.transform.position.x, nextpuzzel.transform.position.y, player.transform.position.z);
        FindObjectOfType<Parkour>().turnon = true;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (onetime)
        {
            onetime = false;
            lastspawnduration += Time.time;
            StartCoroutine(StartDeathMatch());
        }
    }

}
