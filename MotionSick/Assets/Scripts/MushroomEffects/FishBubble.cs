using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class FishBubble : Effect {
    Fisheye fisheye;

    float max = 0.3f;
    float t = 0;
    float previous;

    public override void init()
    {
        fisheye = Cam.GetComponent<Fisheye>();
        killTime = 5f;
    }

    public override void run(float intensity)
    {
        if (On)
        {
            fisheye.strengthX = (-max * intensity) * Mathf.Cos(toRad(Time.time - time) / loopTime) + (max * intensity);
            fisheye.strengthY = (-max * intensity) * Mathf.Cos(toRad(Time.time - time) / loopTime) + (max * intensity);
        }
        else if (turningOff && previous > 0)
        {
            t += (1 / killTime) * Time.deltaTime;
            fisheye.strengthX = Mathf.Lerp(previous, 0, t);
            fisheye.strengthY = Mathf.Lerp(previous, 0, t);
        }
        else if (turningOff)
        {
            turningOff = false;
            t = 0;
        }
    }
}
