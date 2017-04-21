using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

    private bool running = false;

    private bool dropped = true;

    [SerializeField]
    private Light lightSource;

	// Use this for initialization
	void Start () {
        lightSource.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.parent != null && dropped)
        {
            dropped = false;

            transform.localRotation = Quaternion.identity;
            Vector3 newAngle = transform.localEulerAngles;
            newAngle.x = 90;
            transform.localRotation = Quaternion.Euler(newAngle);

            Vector3 newPosition = transform.localPosition;
            newPosition.x = 0;
            newPosition.y = 0;
            transform.localPosition = newPosition;

            if(!running)
            {
                running = true;
                lightSource.enabled = true;
            }
        }

        if (transform.parent == null && !dropped)
            dropped = true;
	}
}
