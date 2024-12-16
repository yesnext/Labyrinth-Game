using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRun : MonoBehaviour
{
    public bool turnon;
    public Lava lava;
    public AudioSource audioSource;
    public AudioClip FireRangeAttackClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(turnon){
            StartLava();
        }
    }
    public void StartLava(){
        turnon = false;
        audioSource.PlayOneShot(FireRangeAttackClip);
        Instantiate(lava,transform.position, Quaternion.identity);
    }

}
