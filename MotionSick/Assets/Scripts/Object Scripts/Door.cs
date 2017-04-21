using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool locked = true;
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
        if (locked && needs != null &&
            playerAttributes.hasItem == needs)
        { 
            locked = false;
            playerAttributes.hasItem.GetComponent<Rigidbody>().isKinematic = false;
            playerAttributes.hasItem.transform.parent = null;
            playerAttributes.hasItem = null;
        }

        if (open)
        {
            doorAnim.Play("DoorClose");
            open = !open;
        }
        else if (locked)
            doorAnim.Play("DoorLocked");
        else
        {
            doorAnim.Play("DoorOpen");
            open = !open;
        }

        //Maybe also make a noise.
    }

}