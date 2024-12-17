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
         SceneManager.LoadScene(1);


    }
}
