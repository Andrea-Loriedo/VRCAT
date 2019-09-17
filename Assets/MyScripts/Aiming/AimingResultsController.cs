using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class AimingResultsController : MonoBehaviour
{
    [HideInInspector] public float speed;

    List<float> speeds = new List<float>();

    public UXF.Session session;

    bool recording = false;

    float sum = 0f;

    void Start()
    {
        speeds.Capacity = 1024;
    }

    void FixedUpdate()
    { 
        if (recording)
        {
            speeds.Add(speed);
			sum += speed;
        }
    }

    public void StartRecording(Trial trial)
    {
		speeds.Clear();
        recording = true;
    }
    
    public void StopRecording(Trial trial)
    {
        recording = false;
        float m = MeanCalculation();
		trial.result["mean_speed"] = m;
		sum = 0;
    }

	float MeanCalculation()
	{
		int recordings = speeds.Count;
		float mean = (sum/recordings);

		return mean;
	}

    public void RecordVelocity(Vector3 currPosition, Vector3 prevPosition)
    {
        speed = (currPosition - prevPosition).magnitude / Time.fixedDeltaTime;
    }
}
