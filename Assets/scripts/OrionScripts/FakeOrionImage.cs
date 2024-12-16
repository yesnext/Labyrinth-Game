using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeOrionImage : UniversalEnemyNeeds
{
    public SummonsSpawnLocation spawnlocation;
    public HealingOrion healingOrion;
    public void Intialize(SummonsSpawnLocation spawnloc)
    {
        spawnloc.ocupied = true;
        spawnlocation = spawnloc;
    }
    void Start()
    {
        healingOrion = FindObjectOfType<HealingOrion>();
        player = FindObjectOfType<PlayerStats>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ChangedDirectionFollow();
        if (Time.time - healingOrion.LastTeleportTime > healingOrion.teleportcooldown && !healingOrion.Teleporting)
        {
            StartCoroutine(teleport());
        }
    }
    public IEnumerator teleport()
    { 
        audioSource.PlayOneShot(RangeAttackClip);
        yield return new WaitForSeconds(healingOrion.TeleportingAnimationDuration);
    }
    public void FixedUpdate()
    {
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.transform.position);
    }
    public override void TakeDamage(int damage)
    {
        Health = Health - damage;
        if (Health <= 0)
        {
            spawnlocation.ocupied = false;
            Destroy(this.gameObject);
        }
    }
}
