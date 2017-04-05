using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect {

    public GameObject Cam;

    public bool On = false;

    protected float time;
    protected float lastIntensity;
    protected float killTime = 3f;
    protected bool turningOff = false;

    public virtual void turnOn()
    {
        time = Time.time;
        On = true;

        init();
    }

    public virtual void turnOff()
    {
        On = false;
        turningOff = true;
    }

    public virtual void init()
    { }

    public virtual void run(float intensity)
    {
        lastIntensity = intensity;
    }

    public float toRad(float degrees)
    {
        return degrees * (Mathf.PI / 180);
    }
}
