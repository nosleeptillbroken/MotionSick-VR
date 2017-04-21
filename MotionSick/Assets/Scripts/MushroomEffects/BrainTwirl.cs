using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class BrainTwirl : Effect {

    Twirl twirl;

    public override void init()
    {
        time = Time.time;
        twirl = Cam.GetComponent<Twirl>();
    }

    override public void run(float intensity)
    {
        if (On)
        {
            float sinTime = Mathf.Sin(Time.time - time);
            twirl.angle = (360 + Mathf.LerpUnclamped(0, 10, sinTime * Mathf.Clamp01(intensity - 0.25f))) % 360;
        }
    }
}
