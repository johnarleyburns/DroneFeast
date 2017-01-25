using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMessage : MonoBehaviour {

    public Text TitleText;
    public Text SubtitleText;

    private Color origTitleColor;
    private Color origSubtitleColor;

	// Use this for initialization
	void Start () {
        TitleText.enabled = true;
        SubtitleText.enabled = true;
        TitleText.CrossFadeAlpha(0.0f, 4f, false);
        SubtitleText.CrossFadeAlpha(0.0f, 5f, false);
    }

    // Update is called once per frame
    void Update () {
	}
}
