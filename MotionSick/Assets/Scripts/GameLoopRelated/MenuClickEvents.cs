using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuClickEvents : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        // Scene menuScene = SceneManager.GetActiveScene();
        // Scene currentScene = SceneManager.GetActiveScene();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //
    // Main Menu Functions
    //
    public void Play()
    {
        // Loads the main Gameplay Scene
        SceneManager.LoadScene(1);
    }

    //
    // Win Screen Functions
    //

    //
    // Game Over Menu Functions
    //

    public void Replay()
    {
        // This will be the name of whichever scene we are playing in
        SceneManager.LoadScene(1);
        // Debug.Log("Reloaded");
    }

    // Return To Menu Button
    // Assuming main menu is in a different scene.
    public void ReturnToMenu()
    {
        // This will be the name of the scene where we keep the menu
        SceneManager.LoadScene(0);
        // Debug.Log("Main Menu Loaded");
    }

    //Quit Button
    public void QuitButton()
    {
        Application.Quit();
    }
}
