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
        RecordHit();
    }

    public void RecordHit()
    {
        Scorer.ScoreHit();

        GameObject nBody = transform.parent.gameObject;
        GameObject p = Instantiate(ShipExplosion, nBody.transform.position, nBody.transform.rotation, null);
        Vector3 v = GravityEngine.instance.GetVelocity(nBody);
        GravityEngine.instance.AddBody(p);
        GravityEngine.instance.UpdatePositionAndVelocity(p.GetComponent<NBody>(), nBody.transform.position, v);

        Indicator.RemoveIndicator(nBody.transform);
        TargetTracker.RemoveTarget(nBody);
        GravityEngine.instance.InactivateBody(nBody);
        nBody.SetActive(false);
        Destroy(gameObject);

    }

}
