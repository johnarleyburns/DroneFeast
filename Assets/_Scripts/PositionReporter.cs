using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionReporter : MonoBehaviour {

    public GameObject Planet;
    public float PlanetRadius;
    public PositionController PositionController;
    public GameObject OrbitPredictor;

    private const float INTERVAL = 0.25f;
    private float updateTimer = INTERVAL;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (updateTimer <= 0)
        {
            updateTimer = INTERVAL;
            float h = (transform.position - Planet.transform.position).magnitude - PlanetRadius;
            PositionController.ReportOrbitHeight(h);

            EllipseBase e = OrbitPredictor.GetComponent<EllipseBase>();
            if (e != null)
            {
                OrbitData o = e.GetOrbitData();
                PositionController.ReportOrbitData(o);
            }
        }
        else
        {
            updateTimer -= Time.deltaTime;
        }
    }
}
