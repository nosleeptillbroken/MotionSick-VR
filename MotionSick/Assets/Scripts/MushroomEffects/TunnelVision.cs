using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class TunnelVision : Effect {
    VignetteAndChromaticAberration vignette;

    float max = 0.6f;
    float t = 0;
    float previous;

    public override void init()
    {
        vignette = Cam.GetComponent<VignetteAndChromaticAberration>();
        killTime = 5f;
    }

    public override void run(float intensity)
    {
        if (On)
        {
            vignette.chromaticAberration = max * intensity * Mathf.Sin((Time.time - time) / 3);
            previous = vignette.chromaticAberration;
        }
        else if (turningOff && previous > 0)
        {
            t += (1 / killTime) * Time.deltaTime;
            previous = Mathf.Lerp(previous, 0, t);
        }
        else if (turningOff)
        {
            turningOff = false;
            t = 0;
        }
    }
}
