using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionReporter : MonoBehaviour {

    public GameObject Planet;
    public float PlanetRadius;
    public PositionController PositionController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h = (transform.position - Planet.transform.position).magnitude - PlanetRadius;
        PositionController.ReportOrbitHeight(h);	
	}
}
