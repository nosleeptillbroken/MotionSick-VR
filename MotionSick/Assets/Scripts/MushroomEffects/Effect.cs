using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect {

    public GameObject Cam;

    public bool On = false;

    protected float time;

    public virtual void turnOn()
    {
        init();
    }

    public virtual void turnOff()
    { }

    public virtual void init()
    {
        time = Time.time;
    }

    public virtual void run(float intensity)
    { }

    public float toRad(float degrees)
    {
        return degrees * (Mathf.PI / 180);
    }
}
