using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Greyman;

public class ScoreTrigger : MonoBehaviour {

    public ScoreController Scorer;
    public OffScreenIndicator Indicator;
    public TargetTracker TargetTracker;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameObject nBody = other.transform.parent.gameObject;
            Scorer.ScoreHit();
            Indicator.RemoveIndicator(nBody.transform);
            TargetTracker.RemoveTarget(nBody);
            GravityEngine.instance.InactivateBody(nBody);
            nBody.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
