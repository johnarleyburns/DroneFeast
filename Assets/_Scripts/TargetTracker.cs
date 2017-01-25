using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Greyman;

public class TargetTracker : MonoBehaviour {

    public GameObject TargetPredictorPrefab;
    public GameObject CenterBody;
    public OffScreenIndicator Indicator;
    public int TargetIndicatorId;
    public int NearestIndicatorId;
    
    private GameObject nearestTarget = null;
    private GameObject predictorBody;
    private OrbitPredictor predictor;
    private const float TIMER_INTERVAL = 1; // sec;
    private float updateTimer = TIMER_INTERVAL; // sec

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        if (updateTimer <= 0)
        {
            UpdateNearestTarget();
        }
        else
        {
            updateTimer -= Time.deltaTime;
        }
    }

    public void RemoveTarget(GameObject t)
    {
        nearestTarget = null;
        if (predictorBody != null)
        {
            Destroy(predictorBody);
        }
        updateTimer = TIMER_INTERVAL;
    }

    private void UpdateNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float nearestDist = 0;
        foreach (GameObject t in targets)
        {
            if (!t.activeInHierarchy)
            {
                continue;
            }
            float dist = Vector3.Distance(transform.position, t.transform.position);
            if (nearest == null)
            {
                nearest = t;
                nearestDist = dist;
            }
            else
            {
                if (dist < nearestDist)
                {
                    nearest = t;
                    nearestDist = dist;
                }
            }
        }
        UpdateOrbit(nearestTarget, nearest);
        UpdateIndicator(nearestTarget, nearest);
        nearestTarget = nearest;
    }

    private void UpdateOrbit(GameObject oldTarget, GameObject newTarget)
    {
        bool isChanged = oldTarget != newTarget;
        if (isChanged)
        {
            if (oldTarget != null)
            {
                if (predictorBody != null)
                {
                    Destroy(predictorBody);
                }
            }
            if (newTarget != null)
            {
                predictorBody = Instantiate(TargetPredictorPrefab, null);
                predictor = predictorBody.GetComponent<OrbitPredictor>();
                predictor.centerBody = CenterBody;
                predictor.body = newTarget;
            }
        }
    }

    private void UpdateIndicator(GameObject oldTarget, GameObject newTarget)
    {
        bool isChanged = oldTarget != newTarget;
        if (isChanged)
        {
            if (oldTarget != null)
            {
                Indicator.RemoveIndicator(oldTarget.transform);
                Indicator.AddIndicator(oldTarget.transform, TargetIndicatorId);
            }
            if (newTarget != null)
            {
                Indicator.RemoveIndicator(newTarget.transform);
                Indicator.AddIndicator(newTarget.transform, NearestIndicatorId);
            }
        }
    }

}
