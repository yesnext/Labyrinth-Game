using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class HealingOrion : UniversalEnemyNeeds
{
    private float teleportcooldown = 8.0f;
    private float LastTeleportTime;
    public bool agrue = false;
    public float agruedistance = 3.0f;
    public AttackingOrion Attackingorion;
    public GameObject fakes;
    private Attackorionloction AttackingorionLocation;
    private SummonsSpawnLocation[] teleportlocations;
    private SummonsSpawnLocation previouslocation;
    private bool onetime = true;
    // Start is called before the first frame update
    void Start()
    {
        LastTeleportTime = -teleportcooldown;
        player = FindObjectOfType<PlayerStats>();
        teleportlocations = FindObjectsOfType<SummonsSpawnLocation>();
        AttackingorionLocation = FindObjectOfType<Attackorionloction>();
    }

    // Update is called once per frame
    void Update()
    {
        GhangedirectionFollow();
        if (agrue)
        {
            teleport();
        }
        if (distance < agruedistance && onetime)
        {
            SummonAttackOrion();
            agrue = true;
        }
    }
    public void teleport()
    {
        if (Time.time - LastTeleportTime > teleportcooldown)
        {
            foreach (SummonsSpawnLocation teleport in teleportlocations)
            {
                if (!teleport.ocupied)
                {
                    Vector3 respawnPosition = new Vector3(teleport.transform.position.x, teleport.transform.position.y, this.transform.position.z);
                    teleport.ocupied = true;
                    this.transform.position = respawnPosition;
                    if (previouslocation != null)
                    {
                        previouslocation.ocupied = false;
                    }
                    previouslocation = teleport;
                    break;
                }
            }
            foreach (SummonsSpawnLocation teleport in teleportlocations)
            {
                if (!teleport.ocupied)
                {
                    GameObject minion = Instantiate(fakes, teleport.transform.position, teleport.transform.rotation);
                    FakeOrionImage minioncontroller = minion.GetComponent<FakeOrionImage>();
                    minioncontroller.Intialize(teleport);
                }
            }
            LastTeleportTime = Time.time;

        }
    }
    public void SummonAttackOrion()
    {
        if (onetime)
        {
            Instantiate(Attackingorion, AttackingorionLocation.transform.position, Attackingorion.transform.rotation);
            onetime = false;
        }
    }
    public override void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);

    }
    public override void TakeDamage(int damage)
    {
        Health = Health - damage;
        if (Health <= 0)
        {
            Attackingorion.IsImmune = false;
            foreach(FakeOrionImage fake in FindObjectsOfType<FakeOrionImage>()){
                Destroy(fake.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

}
