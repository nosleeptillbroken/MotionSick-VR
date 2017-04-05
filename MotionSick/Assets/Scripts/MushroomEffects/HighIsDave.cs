using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighIsDave : MonoBehaviour {

    private List<Effect> Effects;

    public float intensity = 0f;

    [SerializeField]
    private GameObject camObject;

    void Start()
    {
        Effects = new List<Effect>();
        Effects.Add(new FOVWarp());
        Effects.Add(new ColorSplitting());

        Debug.Log(Effects);

        foreach(Effect effect in Effects)
        {
            effect.Cam = camObject;
            effect.init();
            effect.On = true;
            Debug.Log(effect.On);
        }

        StartCoroutine(randomOff());
    }
	
	// Update is called once per frame
	void Update () {
		foreach(Effect effect in Effects)
        {
            effect.run(intensity);
        }
	}

    IEnumerator randomOff()
    {
        yield return new WaitForSeconds(5);
        Effects[0].turnOff();
    }
}
