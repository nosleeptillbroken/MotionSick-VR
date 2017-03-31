using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewTrigger : MonoBehaviour {

    public Camera viewer;

    private ulong framesVisible = 0;
    private float timeVisible = 0.0f;

    void Awake()
    {
        if (viewer == null) viewer = Camera.main;
    }
	
	void Update ()
    {
        VisibleEvent e = new VisibleEvent();
        e.camera = viewer;
        e.time = timeVisible;
        e.frames = framesVisible;

        if (isVisibleFrom(viewer))
        {
            if(framesVisible == 0)
            {
                gameObject.SendMessageUpwards("OnViewEnter", e, SendMessageOptions.DontRequireReceiver);
                framesVisible = 1;
                timeVisible = Time.deltaTime;
            }
            else
            {
                gameObject.SendMessageUpwards("OnViewStay", e, SendMessageOptions.DontRequireReceiver);
                framesVisible += 1;
                timeVisible += Time.deltaTime;
            }
        }	
        else
        {
            if(framesVisible != 0)
            {
                gameObject.SendMessageUpwards("OnViewExit", e, SendMessageOptions.DontRequireReceiver);
                framesVisible = 0;
                timeVisible = 0.0f;
            }
        }	
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
