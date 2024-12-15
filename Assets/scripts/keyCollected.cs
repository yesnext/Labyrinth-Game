using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class keyCollected : MonoBehaviour
{
    public static keyCollected instance; // Singleton instance
    public bool KeyCollected = false;
    public string sceneToLoad; 

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
            SceneManager.LoadScene(sceneToLoad);
            //switch scene back to puzzle 3
        }
    }

    public bool isCollected(){
        return KeyCollected;
    }
}
