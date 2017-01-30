using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareFader : MonoBehaviour {

    public float LifetimeSec = 3;

    private float startTime;
    private LensFlare flare;
    private float initialBrightness;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        flare = GetComponent<LensFlare>();
        initialBrightness = flare.brightness;
	}
	
	// Update is called once per frame
	void Update () {
        float t = (Time.time - startTime) / LifetimeSec;
        if (t <= 0)
        {
            Destroy(this);
        }
        else
        {
            float brightness = Mathf.SmoothStep(initialBrightness, 0, t);
            flare.brightness = brightness;
        }
    }

}
