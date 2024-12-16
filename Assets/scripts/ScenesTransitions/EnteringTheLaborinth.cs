using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnteringTheLaborinth : MonoBehaviour
{
    private float distance = 1f;
    public float detectingdistance;
    public PlayerStats player;
    public KeyCode switchtoscene;
    public string nextscene;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (distance < detectingdistance && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(nextscene);
        }
    }
    public void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
    }
}
