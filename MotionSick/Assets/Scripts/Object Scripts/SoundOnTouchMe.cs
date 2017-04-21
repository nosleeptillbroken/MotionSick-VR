using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnTouchMe : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.GetComponent<KnockMeDown>())
        {
            if (!GetComponent<AudioSource>().isPlaying && this.enabled)
            {
                GetComponent<AudioSource>().Play();
                this.enabled = false;
            }
        }
    }

}
