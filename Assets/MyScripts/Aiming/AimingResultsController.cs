using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingResultsController : MonoBehaviour
{
    [HideInInspector] public float speed;
    public UXF.Session session;
    Vector3 m_physMousePos;
    
    void Start()
    {
    }
    
    public void RecordVelocity(Vector3 currPosition, Vector3 prevPosition)
    {
        // speed = rb.velocity.magnitude;  

        // Debug.LogFormat("Speed = {0}", speed);  
        float speed = (currPosition - prevPosition).magnitude / Time.fixedDeltaTime;
        Debug.LogFormat("Speed = {0}", speed);
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
