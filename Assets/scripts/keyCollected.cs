using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyCollected : MonoBehaviour
{
    public static keyCollected instance; // Singleton instance
    public bool KeyCollected = false;

    void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the key
            KeyCollected = true; // Mark key as collected
            Debug.Log("Key collected!");
            //switch scene back to puzzle 3
        }
    }
}
