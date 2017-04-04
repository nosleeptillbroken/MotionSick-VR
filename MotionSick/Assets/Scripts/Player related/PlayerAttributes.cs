using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{

    // How many hits you can take before dying
    private int playerHealth;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Decrement Health Variable
        playerHealth--;
        // Just for testing
        Debug.Log("boop");
        // If the Player is Dead
        if (playerHealth <= 0)
        {
            // If the collision is with an enemy
            if (other.tag == "enemy")
            {
                // Begin the Process of killing the player
                GetComponent<PlayerDeath>().OnDeath();
            }
        }
    }

    #region Getters

    public int GetHealth()
    {
        return playerHealth;
    }
    #endregion

    #region Setters

    public void SetHealth(int val)
    {
        playerHealth = val;
    }
    #endregion

}