using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   
    public AudioSource musicSource;
    public static AudioManager Instance = null;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;
    // Start is called before the first frame update
    void Awake()
    {

        if (Instance == null)

            Instance = this;

        else if (Instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of Soundanager.
            Destroy(gameObject);
        //set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
   public void PlayMusic(AudioClip musicClip)
{
    musicSource.clip = musicClip;
    musicSource.Play();
}
   
    public void StopAllAudio()
{
    musicSource.Stop();
}

}

