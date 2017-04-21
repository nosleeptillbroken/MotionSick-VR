using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool locked = false;
    public GameObject needs = null;

    private PlayerController playerController;
    private PlayerAttributes playerAttributes;
    private Animation doorAnim;
    private bool open = false;

    // Use this for initialization
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerAttributes = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttributes>();
        doorAnim = gameObject.GetComponent<Animation>();

        if (needs == null)
            locked = false;
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

        if (locked && needs != null &&
            playerAttributes.hasItem == needs)
            locked = false;

        if (open)
            doorAnim.Play("DoorClose");
        else if (locked)
            doorAnim.Play("DoorLocked");
        else
            doorAnim.Play("DoorOpen");

        //Maybe also make a noise.
    }

}