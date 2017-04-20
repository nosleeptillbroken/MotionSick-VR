using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remote : MonoBehaviour
{
    public Animator tvAnim;
    public AudioSource tvSound;
    private bool tripped = false;

    private bool overlapping = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            overlapping = true;

        if (other.gameObject.CompareTag("Player") && tripped == false)
        {
            tripped = true;
            toggleTelevision(true);
            other.gameObject.GetComponent<PlayerController>().SetInteractableObject(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            overlapping = false;
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
