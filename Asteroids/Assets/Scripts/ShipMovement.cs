using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private Vector3 vehiclePosition = new Vector3(0, 0, 0);

    // Direction the vehicle is facing
    public static Vector3 direction = Vector3.right; // or new Vector3(1, 0, 0)

    // Velocity of the vehicle
    public static Vector3 velocity = Vector3.zero;

    public float maxSpeed = 5f;
    public float turnSpeed = 3f;

    // Acceleration vector will calculate the rate of change per second
    private Vector3 acceleration = Vector3.zero;

    public float accelerationRate = 0.001f;
    public float decelerationRate = 0.95f;

    // Fields for key input
    private bool isAccelerating = false;
    private bool turningRight = false;
    private bool turningLeft = false;

    // Cam fields
    float totalCamWidth;
    float totalCamHeight;

    void Start()
    {
        // Cam Dimensions
        Camera camera = Camera.main;
        totalCamHeight = camera.orthographicSize * 2f;
        totalCamWidth = camera.aspect * totalCamHeight;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Input for Up Arrow Key
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isAccelerating = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isAccelerating = false;
        }

        // Get Input for Right Arrow Key
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            turningRight = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            turningRight = false;
        }

        // Get Input for Left Arrow Key
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            turningLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            turningLeft = false;
        }

        // Conditionals for turning
        if (turningRight)
        {
            // Rotate the direction vector by turn speed degrees each frame
            direction = Quaternion.Euler(0, 0, -turnSpeed) * direction;
        }
        if (turningLeft)
        {
            // Rotate the direction vector by turn speed degrees each frame
            direction = Quaternion.Euler(0, 0, turnSpeed) * direction;
        }

        // Conditional for when the car is accelerating/decelerating
        if (isAccelerating)
        {
            // Calculate the acceleration vector
            acceleration = direction * accelerationRate;

            // Velocity is direction * speed
            velocity += acceleration;
        }
        else
        {
            // Decelerate the vehicle 
            velocity *= decelerationRate;
        }

        // Limit the Velocity so the vehicle doesnt go to fast
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Add velocity to our position
        vehiclePosition += velocity;

        // Conditionals for screen wrapping on the x-axis
        if (transform.position.x > totalCamWidth / 2 && !(transform.position.x > -totalCamWidth / 2 && transform.position.x < totalCamWidth / 2))
        {
            vehiclePosition.x = -totalCamWidth / 2;
        }
        if (transform.position.x < -totalCamWidth / 2 && !(transform.position.x > -totalCamWidth / 2 && transform.position.x < totalCamWidth / 2))
        {
            vehiclePosition.x = totalCamWidth / 2;
        }

        // Conditionals for screen wrapping on the y-axis
        if (transform.position.y > totalCamHeight / 2 && !(transform.position.y > -totalCamHeight / 2 && transform.position.y < totalCamHeight / 2))
        {
            vehiclePosition.y = -totalCamHeight / 2;
        }
        if (transform.position.y < -totalCamHeight / 2 && !(transform.position.y > -totalCamHeight / 2 && transform.position.y < totalCamHeight / 2))
        {
            vehiclePosition.y = totalCamHeight / 2;
        }

        // Move the vehicle to it's new position
        transform.position = vehiclePosition;

        // Set the Vehicle's rotation to match the direction
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }
}
