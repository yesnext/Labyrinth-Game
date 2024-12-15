using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    

    public void Quit()
    {

        Application.Quit();
    }
    public void GoToIntroScene()
{
    AudioManager.Instance.StopAllAudio();
    Application.LoadLevel(0);


}
public void GoToGameScene()
{
    AudioManager.Instance.StopAllAudio();
    Application.LoadLevel(1);
}
}
