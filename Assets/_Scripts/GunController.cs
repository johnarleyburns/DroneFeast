using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Greyman;

public class GunController : MonoBehaviour {

    public ScoreController Scorer;
    public OffScreenIndicator Indicator;
    public GameObject nBodyBullet;
    public float bulletSpawnDist = 1;
    public float bulletV = 1000;
    public float fireInterval = 0.05f;

	// Use this for initialization
	void Start () {
		
	}

    float timer = 0;

	// Update is called once per frame
	void Update () {
        if (Scorer.IsRunning)
        {
            UpdateGuns();
        }
	}

    private void UpdateGuns()
    {
        if (Input.GetAxisRaw("FireGun") == 1 && timer <= 0)
        {
            timer = fireInterval;
            GameObject b = GameObject.Instantiate(nBodyBullet);
            b.transform.GetChild(0).transform.rotation = transform.rotation;
            Vector3 p = transform.position; // + bulletSpawnDist * transform.forward;
            Vector3 v = GravityEngine.instance.GetVelocity(transform.parent.gameObject) + bulletV * transform.forward;
            GravityEngine.instance.AddBody(b);
            GravityEngine.instance.UpdatePositionAndVelocity(b.GetComponent<NBody>(), p, v);
            b.GetComponentInChildren<CapsuleCollider>().enabled = true;
        }
        timer -= Time.deltaTime;
    }
}
