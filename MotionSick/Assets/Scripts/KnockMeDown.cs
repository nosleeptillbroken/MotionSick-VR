using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class KnockMeDown : MonoBehaviour {
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            transform.parent.SendMessage("OnCollisionEnter", other, SendMessageOptions.DontRequireReceiver);
            rb.isKinematic = false;
        }
    }
}
