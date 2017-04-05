using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVWarp : Effect {

    private Camera cam;

    float max = 15;
    float originalFOV;

    public override void init()
    {
        time = Time.time;
        cam = Cam.GetComponent<Camera>();
        originalFOV = cam.fieldOfView;
    }

    public override void run(float intensity)
    {
        cam.fieldOfView =originalFOV +  max * intensity * Mathf.Sin(Time.time - time);
    }
}
