using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    private bool activated = false;
    public AudioSource noise;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag("Player") && activated == false)
        {
            activated = true;
            //noise.Play();
            Debug.Log("RRRRRRRRRMMMMMMMM");
        }
    }

    public void interact()
    {
        //noise.Stop();
        Debug.Log("shut that racket off");
    }
}
