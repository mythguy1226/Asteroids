using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{
    // Fields
    CollisionDetection detector;
    public GameObject ship;
    GameObject asteroid;
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;
    public GameObject asteroid4;
    public Texture texture;
    public static int score;
    GameObject player;
    public GameObject bullet;
    public int lives = 3;
    public static bool isColliding = false;
    int asteroidCount = 4;
    bool isInvincible;
    float timer;
    public GameObject powerup;
    bool isPowerupActive;
    GameObject activePowerup;

    // Made static so it's easier to access from other classes
    public static List<GameObject> asteroids;

    // Cam fields
    float totalCamWidth;
    float totalCamHeight;

    // New list is to store indices for destroyed items to be removed after destruction
    List<int> indices = new List<int>();

    // AudioSource
    new AudioSource[] audio;

    // Start is called before the first frame update
    void Start()
    {
        asteroids = new List<GameObject>();
        // Cam Dimensions
        Camera camera = Camera.main;
        totalCamHeight = camera.orthographicSize * 2f;
        totalCamWidth = camera.aspect * totalCamHeight;

        // Initializations
        detector = GetComponent<CollisionDetection>();
        player = Instantiate(ship, Vector3.zero, Quaternion.identity);
        score = 0;
        isInvincible = true;
        isPowerupActive = false;
        audio = GetComponents<AudioSource>();
        audio[2].Play();

        // Randomly choose one of the 4 Asteroid prefabs 
        // and then place them randomly around the screen
        for (int i = 0; i < asteroidCount; i++)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    asteroid = asteroid1;
                    break;
                case 1:
                    asteroid = asteroid2;
                    break;
                case 2:
                    asteroid = asteroid3;
                    break;
                case 3:
                    asteroid = asteroid4;
                    break;
            }

            asteroids.Add(Instantiate(asteroid, new Vector3(Random.Range(0, totalCamWidth/2), Random.Range(0, totalCamHeight/2), 0), Quaternion.identity));
        }

        // Give Player a Grace Period Upon Spawning
        GracePeriod(5);
    }

    // Update is called once per frame
    void Update()
    {
        // Bring player to Game Over screen if lives run out
        if (lives <= 0)
        {
            SceneManager.LoadScene(2);
        }

        // Only run collisions if player isn't currently invincible
        if (!isInvincible)
        {
            // Iterate through all asteroids
            for(int i = 0; i < asteroids.Count; i++)
            {
                // If a Collision is detected destroy the asteroid and remove a life
                if (detector.CircleCollision(player, asteroids[i]))
                {
                    Destroy(player);
                    lives--;
                    indices.Add(i);
                    Destroy(asteroids[i]);
                    player = Instantiate(ship, Vector3.zero, Quaternion.identity);
                    audio[0].Play();
                    GracePeriod(5);
                }

                // Go through each index of the destroyed items and remove it from asteroids
                foreach (int index in indices)
                {
                    asteroids.RemoveAt(index);
                }
                indices.Clear();
            }
        }

        // Check Collisions for Powerups
        if (activePowerup != null)
        {
            if (detector.CircleCollision(player, activePowerup))
            {
                Destroy(activePowerup);
                isPowerupActive = false;
                GracePeriod(5);
                audio[1].Play();
            }
        }

        // If there are no asteroids left on screen spawn more but 
        // add another asteroid every wave to increase difficulty
        if (asteroids.Count == 0)
        {
            asteroidCount++;
            for (int i = 0; i < asteroidCount; i++)
            {
                switch (Random.Range(0, 4))
                {
                    case 0:
                        asteroid = asteroid1;
                        break;
                    case 1:
                        asteroid = asteroid2;
                        break;
                    case 2:
                        asteroid = asteroid3;
                        break;
                    case 3:
                        asteroid = asteroid4;
                        break;
                }

                asteroids.Add(Instantiate(asteroid, new Vector3(Random.Range(0, totalCamWidth / 2), Random.Range(0, totalCamHeight / 2), 0), Quaternion.identity));
            }
            // Random Chance to bring in a Power Up
            if(Random.Range(0, 10) < 5 && !isPowerupActive && !isInvincible) // 50% chance, No other power ups on screen, player isn't already protected
            {
                activePowerup = Instantiate(powerup, new Vector3(Random.Range(0, totalCamWidth / 2), Random.Range(0, totalCamHeight / 2), 0), Quaternion.identity);
                isPowerupActive = true;
            }
            GracePeriod(2);
        }

        // Timer for Player Invincibility
        if (isInvincible)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                isInvincible = false;
                player.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            }
        }
    }
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        // GUI Text Style Rules
        style.fontSize = 40;
        style.normal.textColor = Color.white;

        // Label that keep track of the player's score throughout the game
        GUI.Label(new Rect(5, 70, 50, 50), "" + score, style);
        // GUI for keeping track of the amount of lives the player has
        switch (lives)
        {
            case 1:
                GUI.DrawTexture(new Rect(5, 10, 50, 50), texture);
                break;
            case 2:
                GUI.DrawTexture(new Rect(5, 10, 50, 50), texture);
                GUI.DrawTexture(new Rect(65, 10, 50, 50), texture);
                break;
            case 3:
                GUI.DrawTexture(new Rect(5, 10, 50, 50), texture);
                GUI.DrawTexture(new Rect(65, 10, 50, 50), texture);
                GUI.DrawTexture(new Rect(125, 10, 50, 50), texture);
                break;
        }

        if(isInvincible)
        {
            style.normal.textColor = new Color(0, 255, 255);
            GUI.Label(new Rect(Screen.width / 2 - 100, 20, 100, 50), "Forcefields Active", style);
        }
    }

    // This method sets the timer for player invincibility in seconds
    void GracePeriod(float time)
    {
        timer = time;
        isInvincible = true;
        player.GetComponent<SpriteRenderer>().color = new Color(0, 255, 255);
    }

}
