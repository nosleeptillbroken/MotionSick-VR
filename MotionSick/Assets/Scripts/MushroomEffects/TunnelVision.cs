using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class TunnelVision : Effect {
    VignetteAndChromaticAberration vignette;

    float max = 0.15f;
    float t = 0;
    float previous;

    bool fadingIn;

    public override void turnOn()
    {
        vignette = Cam.GetComponent<VignetteAndChromaticAberration>();
        killTime = 5f;
        loopTime = 20f;
        fadingIn = true;
        On = true;
        t = Time.time + 2;
    }

    public override void turnOff()
    {
        On = false;
        turningOff = true;
        t = Time.time + killTime;
    }

    public override void run(float intensity)
    {
        if (fadingIn && Time.time < t)
        {
            vignette.intensity = Mathf.Lerp(0, 0.15f, Time.time / t);
        }
        else if (fadingIn)
        {
            fadingIn = false;
            time = Time.time;
        }
        else if (On)
        {
            vignette.intensity = (-max * intensity) * Mathf.Cos(toRad(Time.time - time) / loopTime) + (max * intensity) + 0.15f;
            previous = vignette.intensity;
        }
        else if (turningOff && Time.time < t)
        {
            //vignette.intensity = Mathf.Lerp(previous, 0, Time.time / t);
            vignette.intensity = previous * (1 - (Time.time / t));
        }
        else if (turningOff)
        {
            turningOff = false;
        }
    }
}
