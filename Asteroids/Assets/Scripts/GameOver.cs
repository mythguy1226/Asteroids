using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Background field
    public Texture2D background;

    // AudioSource
    new AudioSource[] audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponents<AudioSource>();
        audio[0].Play();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnGUI()
    {
        // Declare Text Style and Button Style
        GUIStyle style = new GUIStyle();
        GUIStyle boxSkin = new GUIStyle("button");

        // Set Style Rules
        style.fontSize = 60;
        style.normal.textColor = Color.red;

        boxSkin.normal.textColor = new Color(0, 255, 255);
        boxSkin.normal.background = background;
        boxSkin.fontSize = 40;

        // Place the Header and the Buttons
        GUI.Label(new Rect(Screen.width / 2 - 125, 20, 200, 150), "Game Over", style);

        if (GUI.Button(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 100, 500, 150), "Play Again", boxSkin))
        {
            SceneManager.LoadScene(1);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 250, Screen.height / 2 + 100, 500, 150), "Leave to Home Screen", boxSkin))
        {
            SceneManager.LoadScene(0);
        }
    }
}
