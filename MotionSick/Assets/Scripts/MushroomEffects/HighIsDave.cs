using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighIsDave : MonoBehaviour {

    private List<Effect> Effects;

    public float intensity = 0f;

    [SerializeField]
    private float startDelay = 5f;
    [SerializeField]
    private float newEffectTime = 10f;
    [SerializeField]
    private float timeUntilPeak = 180f;

    private float minIntensity = 0.2f;
    private float startTime;
    private float peakTime;

    [SerializeField]
    private GameObject camObject;

    void Start()
    {
        startTime = Time.time;
        peakTime = startTime + timeUntilPeak;

        Effects = new List<Effect>();
        Effects.Add(new FOVWarp());
        Effects.Add(new ColorSplitting());
        Effects.Add(new TunnelVision());
        Effects.Add(new FishBubble());

        foreach (Effect effect in Effects)
        {
            effect.Cam = camObject;
        }

        StartCoroutine(TurnOnRandomEffects());
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time < peakTime)
        {
            intensity = Time.time / peakTime;
        }

		foreach(Effect effect in Effects)
        {
            effect.run(intensity);
        }
	}

    IEnumerator TurnOnRandomEffects()
    {
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            int i = Random.Range(0, Effects.Count - 1);
            if (Effects[i].On)
            {
                Debug.Log("Effect " + i + " is already on, so we're turning it off.");
                Effects[i].turnOff();
                int j = i;
                while (Effects[++i % (Effects.Count)].On)
                { Debug.Log("Effect " + i + " is also on.."); }

                i %= (Effects.Count);
                if (i != j)
                {
                    Debug.Log("So we're turning on effect " + i + " instead.");
                    Effects[i].turnOn();
                }
                else
                {
                    Debug.Log("So we're just going to leave it off.");
                }
            }
            else
            {
                Debug.Log("Turning on effect " + i);
                Effects[i].turnOn();
            }
            yield return new WaitForSeconds(newEffectTime);
        }
    }
}
