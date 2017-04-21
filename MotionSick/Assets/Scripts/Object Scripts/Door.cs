using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private PlayerController playerController;
    private Animation doorAnim;
    private bool open = false;

    // Use this for initialization
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        doorAnim = gameObject.GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerController.SetInteractableObject(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        playerController.SetInteractableObject(null);
    }

    private void Interact()
    {
        open = !open;

        if (open)
            doorAnim.Play();
        else
            doorAnim.Rewind();

        //Maybe also make a noise.
    }

}