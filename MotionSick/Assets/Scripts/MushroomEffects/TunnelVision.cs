using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class TunnelVision : Effect {
    VignetteAndChromaticAberration vignette;

    float max = 0.5f;
    float t = 0;
    float previous;

    bool fadingIn;

    public override void turnOn()
    {
        time = Time.time;
        vignette = Cam.GetComponent<VignetteAndChromaticAberration>();
        killTime = 10f;
        fadingIn = true;
    }

    public override void run(float intensity)
    {
        if (On)
        {
            vignette.intensity = (-max * intensity) * Mathf.Cos(toRad(Time.time - time) / 5) + max;
            previous = vignette.intensity;
        }
        else if (turningOff && previous > 0)
        {
            t += (1 / killTime) * Time.deltaTime;
            vignette.intensity = Mathf.Lerp(previous, 0, t);
        }
        else if (turningOff)
        {
            turningOff = false;
            t = 0;
        }

        if (fadingIn && t < 1)
        {
            t += (0.5f / killTime) * Time.deltaTime;
            vignette.intensity = Mathf.Lerp(0, 0.1f, t);
        } 
        else if (fadingIn)
        {
            t = 0;
            fadingIn = false;
            On = true;
        }
    }
}
