using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField]
    private float delayforload = 3f;
    [SerializeField]
    private string sceneNameToLoad;

    private float timeelapsed;
    private void Update(){
        timeelapsed += Time.deltaTime;
        if(timeelapsed>delayforload){
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }
}