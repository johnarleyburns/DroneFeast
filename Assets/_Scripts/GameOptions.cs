using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOptions : MonoBehaviour {

    private bool invertedPitch = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsInvertedPitch
    {
        get
        {
            return invertedPitch;
        }
        set
        {
            invertedPitch = value;
        }
    }

    public void SetFrom(GameOptions o)
    {
        invertedPitch = o.IsInvertedPitch;
    }

}
