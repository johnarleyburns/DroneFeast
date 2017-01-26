using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathTrigger : MonoBehaviour {

    public Camera PlayerCamera;
    public ScoreController Scorer;
    public GameObject Explosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        PlayerCamera.transform.parent = null;
        GameObject nBody = transform.parent.gameObject;

        GameObject e = Instantiate(Explosion);
        e.transform.position = nBody.transform.position;
        e.GetComponent<ParticleSystem>().Play();

        Scorer.PlayerDeath(other.gameObject);

        GravityEngine.instance.InactivateBody(nBody);
        transform.GetChild(0).gameObject.SetActive(false);
    }


}
