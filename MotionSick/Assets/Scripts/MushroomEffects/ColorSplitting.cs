using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class ColorSplitting : Effect {

    VignetteAndChromaticAberration chrome;

    float max = 50;
    float t = 0;
    float previousChromAb;

    public override void init()
    {
        chrome = Cam.GetComponent<VignetteAndChromaticAberration>();
        killTime = 5f;
    }

    public override void run(float intensity)
    {
        if (On)
        {
            chrome.chromaticAberration = max * intensity * Mathf.Sin((Time.time - time)/3);
            previousChromAb = chrome.chromaticAberration;
        } 
        else if (turningOff && previousChromAb > 0)
        {
            t += (1 / killTime) * Time.deltaTime;
            chrome.chromaticAberration = Mathf.Lerp(previousChromAb, 0, t);
        }
        else if (turningOff)
        {
            turningOff = false;
            t = 0;
        }
    }
}
