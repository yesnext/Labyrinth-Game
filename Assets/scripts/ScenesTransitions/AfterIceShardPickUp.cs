using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterIceShardPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other){
        FindObjectOfType<PlayerStats>().unlockedIce = true;
        SceneManager.LoadScene("Post Harl Ice Choice");
    }
}
