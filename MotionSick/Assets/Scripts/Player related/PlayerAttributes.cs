using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{

    // How many hits you can take before dying
    [SerializeField] private int playerHealth;
    public GameObject hasItem;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        

        // If the Collision is with the end Goal
        if(other.tag == "winCondition")
        {
            // Begin the Process of victory
            GetComponent<PlayerVictory>().OnWin();
        }
    }

    private void OnPlayerHurt()
    {
        // Decrement Health Variable
        playerHealth--;

        // Just for testing
        Debug.Log("boop");

        // If the Player is Dead
        if (playerHealth <= 0)
        {
            // Begin the Process of killing the player
            GetComponent<PlayerDeath>().OnDeath();
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