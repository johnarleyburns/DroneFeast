using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustController : MonoBehaviour {

    public ParticleSystem thrustPS;
    public AudioSource thrustSound;

    private float thrustFactor = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float t = Input.GetAxisRaw("Thrust");
        if (t > 0)
        {
            GameObject nBody = transform.parent.gameObject;
            Vector3 v = GravityEngine.instance.GetVelocity(nBody);
            v += thrustFactor * transform.forward * Time.deltaTime;
            GravityEngine.instance.UpdatePositionAndVelocity(nBody.GetComponent<NBody>(), nBody.transform.position, v);
            if (!thrustPS.isPlaying)
            {
                thrustPS.Play();
            }
            if (!thrustSound.isPlaying)
            {
                thrustSound.Play();
            }
        }
        else
        {
            if (thrustPS.isPlaying)
            {
                thrustPS.Stop();
            }
            if (thrustSound.isPlaying)
            {
                thrustSound.Pause();
            }
        }

	}
}
