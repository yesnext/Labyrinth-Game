using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echos : MonoBehaviour
{
    public static Echos instance; // Singleton instance
    public int numberOfEchos;

    void Awake()
    {
        // Check if there's already an instance of Echos
        if (instance == null)
        {
            instance = this; // Set the current instance
            DontDestroyOnLoad(gameObject); // Make this object persistent across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void Add(int amount)
    {
        numberOfEchos += amount;
        Debug.Log("Number of Echos: " + numberOfEchos);
    }

    public void remove(int amount){
        numberOfEchos -= amount;
        Debug.Log("Number of Echos: " + numberOfEchos);
    }

    public int echoCheck(){
        return numberOfEchos;
    }
}