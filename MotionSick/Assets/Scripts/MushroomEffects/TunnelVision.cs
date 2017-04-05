﻿using System.Collections;
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
        killTime = 10f;
        loopTime = 20f;
        fadingIn = true;
    }

    public override void run(float intensity)
    {
        if (On)
        {
            vignette.intensity = (max * intensity) * Mathf.Sin(toRad(Time.time - time) / loopTime) + (max * intensity) + 0.15f;
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
        else if (fadingIn && t < 1)
        {
            t += (1f / 2f) * Time.deltaTime;
            vignette.intensity = Mathf.Lerp(0, max * intensity + 0.15f, t);
        } 
        else if (fadingIn)
        {
            t = 0;
            fadingIn = false;
            On = true;
            time = Time.time;
        }
    }
}
