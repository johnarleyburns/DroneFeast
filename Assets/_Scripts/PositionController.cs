using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionController : MonoBehaviour {

    public Text OrbitHeightText;
    public Text OrbitPhaseText;

    private float orbitHeight;
    private OrbitData orbitData;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReportOrbitHeight(float h)
    {
        orbitHeight = h;
        SyncOrbitHeight();
    }

    public void ReportOrbitData(OrbitData o)
    {
        orbitData = o;
        SyncOrbitData();
    }

    private void SyncOrbitHeight()
    {
        OrbitHeightText.text = string.Format("{0:0} km", orbitHeight);
    }

    private void SyncOrbitData()
    {
        if (orbitData != null)
        {
            OrbitPhaseText.text = string.Format("{0:0} deg", orbitData.phase);
        }
    }

}
