using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
      public Transform[] platforms; // Assign platforms here
    public BossController boss;
    public int maxHealth = 50;
    public int currentHealth;
    private float teleportInterval = 5f;
    private float healAmount = 10f;

    void Start()
    {
        currentHealth = maxHealth;
        if (platforms == null || platforms.Length == 0)
        {
            Debug.LogError("No platforms assigned!");
        }

        InvokeRepeating(nameof(Teleport), teleportInterval, teleportInterval);
    }

    void Teleport()
    {
        if (platforms.Length == 0) return;

        int randomIndex = Random.Range(0, platforms.Length);
        transform.position = platforms[randomIndex].position; // Move to random platform
        HealBoss();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void HealBoss()
    {
        if (boss != null)
        {
            boss.Heal(Mathf.RoundToInt(healAmount)); // Heal the boss
        }
    }

    private void Die()
    {
        Debug.Log("Companion Defeated!");
        if (boss != null)
        {
            boss.SetVulnerability(true); // Make the boss vulnerable
        }
        Destroy(gameObject);
    }
}