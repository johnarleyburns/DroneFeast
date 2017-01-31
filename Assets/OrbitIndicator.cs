using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitIndicator : MonoBehaviour {

    public GameObject RefNBody;
    public GameObject ShipNBody;
    public GameObject ProgradeIndicator;
    public GameObject RetrogradeIndicator;
    public GameObject NormalPlusIndicator;
    public GameObject NormalMinusIndicator;

    private float indicatorRadius;

    // Use this for initialization
    void Start () {
        indicatorRadius = ProgradeIndicator.transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update () {
        Vector3 refV = GravityEngine.instance.GetVelocity(RefNBody);
        Vector3 shipV = GravityEngine.instance.GetVelocity(ShipNBody);
        Vector3 relV = shipV - refV;
        Vector3 relVBearing = relV.normalized;

        Vector3 relVPos = indicatorRadius * relVBearing;
        ProgradeIndicator.transform.position = ShipNBody.transform.position + relVPos;
        RetrogradeIndicator.transform.position = ShipNBody.transform.position - relVPos;

        Vector3 refBearing = (RefNBody.transform.position - ShipNBody.transform.position).normalized;
        Vector3 normalMinus = indicatorRadius * Vector3.Cross(relVBearing, refBearing); // unity is left handed
        NormalMinusIndicator.transform.position = ShipNBody.transform.position + normalMinus;
        NormalPlusIndicator.transform.position = ShipNBody.transform.position - normalMinus;
    }

}
