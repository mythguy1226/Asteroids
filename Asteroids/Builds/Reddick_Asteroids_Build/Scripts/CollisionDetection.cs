using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method for Collisions
    public bool CircleCollision(GameObject object1, GameObject object2)
    {
        // Get the centers and radius values for both objects
        Vector3 object1Center = object1.GetComponent<SpriteRenderer>().bounds.center;
        float object1Radius = object1.GetComponent<SpriteRenderer>().bounds.extents.x;
        Vector3 object2Center = object2.GetComponent<SpriteRenderer>().bounds.center;
        float object2Radius = object2.GetComponent<SpriteRenderer>().bounds.extents.x;

        // Calculate the distance between the two centers using Pythagorean's Theorem
        float distance = Mathf.Sqrt(Mathf.Pow((object1Center.x - object2Center.x), 2) + Mathf.Pow((object1Center.y - object2Center.y), 2));

        // Conditional for Collisions based on distance between the two circles and the radius of each circle
        if (distance < (object1Radius + object2Radius))
        {
            return true;
        }
        return false;
    }
}
