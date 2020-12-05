using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Fields
    public GameObject ship;
    public GameObject bullet;

    // All Sounds the ship makes goes into this Array
    new AudioSource[] audio;

    // Start is called before the first frame update
    void Start()
    {
        // Initializations
        ship = GetComponent<ShipMovement>().gameObject;
        audio = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot everytime the player presses space
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, new Vector3(ship.transform.position.x, ship.transform.position.y, ship.transform.position.z), Quaternion.identity);
            audio[0].Play();
        }
    }
}
