using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remote : MonoBehaviour
{
    public GameObject tvImage;
    public AudioSource tvSound;
    private bool tripped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && tripped == false)
        {
            toggleTelevision(true);
            tripped = true;
        }
    }

    private void toggleTelevision(bool toggle)
    {
        Debug.Log("ToggleTV");
        tvImage.SetActive(toggle);
        /*
        if (tvSound.isPlaying)
            tvSound.Stop();
        else
            tvSound.Play();
            */
    }

    public void interact()
    {
        toggleTelevision(false);
    }
}
