using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    public AudioClip musicClip; // Assign this in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        // Check if the AudioManager instance exists
        if (AudioManager.Instance != null)
        {
            // Play the assigned music clip
            AudioManager.Instance.PlayMusic(musicClip);
        }
        else
        {
            Debug.LogWarning("AudioManager instance is not available.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}