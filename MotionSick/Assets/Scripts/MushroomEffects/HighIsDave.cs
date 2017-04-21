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

    void Awake()
    {
        GetComponent<PlayerController>().enabled = true;
    }

    void Start()
    {
        startTime = Time.time;
        peakTime = startTime + timeUntilPeak;

        Effects = new List<Effect>();
        Effects.Add(new ColorSplitting());
        Effects.Add(new FOVWarp());
        Effects.Add(new TunnelVision());
        Effects.Add(new FishBubble());

        foreach (Effect effect in Effects)
        {
            effect.Cam = camObject;
        }

        Effects[0].turnOn();

        StartCoroutine(TurnOnRandomEffects());
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time < peakTime)
        {
            intensity = (Time.time - startTime) / peakTime;
        }

		foreach(Effect effect in Effects)
        {
            effect.run(intensity);
        }

        AudioSource[] auds = GetComponents<AudioSource>();

        GetComponent<AudioReverbZone>().room = (int)Mathf.Lerp(-6000, -1000, intensity);
        auds[0].volume = Mathf.Lerp(0.01f, 0.5f, intensity);
        auds[0].panStereo = Mathf.Sin(Time.time - startTime) * intensity/2;
        auds[1].volume = Mathf.Lerp(-0.5f, 0.5f, intensity);

        UnityStandardAssets.ImageEffects.Twirl twirl = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.Twirl>();
        if(twirl)
        {
            float sinTime = Mathf.Sin(Time.time - startTime);
            twirl.angle = (360 + Mathf.LerpUnclamped(0, 10, sinTime * Mathf.Clamp01(intensity-0.25f))) % 360;
        }
	}

    IEnumerator TurnOnRandomEffects()
    {
        yield return new WaitForSeconds(startDelay);

        int totalEffectsOn = 1;

        startTime = Time.time;
        peakTime = startTime + timeUntilPeak;

        while (totalEffectsOn < Effects.Count)
        {
            int i = Random.Range(0, Effects.Count - 1);
            if (!Effects[i].On)
            {
                int j = i;
                while (Effects[++i % (Effects.Count)].On)
                {
                    Debug.Log("Effect " + i + " is also on..");
                }

                i %= (Effects.Count);
                if (i != j)
                {
                    Debug.Log("So we're turning on effect " + i + " instead.");
                    Effects[i].turnOn();
                    totalEffectsOn++; 
                }
            }
            else
            {
                Debug.Log("Turning on effect " + i);
                Effects[i].turnOn();
                totalEffectsOn++;
            }
            yield return new WaitForSeconds(newEffectTime);
        }
    }
}
