using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class ColorSplitting : Effect {

    VignetteAndChromaticAberration chrome;

    public override void init()
    {
        VignetteAndChromaticAberration temp = Cam.GetComponent<VignetteAndChromaticAberration>();

        if (temp == null)
        {
            Cam.AddComponent<VignetteAndChromaticAberration>();
        }
    }
}
