using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private PlayerController playerController;
    private Animation doorAnim;
    private bool open = false;

    public AudioClip openClip;
    public AudioClip closeClip;

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
        AudioSource aud = GetComponent<AudioSource>();
        if(aud)aud.Stop();

        open = !open;

        if (open)
        {
            if (aud) aud.clip = openClip; aud.Play();
            doorAnim.Play();
        }
        else
        {
            if (aud) aud.clip = closeClip; aud.Play();
            doorAnim.Rewind();
        }

        //Maybe also make a noise.
    }

}