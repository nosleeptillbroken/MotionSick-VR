using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remote : MonoBehaviour
{
    public Animator tvAnim;
    public AudioSource tvSound;
    private bool tripped = false;
    private PlayerController playerController;
    private PlayerAttributes playerAttributes;

    private bool overlapping = false;

    private void Start()
    {
        playerAttributes = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttributes>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            overlapping = true;
            playerController.SetInteractableObject(gameObject);
        }

        if (other.gameObject.CompareTag("Player") && tripped == false)
        {
            tripped = true;
            toggleTelevision(true);
            playerAttributes.SetHealth(playerAttributes.GetHealth() - 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            overlapping = false;
            playerController.SetInteractableObject(null);
        }
    }

    private void toggleTelevision(bool toggle)
    {
        //Debug.Log("ToggleTV");
        tvAnim.SetBool("On", toggle);
        /*
        if (tvSound.isPlaying)
            tvSound.Stop();
        else
            tvSound.Play();
            */
    }

    public void Interact()
    {
        if (overlapping)
            toggleTelevision(false);
    }
}
