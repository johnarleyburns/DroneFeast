using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Crosstales.RTVoice;

public class ScoreController : MonoBehaviour {

    public int CountdownSec;
    public Text CountdownText;
    public Text RemainingText;
    public AudioSource KillAS;
    public int StartingTargetCount;
    public Text DefeatTitleText;
    public Text DefeatKilledSubtitleText;
    public Text DefeatNoTimeSubtitleText;
    public Text VictoryTitleText;
    public Text VictorySubtitleText;
    public AudioSource DefeatKilledSound;
    public AudioSource DefeatNoTimeSound;
    public AudioSource VictorySound;
    public AudioSource DefeatMusic;
    public AudioSource VictoryMusic;
    public int WarnSec = 30;
    public int CriticalSec = 10;

    private bool isGameOver;
    private int hitCounter = 0;
    private int remainingCounter = 0;
    private float gameOverKeyTimer;
    private float countdown;
    private float updateTimer = 1;

	// Use this for initialization
	void Start () {
        countdown = CountdownSec;
        DefeatTitleText.enabled = false;
        DefeatKilledSubtitleText.enabled = false;
        DefeatNoTimeSubtitleText.enabled = false;
        VictoryTitleText.enabled = false;
        VictorySubtitleText.enabled = false;
        remainingCounter = StartingTargetCount;
        SyncCounters();
        Speaker.Speak("Destroy all drones before the drop ship arrives.");
	}
	
    public bool IsRunning
    {
        get
        {
            return !isGameOver;
        }
    }

	// Update is called once per frame
	void Update () {
		if (isGameOver)
        {
            UpdateGameOver();
        }
        else
        {
            UpdateInGame();
        }
	}

    private void UpdateGameOver()
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

    private void UpdateInGame()
    {
        if (countdown <= 0 && remainingCounter > 0)
        {
            SyncCounters();
            PlayerOutOfTime();
        }
        else if (updateTimer <= 0)
        {
            updateTimer = 1;
            SyncCounters();
            if (countdown <= CriticalSec)
            {
                CriticalCountdown();
            }
            else if (countdown <= WarnSec)
            {
                WarnCountdown();
            }
        }
        else
        {
            updateTimer -= Time.deltaTime;
            countdown -= Time.deltaTime;
        }
    }

    public void ScoreHit()
    {
        hitCounter++;
        remainingCounter--;
        KillAS.Play();
        SyncCounters();
        if (remainingCounter <= 0)
        {
            PlayerVictory();
        }
    }

    private void SyncCounters()
    {
        int cd = Mathf.CeilToInt(countdown);
        int min = cd / 60;
        int sec = cd % 60;
        CountdownText.text = string.Format("{0:00}:{1:00}", min, sec);
        RemainingText.text = string.Format("{0:00}", remainingCounter);
    }


    public void PlayerDeath()
    {
        RecordGameOver();
        DefeatTitleText.enabled = true;
        DefeatKilledSubtitleText.enabled = true;
        DefeatKilledSound.Play();
        DefeatMusic.Play();
        Speaker.Speak("Defeat.");
    }

    public void PlayerOutOfTime()
    {
        RecordGameOver();
        DefeatTitleText.enabled = true;
        DefeatNoTimeSubtitleText.enabled = true;
        DefeatNoTimeSound.Play();
        DefeatMusic.Play();
        Speaker.Speak("Defeat.");
    }

    public void PlayerVictory()
    {
        RecordGameOver();
        VictoryTitleText.enabled = true;
        VictorySubtitleText.enabled = true;
        VictorySound.Play();
        VictoryMusic.Play();
        Speaker.Speak("Victory!");
    }

    private void RecordGameOver()
    {
        isGameOver = true;
        gameOverKeyTimer = 3f;
    }

    private bool warned = false;

    private void WarnCountdown()
    {
        if (!warned)
        {
            warned = true;
            CountdownText.color = Color.yellow;
            Speaker.Speak("Thirty seconds remaining.");
        }
    }

    private bool critical = false;

    private void CriticalCountdown()
    {
        if (!critical)
        {
            critical = true;
            CountdownText.color = Color.red;
            Speaker.Speak("Time critical!");
        }
        CountdownText.CrossFadeAlpha(1f, 0f, false);
        CountdownText.CrossFadeAlpha(0.1f, 1f, false);
    }

}