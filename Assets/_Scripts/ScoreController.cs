using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour {

    public Text KillsText;
    public Text RemainingText;
    public AudioSource KillAS;
    public int StartingTargetCount;
    public Text GameOverText;
    public AudioSource GameOverSound;
    public AudioSource GameOverMusic;

    private bool isGameOver;
    private int hitCounter = 0;
    private int remainingCounter = 0;
    private float gameOverKeyTimer;

	// Use this for initialization
	void Start () {
        GameOverText.enabled = false;
        remainingCounter = StartingTargetCount;
        SyncCounters();
	}
	
	// Update is called once per frame
	void Update () {
		if (isGameOver)
        {
            if (gameOverKeyTimer <= 0)
            {
                if (Input.anyKey)
                {
                    if (SceneManager.sceneCount > 1)
                    {
                        SceneManager.LoadSceneAsync(0);
                    }
                    else
                    {
                        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                    }
                }
            }
            else
            {
                gameOverKeyTimer -= Time.deltaTime;
            }
        }
	}

    public void ScoreHit()
    {
        hitCounter++;
        remainingCounter--;
        KillAS.Play();
        SyncCounters();
    }

    private void SyncCounters()
    {
        KillsText.text = string.Format("{0:000}", hitCounter);
        RemainingText.text = string.Format("{0:000}", remainingCounter);
    }


    public void PlayerDeath()
    {
        GameOverText.enabled = true;
        GameOverSound.Play();
        GameOverMusic.Play();
        isGameOver = true;
        gameOverKeyTimer = 1.5f;
    }

}