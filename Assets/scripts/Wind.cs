using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private Vector3 windDirection; // Direction of the wind
    private float windSpeed = 2f; // Speed of the wind
    private Vector3 startPosition; // Starting position of the wind object
    private Rigidbody2D rb;
    public float maxDistance = 10f; // Maximum distance the wind can travel before being destroyed


void Start(){
    rb = GetComponent<Rigidbody2D>();
}
    // Method to initialize the wind direction, speed, and starting position
    public void Initialize(Vector3 direction)
    {
        windDirection = direction.normalized; // Normalize the direction vector
        startPosition = transform.position; // Set the starting position
    }

    void Update()
    {
        // Move the wind object in the direction it's pointing
     rb.velocity=transform.right*windSpeed;

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Redirector")
        {
            Destroy(this.gameObject);
        }
    }

}