using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class FishBubble : Effect {
    Fisheye fisheye;

    float max = 0.15f;
    float t = 0;
    float previous;

    bool fadingIn;

    public override void turnOn()
    {
        fisheye = Cam.GetComponent<Fisheye>();
        killTime = 5f;
        fadingIn = true;
    }

    public override void run(float intensity)
    {
        if (On)
        {
          //  fisheye.intensity = (max * intensity) * Mathf.Sin(toRad(Time.time - time) / loopTime) + (max * intensity) + 0.15f;
            //previous = fisheye.intensity;
        }
        else if (turningOff && previous > 0)
        {
            t += (1 / killTime) * Time.deltaTime;
            //fisheye.intensity = Mathf.Lerp(previous, 0, t);
        }
        else if (turningOff)
        {
            turningOff = false;
            t = 0;
        }
    }
}
