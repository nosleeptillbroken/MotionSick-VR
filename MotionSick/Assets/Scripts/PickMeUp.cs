using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickMeUp : MonoBehaviour {

    private GameObject player;
    private PlayerController playerController;
    private PlayerAttributes playerAttributes;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
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
        if (playerAttributes.hasItem != null)
        {
            playerAttributes.hasItem.GetComponent<Rigidbody>().isKinematic = false;
            playerAttributes.hasItem.transform.parent = null;
        }

        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        transform.parent = player.transform;
        playerAttributes.hasItem = gameObject;

        Vector3 newPosition = transform.localPosition;
        newPosition.x = 0;
        newPosition.y = 0;

        transform.localPosition = newPosition;
    }
}
