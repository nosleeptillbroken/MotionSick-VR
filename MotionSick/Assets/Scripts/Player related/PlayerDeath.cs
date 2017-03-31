using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{

    //This Script needs to be attached to a Player to work
    private void Start()
    {
        Scene menuScene = SceneManager.GetActiveScene();
        Scene currentScene = SceneManager.GetActiveScene();
    }

    //Whatever you want as your Game Over Menu
    public GameObject gameOverMenu;
    // Menu Scene, currentyly set to be the active scene
    //public string menuScene = "Rob";
    
    // Menu Scene, currentyly set to be the active scene
    //public string currentScene = "Rob";
    
    // Call this when health equals 0
    // It disables the PlayerController
    // It also spawns a Game Over screen
    public void OnDeath()
    {
        // Disable Player Control
        GetComponent<PlayerController>().enabled = false;
        // Create the Game Over menu
        Instantiate(gameOverMenu);
    }

    // Replay Menu Button
    //There are some issues with reloading the scene and Rewired
    public void Replay()
    {
        // This will be the name of whichever scene we are playing in
        //SceneManager.LoadScene(currentScene.name);
        Debug.Log("Reloaded");
    }

    //Return To Menu Button
    // Assuming main menu is in a different scene.
    public void ReturnToMenu()
    {
        // This will be the name of the scene where we keep the menu
        //SceneManager.LoadScene(menuScene.name);
        Debug.Log("Main Menu Loaded");
    }

    //Quit Button
    public void QuitButton()
    {
        Application.Quit();
    }
}
