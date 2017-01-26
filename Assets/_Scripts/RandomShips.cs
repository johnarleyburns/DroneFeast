using UnityEngine;
using System.Collections;
using Greyman;

/// <summary>
/// Random planets.
/// Create a random planets that will be in orbit around the NBody that this object is
/// a compnent of. 
///
/// This component must be attached to a GameObject with an NBody component. 
///
/// </summary>
public class RandomShips : MonoBehaviour {

	//! The number of planets to be created when the component starts
	public int numPlanets;

	//! A list of prefabs. Each planet will use a prefab chosen at random from this list.
	public GameObject[] shipPrefabs;

	public float a_min = 1f; 
	public float a_max = 20f;

	public float ecc_min = 0f; 
	public float ecc_max = 0.3f;

	public float incl_min = 0f;
	public float incl_max = 180f;

	public float omega_lc_min = 0f;
	public float omega_lc_max = 360f;

	public float omega_uc_min = 0f;
	public float omega_uc_max = 360f;

	public float phase_min = 0f;
	public float phase_max = 360f;

	public float scale_min = 0.5f;
	public float scale_max = 2f;

    public ScoreController Scorer;
    public TargetTracker Tracker;
    public OffScreenIndicator OffscreenIndicator;
    public int DroneIndicatorId;

	// Use this for initialization
	void Start () {

		if (GetComponent<NBody>() == null) {
				Debug.LogError("Component must be attached to object with an NBody attached.");
				return;
			}
		// Check all the prefabs have NBody and OrbitEllipse components
		foreach (GameObject g in shipPrefabs) {
			if (g.GetComponent<NBody>() == null) {
				Debug.LogError("Prefab " + g.name + " missing component NBody");
				return;
			}
			if (g.GetComponent<OrbitEllipse>() == null) {
				Debug.LogError("Prefab " + g.name + " missing component OrbitEllipse");
				return;
			}
		}

		for (int i=0; i < numPlanets; i++) {
			AddBody(i);
		}
	}

	private void AddBody(int i) {

		// Pick a prefab
		int prefabNum = (int) Random.Range(0, shipPrefabs.Length);

		GameObject ship = Instantiate(shipPrefabs[prefabNum]) as GameObject;
        // make a child of this object
        ship.name = "Drone " + i.ToString();
		ship.transform.parent = gameObject.transform;

		OrbitEllipse oe = ship.GetComponent<OrbitEllipse>();
		oe.centerObject = gameObject;
		// set ranges with appropriate limits
		oe.a = Random.Range(Mathf.Max(a_min, 0.1f), a_max);
		oe.ecc = Random.Range(Mathf.Max(ecc_min, 0f), Mathf.Min(0.99f,ecc_max));
		oe.inclination = Random.Range(Mathf.Max(0,incl_min), Mathf.Min(180f,incl_max));
		oe.omega_lc = Random.Range(Mathf.Max(0, omega_lc_min), Mathf.Min(359.9f, omega_lc_max));
		oe.omega_uc = Random.Range(Mathf.Max(0, omega_uc_min), Mathf.Min(359.9f, omega_uc_max));
		oe.phase = Random.Range(Mathf.Max(0, phase_min), Mathf.Min(359.9f, phase_max));

		// If there is a MeshRenderer - apply scale
		MeshRenderer mr = ship.GetComponentInChildren<MeshRenderer>();
		if (mr != null) {
			mr.transform.localScale = Random.Range(Mathf.Max(scale_min, 0.01f), scale_max) * Vector3.one;
		}

		// If there is an OrbitPredictor, assign the parent
		OrbitPredictor op = ship.GetComponentInChildren<OrbitPredictor>();
		if (op != null) {
			op.body = ship;
			op.centerBody = gameObject;
		}

		oe.Init();

        AddIndicator(ship);
        AddScorer(ship.GetComponentInChildren<ScoreTrigger>());
	}
	
    private void AddIndicator(GameObject g)
    {
        OffscreenIndicator.AddIndicator(g.transform, DroneIndicatorId);
    }

    private void AddScorer(ScoreTrigger t)
    {
        t.Scorer = Scorer;
        t.Indicator = OffscreenIndicator;
        t.TargetTracker = Tracker;
    }

}
