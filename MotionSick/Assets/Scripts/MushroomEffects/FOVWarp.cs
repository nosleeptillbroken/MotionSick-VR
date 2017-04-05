using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVWarp : Effect {

    Camera cam;

    float max = 10;
    float originalFOV;

    float endingFOV;
    float t = 0;

    public override void init()
    {
        time = Time.time;
        cam = Cam.GetComponent<Camera>();
        originalFOV = cam.fieldOfView;

        Debug.Log(originalFOV);
    }

    public override void turnOff()
    {
        On = false;
        turningOff = true;
        endingFOV = cam.fieldOfView;
    }

    public override void run(float intensity)
    {
        if (On)
        {
            cam.fieldOfView = originalFOV + max * intensity * Mathf.Sin(Time.time - time);
            lastIntensity = intensity;
        }
        else if (turningOff && t < 1)
        {
            t += (1 / killTime) * Time.deltaTime;
            cam.fieldOfView = Mathf.Lerp(endingFOV, originalFOV, t);
        }
        else if (turningOff)
        {
            turningOff = false;
            t = 0;
        }
    }
}
