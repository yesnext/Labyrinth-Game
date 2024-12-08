using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    public bool isVulnerable = false; // Set to true when the companion is dead
    public float speedtowardplayer = 0.1f;
    public int damage = 10; // Damage dealt by the boss

    private controls player;

    void Start()
    {
        currentHealth = maxHealth;
        player = FindObjectOfType<controls>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speedtowardplayer * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (!isVulnerable) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth); // Ensure health doesn't exceed max
    }

    public void SetVulnerability(bool state)
    {
        isVulnerable = state;
    }

    private void Die()
    {
        Debug.Log("Boss Defeated!");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerStats>().TakeDamage(damage);
        }
    }
}