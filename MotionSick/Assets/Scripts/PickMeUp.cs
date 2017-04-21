using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickMeUp : MonoBehaviour {

    private Transform originalParent;
    private GameObject player;
    private PlayerController playerController;
    private PlayerAttributes playerAttributes;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        originalParent = transform.parent;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerAttributes = player.GetComponent<PlayerAttributes>();
        rb = GetComponent<Rigidbody>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerController.SetInteractableObject(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerController.SetInteractableObject(null);
    }

    public void Interact()
    {
        if(playerAttributes.hasItem == gameObject)
        {
            Drop();
        }
        else if (playerAttributes.hasItem != null)
        {
            playerAttributes.hasItem.GetComponent<PickMeUp>().Drop();
            PickUp();
        }
        else
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        rb.isKinematic = true;
        transform.parent = player.transform;
        playerAttributes.hasItem = gameObject;
    }

    public void Drop()
    {
        rb.isKinematic = false;
        transform.parent = originalParent;
        playerAttributes.hasItem = null;
    }
}
