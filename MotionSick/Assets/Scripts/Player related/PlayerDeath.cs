using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    //This Script needs to be attached to a Player to work
    private void Start()
    {
    }

    //Whatever you want as your Game Over Menu
    public GameObject gameOverMenu;
    
    // Call this when health equals 0
    // It disables the PlayerController
    // It also spawns a Game Over screen
    public void OnDeath()
    {
        // Disable Player Control
        GetComponent<PlayerController>().enabled = false;
        // Create the Game Over menu
        Instantiate(gameOverMenu);
        Cursor.visible = true;
    }

    // Replay Menu Button
    //There are some issues with reloading the scene and Rewired
    
}
