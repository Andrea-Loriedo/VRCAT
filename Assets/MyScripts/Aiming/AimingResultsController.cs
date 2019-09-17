using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class AimingResultsController : MonoBehaviour
{
    List<float> speeds = new List<float>();
    [HideInInspector] public float speed;

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
        Debug.LogFormat("Recs = {0}, Sum = {1}", recordings, sum);

		return mean;
	}

    public void RecordVelocity(Vector3 currPosition, Vector3 prevPosition)
    {
        speed = (currPosition - prevPosition).magnitude / Time.fixedDeltaTime;
        // Debug.LogFormat("Speed = {0}", speed);
    }

    // public void RecordResults(Vector3 cursorPosition, Vector3 velocity)
    // {
    //     if (activeTarget)
    //     {
    //         Vector3 hitPos = cursorPosition;
    //         hitPos.z = transform.position.z;
    //         bool hit = activeTarget.CheckHit(hitPos, velocity);
    //         Outcome outcome = hit ? Outcome.Hit : Outcome.Miss;

    //         UXF.Trial trial = session.CurrentTrial;
    //         trial.result["outcome"] = outcome;
    //         trial.result["target_y_position"] = activeTarget.transform.localPosition.y;
    //         trial.result["intercept_x_position"] = cursorPosition.x;
    //         trial.result["intercept_x_velocity"] = velocity.x;
    //         trial.result["intercept_z_velocity"] = velocity.z;
    //         trial.result["intercept_time"] = Time.time;

    //         session.Invoke("EndCurrentTrial", trial.settings.GetFloat("post_intercept_buffer_duration"));

    //         if (hit & trial.settings.GetString("block_type") != "Practice") score.Increment();

    //         activeTarget = null;

    //         feedback.ShowFeedback(outcome);

    //         startZone.ClearTimeout();
    //     }
    // }
}
