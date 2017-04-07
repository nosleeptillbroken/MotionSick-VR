using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVictory : MonoBehaviour {

    public GameObject winMenu;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Call this when Player reached goal
    // It disables the PlayerController
    // It also spawns a Victory screen
    public void OnWin()
    {
        // Disable Player Control
        GetComponent<PlayerController>().enabled = false;
        // Create the Game Over menu
        Instantiate(winMenu);
        Cursor.visible = true;
    }
}
