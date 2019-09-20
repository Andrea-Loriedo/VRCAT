using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class AimingResultsController : MonoBehaviour
{
    [HideInInspector] public float speed;
    [SerializeField] CursorController cursor;

    List<float> speeds = new List<float>();

    Vector3 currPos;
    Vector3 prevPos;    

    public UXF.Session session;

    bool recording = false;

    float sum = 0f;

    void Start()
    {
        speeds.Capacity = 1024;
        prevPos = cursor.GetPosition();
    }

    void FixedUpdate()
    { 
        if (recording)
        {
            currPos = cursor.GetPosition();
            RecordVelocity(currPos, prevPos);
            prevPos = currPos;

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

    void RecordVelocity(Vector3 currPosition, Vector3 prevPosition)
    {
        speed = (currPosition - prevPosition).magnitude / Time.fixedDeltaTime;
        Debug.LogFormat("Speed = {0}", speed);
    }
}
