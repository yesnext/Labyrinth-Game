using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public int requiredWinds = 4;
    private int currentWinds = 0;

    public void OnWindArrived()
    {
        currentWinds++;
        if (currentWinds >= requiredWinds)
        {
            Debug.Log("Puzzle Completed!");
            // Add logic for completing the puzzle here
        }
    }
}
