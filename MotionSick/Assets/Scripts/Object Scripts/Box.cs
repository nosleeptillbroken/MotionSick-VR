using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private bool dropped = false;
    public AudioSource sound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag("Floor") && dropped == false)
        {
            //sound.Play();
            dropped = true;
            Debug.Log("crash");
        }
    }
}
