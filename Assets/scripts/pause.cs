using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class pause : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] AudioSource backgroundMusic;

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale=0;
        backgroundMusic.Pause();

    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
          Time.timeScale=1;
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
          Time.timeScale=1;
        backgroundMusic.Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    void Start()
    {
        PausePanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Pause();
        }
    }
}
