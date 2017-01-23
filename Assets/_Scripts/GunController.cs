using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public ScoreController scorer;
    public GameObject nBodyBullet;
    public float bulletSpawnDist = 3;

    private float bulletV = 1000;
    private float fireInterval = 0.05f;

	// Use this for initialization
	void Start () {
		
	}

    float timer = 0;

	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw("FireGun") == 1 && timer <= 0)
        {
            timer = fireInterval;
            GameObject b = GameObject.Instantiate(nBodyBullet);
            Vector3 p = transform.position + bulletSpawnDist * transform.forward;
            Vector3 v = GravityEngine.instance.GetVelocity(transform.parent.gameObject) + bulletV * transform.forward;
            GravityEngine.instance.AddBody(b);
            GravityEngine.instance.UpdatePositionAndVelocity(b.GetComponent<NBody>(), p, v);
            b.GetComponentInChildren<ScoreTrigger>().scorer = scorer;
            b.GetComponentInChildren<CapsuleCollider>().enabled = true;
        }
        timer -= Time.deltaTime;
	}
}
