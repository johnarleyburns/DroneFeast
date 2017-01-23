using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathTrigger : MonoBehaviour {

    public ScoreController scorer;
    public ParticleSystem Explosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Camera.current.transform.parent = null;
        scorer.PlayerDeath();
        GravityEngine.instance.InactivateBody(transform.parent.gameObject);
        Explosion.Play();
        transform.GetChild(0).gameObject.SetActive(false);
        //Destroy(gameObject);
    }


}
