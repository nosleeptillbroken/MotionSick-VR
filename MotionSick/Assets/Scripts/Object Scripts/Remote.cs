using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remote : MonoBehaviour
{
    public Animator tvAnim;
    public AudioSource tvSound;
    private bool tripped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && tripped == false)
        {
            tripped = true;
            toggleTelevision(true);
        }
    }

    private void toggleTelevision(bool toggle)
    {
        Debug.Log("ToggleTV");
        tvAnim.SetBool("On", tripped);
        /*
        if (tvSound.isPlaying)
            tvSound.Stop();
        else
            tvSound.Play();
            */
    }

    public void Interact()
    {
        toggleTelevision(false);
    }
}
