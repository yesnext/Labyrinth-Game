using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGenerator : MonoBehaviour
{
    public GameObject windPrefab; // Prefab of the wind object
    public Transform windStartPoint; // Where the wind starts
    public int windRotationAngle; // Rotation angle in degrees (can be adjusted in the Inspector)

    void OnMouseDown()
{
  windRotationAngle = (windRotationAngle + 45) % 360;
}
    public void GenerateWind()
    {
        Debug.Log(windRotationAngle);
        // Calculate the rotation for the wind object
        Quaternion rotation = Quaternion.Euler(0, 0, windRotationAngle);

        // Instantiate the wind object with the calculated rotation
        GameObject wind = Instantiate(windPrefab, windStartPoint.position, rotation);
        
        // Initialize the wind object with its direction and speed
        Vector3 windDirection = rotation * Vector3.forward; // Forward direction after applying rotation
       
    }
}