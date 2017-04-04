using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable3 : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().SetInteractableObject(this.gameObject);
            Debug.Log("Entered Trigger bounds.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().SetInteractableObject(null);
            Debug.Log("Left trigger bounds");
        }
    }

    void Interact()
    {
        Debug.Log("Interact message for: " + this.gameObject.name);
    }
}
