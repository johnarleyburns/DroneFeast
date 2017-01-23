using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour {

    public ScoreController scorer;

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
            scorer.ScoreHit();
            GravityEngine.instance.InactivateBody(other.transform.parent.gameObject);
            Destroy(other.gameObject);
        }
    }
}
