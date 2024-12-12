using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public WindGenerator[] generators;
     public Redirector[] redirectors;
    public float windInterval = 2f;

    private void Start()
    {
        StartCoroutine(GenerateWinds());
    StartCoroutine(RedirectWind());
    }

    private System.Collections.IEnumerator GenerateWinds()
    {
        while (true)
        {
            foreach (var generator in generators)
            {
                generator.GenerateWind();
            }
            yield return new WaitForSeconds(windInterval);
        }
    }
    private System.Collections.IEnumerator RedirectWind()
    {
        while (true)
        {
            foreach (var redirector in redirectors)
            {
                redirector.RedirectWind();
            }
            yield return new WaitForSeconds(windInterval);
        }
    }
}
