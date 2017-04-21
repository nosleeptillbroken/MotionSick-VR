using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

	void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.GetComponent<KnockMeDown>())
        {
            if (this.enabled)
            {
                other.gameObject.GetComponent<PlayerAttributes>().SendMessage("OnPlayerHurt", null, SendMessageOptions.DontRequireReceiver);
                this.enabled = false;
            }
        }
    }

}
