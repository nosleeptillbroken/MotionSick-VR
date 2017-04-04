using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable2 : MonoBehaviour
{

    private bool animate = false;
    private float scale = 1f;

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
        animate = !animate;
    }

    void Update()
    {
        if (animate)
        {
            //scale += Time.deltaTime*2;
            gameObject.transform.Rotate(0f,0f, scale);
        }
    }
}
