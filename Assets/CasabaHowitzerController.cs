using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Greyman;
using Crosstales.RTVoice;

public class CasabaHowitzerController : MonoBehaviour {

    public ScoreController Scorer;
    public OffScreenIndicator Indicator;
    public AudioSource LaunchSound;
    public GameObject Explosion;
    public float SpawnDist = 1;
    public float Range = 1000;
    public float FireInterval = 1f;
    public float EffectiveAngleDeg = 5f;

    // Use this for initialization
    void Start()
    {

    }

    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if (Scorer.IsRunning)
        {
            UpdateGuns();
        }
    }

    private void UpdateGuns()
    {
        if (Input.GetAxisRaw("FireGun") == 1 && timer <= 0)
        {
            timer = FireInterval;
            FireHowitzer();
        }
        timer -= Time.deltaTime;
    }

    private void FireHowitzer()
    {
        LaunchSound.Play();
        Vector3 p = transform.position + SpawnDist * transform.forward;
        GameObject b = GameObject.Instantiate(Explosion, p, transform.rotation, null);
        Vector3 v = GravityEngine.instance.GetVelocity(transform.parent.gameObject);
        GravityEngine.instance.AddBody(b);
        GravityEngine.instance.UpdatePositionAndVelocity(b.GetComponent<NBody>(), p, v);
        CheckForHits(p);
    }

    private void CheckForHits(Vector3 source)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("EnemyNBody");
        int hits = 0;
        foreach (GameObject t in targets)
        {
            Vector3 pos = t.transform.position;
            Vector3 bearing = pos - source;
            float dist = bearing.magnitude;
            if (dist <= Range)
            {
                float angle = Vector3.Angle(transform.forward, bearing);
                if (angle <= EffectiveAngleDeg)
                {
                    t.GetComponentInChildren<ScoreTrigger>().RecordHit();
                    hits++;
                }
            }
        }
        HitEffects(hits);
    }

    private void HitEffects(int hits)
    {
        if (hits >= 4)
        {
            Speaker.Speak("Car Nahge");
        }
        else if (hits >= 3)
        {
            Speaker.Speak("Triple hit");
        }
        else if (hits >= 2)
        {
            Speaker.Speak("Double hit");
        }
    }

}
