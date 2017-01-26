using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Greyman;

public class ScoreTrigger : MonoBehaviour
{

    public ScoreController Scorer;
    public OffScreenIndicator Indicator;
    public TargetTracker TargetTracker;
    public GameObject ShipExplosion;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //        if (other.gameObject.tag == "Enemy")
        //        {
        Scorer.ScoreHit();
        GameObject nBody = transform.parent.gameObject;
        GameObject p = Instantiate(ShipExplosion);
        p.transform.position = nBody.transform.position;
        p.GetComponent<ParticleSystem>().Play();
        Indicator.RemoveIndicator(nBody.transform);
        TargetTracker.RemoveTarget(nBody);
        GravityEngine.instance.InactivateBody(nBody);
        nBody.SetActive(false);
        Destroy(gameObject);
        //        }
    }
}
