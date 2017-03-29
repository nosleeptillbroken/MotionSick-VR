using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewTrigger : MonoBehaviour {

    public Camera viewer;

    public ulong framesVisible { get; private set; }

    void Awake() {
        framesVisible = 0;
        if (viewer == null) viewer = Camera.main;
    }
	
	void Update () {
        if(isVisibleFrom(viewer))
        {
            if(framesVisible == 0)
            {
                gameObject.SendMessageUpwards("onViewEnter", viewer, SendMessageOptions.DontRequireReceiver);
                framesVisible = 1;
            }
            else
            {
                gameObject.SendMessageUpwards("onViewStay", viewer, SendMessageOptions.DontRequireReceiver);
                framesVisible += 1;
            }
        }	
        else
        {
            if(framesVisible != 0)
            {
                gameObject.SendMessageUpwards("onViewExit", viewer, SendMessageOptions.DontRequireReceiver);
                framesVisible = 0;
            }
        }	
	}

    void onViewEnter(Camera camera)
    {
        Debug.Log("onViewEnter due to " + camera.gameObject.name);
    }

    void onViewStay(Camera camera)
    {
        Debug.Log("onViewStay due to " + camera.gameObject.name);
    }

    void onViewExit(Camera camera)
    {
        Debug.Log("onViewExit due to " + camera.gameObject.name);
    }

    bool isVisibleFrom(Camera camera)
    {
        if (camera == null) return false;
        Vector3 screenPoint = camera.WorldToViewportPoint(this.transform.position);
        bool x = screenPoint.x > 0 && screenPoint.x < 1;
        bool y = screenPoint.y > 0 && screenPoint.y < 1;
        bool z = screenPoint.z > camera.nearClipPlane && screenPoint.z < camera.farClipPlane;
        return x && y && z;
    }
}
