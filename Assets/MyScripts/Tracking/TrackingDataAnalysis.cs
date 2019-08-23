using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class TrackingDataAnalysis : MonoBehaviour
{

    List<float> distances = new List<float>();
    public Transform cursor;
    public Transform target;

    bool recording = false;

	float sum = 0f;

    // Use this for initialization
    void Start()
    {
		distances.Capacity = 1024;
    }

    // Update is called once per frame
    void Update()
    {
        if (recording)
        {
            float difference = ComparePosition();
            distances.Add(difference);
			sum += difference;
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
		// calculate mean
        float m = MeanCalculation();
		trial.result["mean_distance"] = m;
		sum = 0;
    }

	float MeanCalculation()
	{
		int recordings = distances.Count;
		float mean = (sum/recordings);

		return mean;
	}

    float ComparePosition()
    {
        Vector3 cursorCurrPos = cursor.localPosition;
        Vector3 targetCurrPos = target.localPosition;
        Vector3 diff = targetCurrPos - cursorCurrPos;

        return diff.magnitude;
    }
}

