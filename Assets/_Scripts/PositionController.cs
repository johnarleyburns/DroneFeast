using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionController : MonoBehaviour {

    public Text OrbitHeightText;

    private float orbitHeight;

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

    private void SyncOrbitHeight()
    {
        OrbitHeightText.text = string.Format("{0:0} km", orbitHeight);
    }

}
