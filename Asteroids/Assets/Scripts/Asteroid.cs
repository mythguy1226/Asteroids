using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Velocity fields
    private Vector3 direction;
    private Vector3 asteroidPosition;
    private Vector3 velocity;
    private float speed = 0.01f;

    // Cam fields
    float totalCamWidth;
    float totalCamHeight;

    // Start is called before the first frame update
    void Start()
    {
        // Cam Dimensions
        Camera camera = Camera.main;
        totalCamHeight = camera.orthographicSize * 2f;
        totalCamWidth = camera.aspect * totalCamHeight;

        // Initialize Vectors for Asteroid
        asteroidPosition = transform.position;
        direction = new Vector3(Random.Range(-Mathf.PI, Mathf.PI), Random.Range(-Mathf.PI, Mathf.PI), 0);
        velocity = speed * direction;
    }

    // Update is called once per frame
    void Update()
    {
        // Update Asteroid Position
        asteroidPosition += velocity;


        // Conditionals for screen wrapping on the x-axis
        if (transform.position.x > totalCamWidth / 2 && !(transform.position.x > -totalCamWidth / 2 && transform.position.x < totalCamWidth / 2))
        {
            asteroidPosition.x = -totalCamWidth / 2;
        }
        if (transform.position.x < -totalCamWidth / 2 && !(transform.position.x > -totalCamWidth / 2 && transform.position.x < totalCamWidth / 2))
        {
            asteroidPosition.x = totalCamWidth / 2;
        }

        // Conditionals for screen wrapping on the y-axis
        if (transform.position.y > totalCamHeight / 2 && !(transform.position.y > -totalCamHeight / 2 && transform.position.y < totalCamHeight / 2))
        {
            asteroidPosition.y = -totalCamHeight / 2;
        }
        if (transform.position.y < -totalCamHeight / 2 && !(transform.position.y > -totalCamHeight / 2 && transform.position.y < totalCamHeight / 2))
        {
            asteroidPosition.y = totalCamHeight / 2;
        }

        // After everything is updated make the actual position in-game change
        transform.position = asteroidPosition;
    }
}
