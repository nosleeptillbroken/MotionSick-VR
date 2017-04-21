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

    public AudioClip openClip;
    public AudioClip lockedClip;
    public AudioClip closeClip;

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
        AudioSource aud = GetComponent<AudioSource>();
        if(aud)aud.Stop();

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
            if (aud) aud.clip = closeClip; aud.Play();
            doorAnim.Play("DoorClose");
            open = !open;
        }
        else if (locked)
        {
            if (aud) aud.clip = lockedClip; aud.Play();
            doorAnim.Play("DoorLocked");
        }
        else
        {
            if (aud) aud.clip = openClip; aud.Play();
            doorAnim.Play("DoorOpen");
            open = !open;
        }

        //Maybe also make a noise.
    }

}