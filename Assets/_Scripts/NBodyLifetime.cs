using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBodyLifetime : MonoBehaviour {

    public float LifetimeSec = 5;

    private float lifetime;

	// Use this for initialization
	void Start () {
        lifetime = LifetimeSec;	
	}
	
	// Update is called once per frame
	void Update () {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            GravityEngine.instance.InactivateBody(transform.parent.gameObject);
            transform.parent.gameObject.SetActive(false);
            Destroy(gameObject);
        }
	}
}
