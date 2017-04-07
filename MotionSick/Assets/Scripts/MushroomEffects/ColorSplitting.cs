using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class ColorSplitting : Effect {

    VignetteAndChromaticAberration chrome;

    float max = 40;
    float t = 0;
    float previous;

    public override void init()
    {
        chrome = Cam.GetComponent<VignetteAndChromaticAberration>();
        killTime = 5f;
        loopTime = 6f;
    }

    public override void turnOff()
    {
        On = false;
        turningOff = true;
        t = Time.time + killTime;
    }

    public override void run(float intensity)
    {
        if (On)
        {
            chrome.chromaticAberration = max * intensity * Mathf.Sin(toRad(Time.time - time)/loopTime);
            previous = chrome.chromaticAberration;
        } 
        else if (turningOff && (Time.time < t))
        {
            chrome.chromaticAberration = Mathf.Lerp(previous, 0, Time.time / t);
        }
        else if (turningOff)
        {
            turningOff = false;
            chrome.chromaticAberration = 0;
        }
    }
}
