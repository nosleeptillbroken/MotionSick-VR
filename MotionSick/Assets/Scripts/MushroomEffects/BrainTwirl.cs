using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainTwirl : Effect {

    UnityStandardAssets.ImageEffects.Twirl twirl;

    public override void init()
    {
        base.init();
        twirl = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.Twirl>();
    }

    override public void run(float intensity)
    {
        float sinTime = Mathf.Sin(Time.time - time);
        twirl.angle = (360 + Mathf.LerpUnclamped(0, 10, sinTime * Mathf.Clamp01(intensity - 0.25f))) % 360;
    }
}
