using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable1 : MonoBehaviour
{

    private bool animate = false;
    private float scale;

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
            scale += Time.deltaTime*2;
            this.gameObject.transform.localScale = new Vector3(1f + Mathf.Sin(scale),1f + Mathf.Cos(scale),1f);
        }
    }
}
