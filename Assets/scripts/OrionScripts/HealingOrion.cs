using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class HealingOrion : UniversalEnemyNeeds
{
    public float teleportcooldown = 8.0f;
    public float LastTeleportTime;
    public AttackingOrion Attackingorion;
    public GameObject fakes;
    private Attackorionloction AttackingorionLocation;
    private SummonsSpawnLocation[] teleportlocations;
    private SummonsSpawnLocation previouslocation;
    private bool onetime = true;
    private bool summoningOrion;
    public float TeleportingAnimationDuration;
    public bool Teleporting;
    // Start is called before the first frame update
    void Start()
    {
        LastTeleportTime = -teleportcooldown;
        player = FindObjectOfType<PlayerStats>();
        teleportlocations = FindObjectsOfType<SummonsSpawnLocation>();
        AttackingorionLocation = FindObjectOfType<Attackorionloction>();
        if(player.GetComponent<BossesDefeated>().orion){
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangedDirectionFollow();
        if (aggro && Time.time - LastTeleportTime > teleportcooldown && !Teleporting &&!summoningOrion)
        {
            StartCoroutine(teleport());
        }
        if (distance < aggrodistance && onetime)
        {
            StartCoroutine(SummonAttackOrion());
            aggro = true;
        }
    }
    public IEnumerator teleport()
    {
        Teleporting = true;
        yield return new WaitForSeconds(TeleportingAnimationDuration);
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
        Teleporting = false;
        LastTeleportTime = Time.time;
    }
    public IEnumerator SummonAttackOrion()
    {
        summoningOrion = true;
        yield return new WaitForSeconds(0);
        if (onetime)
        {
            Instantiate(Attackingorion, AttackingorionLocation.transform.position, Attackingorion.transform.rotation);
            onetime = false;
        }
        summoningOrion = false;
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);

    }
    public override void TakeDamage(int damage)
    {
        if (aggro)
        {
            Health = Health - damage;
            if (Health <= 0)
            {
                Attackingorion.IsImmune = false;
                foreach (FakeOrionImage fake in FindObjectsOfType<FakeOrionImage>())
                {
                    Destroy(fake.gameObject);
                }
                player.GetComponent<BossesDefeated>().orion = true;
                Destroy(this.gameObject);
            }
        }
    }

}
