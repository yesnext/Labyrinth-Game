using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationController : MonoBehaviour
{


    public void Quit()
    {

        Application.Quit();
    }
    public void GoToIntroScene()
    {
        AudioManager.Instance.StopAllAudio();
         SceneManager.LoadScene(0);


    }
    public void GoToGameScene()
    {
        AudioManager.Instance.StopAllAudio();
        SceneManager.LoadScene(1);
    }
}
