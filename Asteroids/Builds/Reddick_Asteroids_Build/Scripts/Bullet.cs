using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Fields needed for applying movement to the bullet
    public GameObject ship;
    private Vector3 direction;
    private Vector3 bulletPosition;
    private Vector3 velocity;
    private Vector3 acceleration = Vector3.zero;
    private float accelerationRate = 0.02f;

    // Fields for the asteroid variants
    GameObject asteroidObject;
    public GameObject asteroidObject1;
    public GameObject asteroidObject2;
    public GameObject asteroidObject3;
    public GameObject asteroidObject4;


    // Cam fields
    float totalCamWidth;
    float totalCamHeight;

    // Get the Asteroids and Collision Detector
    List<GameObject> asteroids;
    CollisionDetection detector;

    // New list is to store indices for destroyed items to be removed after destruction
    List<int> indices = new List<int>();


    // Start is called before the first frame update
    void Start()
    {
        // Lifetime of the bullet is only half a second
        Destroy(gameObject, 0.5f);

        // Apply initial bullet velocity, position, and direction to be the same as the ship's
        direction = ShipMovement.direction;
        velocity = ShipMovement.velocity;
        bulletPosition = transform.position;

        // Cam Dimensions
        Camera camera = Camera.main;
        totalCamHeight = camera.orthographicSize * 2f;
        totalCamWidth = camera.aspect * totalCamHeight;

        // Initializations
        asteroids = CollisionManager.asteroids;
        detector = GetComponent<CollisionDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        // Loops through every asteroid and if the bullet hits an
        // asteroid the asteroid will break and then add it's index
        // into the destroyed item's list
        for(int i = 0; i < asteroids.Count; i++)
        {
            if(detector.CircleCollision(gameObject, asteroids[i]))
            {
                indices.Add(i);
                
                Destroy(asteroids[i]);
                CollisionManager.score += 20;
                Destroy(gameObject);
            }

            // Go through each index of the destroyed items and remove it from asteroids
            foreach (int index in indices)
            {
                // Get the Asteroid Variant
                Vector3 size = asteroids[index].transform.localScale;
                if (asteroids[index].name == asteroidObject1.name + "(Clone)")
                {
                    asteroidObject = asteroidObject1;
                }
                else if (asteroids[index].name == asteroidObject2.name + "(Clone)")
                {
                    asteroidObject = asteroidObject2;
                }
                else if (asteroids[index].name == asteroidObject3.name + "(Clone)")
                {
                    asteroidObject = asteroidObject3;
                }
                else if (asteroids[index].name == asteroidObject4.name + "(Clone)")
                {
                    asteroidObject = asteroidObject4;
                }

                // Remove the Asteroid from the list and then check if it's a stage 1 asteroid
                asteroids.RemoveAt(index);

                // When it's a stage 1 asteroid the asteroid should split into two more asteroids
                if (size == asteroidObject.transform.localScale)
                {
                    for(int x = 0; x < 4; x++)
                    {
                        GameObject newAsteroid = Instantiate(asteroidObject, transform.position, Quaternion.identity);
                        newAsteroid.transform.localScale /= 2;
                        asteroids.Add(newAsteroid);
                    }
                }
            }
            indices.Clear();
        }

        // Calculations for applying acceleration to the bullet
        acceleration = direction * accelerationRate;
        velocity += acceleration;

        bulletPosition += velocity;


        // Conditionals for screen wrapping on the x-axis
        if (transform.position.x > totalCamWidth / 2 && !(transform.position.x > -totalCamWidth / 2 && transform.position.x < totalCamWidth / 2))
        {
            bulletPosition.x = -totalCamWidth / 2;
        }
        if (transform.position.x < -totalCamWidth / 2 && !(transform.position.x > -totalCamWidth / 2 && transform.position.x < totalCamWidth / 2))
        {
            bulletPosition.x = totalCamWidth / 2;
        }

        // Conditionals for screen wrapping on the y-axis
        if (transform.position.y > totalCamHeight / 2 && !(transform.position.y > -totalCamHeight / 2 && transform.position.y < totalCamHeight / 2))
        {
            bulletPosition.y = -totalCamHeight / 2;
        }
        if (transform.position.y < -totalCamHeight / 2 && !(transform.position.y > -totalCamHeight / 2 && transform.position.y < totalCamHeight / 2))
        {
            bulletPosition.y = totalCamHeight / 2;
        }

        transform.position = bulletPosition;
    }
}
