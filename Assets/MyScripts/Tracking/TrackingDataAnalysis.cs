using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class TrackingDataAnalysis : MonoBehaviour
{
    // Objects
    [SerializeField] Transform cursor;
    [SerializeField] Transform target;

    List<float> distances = new List<float>();

    bool recording = false;

	float sum = 0f;

    void Start()
    {
		distances.Capacity = 1024;
    }

    void FixedUpdate()
    {
        if (recording)
        {
            float distance = ComparePosition();
            distances.Add(distance);
			sum += distance;
        }
    }

    public void StartRecording(Trial trial)
    {
		distances.Clear();
        recording = true;
    }

	public void StopRecording(Trial trial)
    {
        recording = false;
        float m = MeanCalculation();
		trial.result["mean_distance"] = m;
		sum = 0;
    }

	float MeanCalculation()
	{
		int recordings = distances.Count;
		float mean = (sum/recordings);
        Debug.LogFormat("Recs = {0}, Sum = {1}", recordings, sum);
		return mean;
	}

    float ComparePosition()
    {
        Vector3 cursorCurrPos = cursor.position;
        Vector3 targetCurrPos = target.position;
        float dist = (Vector3.Distance(cursorCurrPos, targetCurrPos)) * 100f; // distance in cm         
        return dist;
    }
}

