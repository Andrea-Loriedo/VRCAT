using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingResultsController : MonoBehaviour
{
    public UXF.Session session;

    public void RecordVelocity(Gameobject cursor, Vector3 velocity)
    {
        Vector3 averageVelocity = velocity.x
        velocity;

    }

    public void RecordResults(Vector3 cursorPosition, Vector3 velocity)
    {
        if (activeTarget)
        {
            Vector3 hitPos = cursorPosition;
            hitPos.z = transform.position.z;
            bool hit = activeTarget.CheckHit(hitPos, velocity);
            Outcome outcome = hit ? Outcome.Hit : Outcome.Miss;

            UXF.Trial trial = session.CurrentTrial;
            trial.result["outcome"] = outcome;
            trial.result["target_y_position"] = activeTarget.transform.localPosition.y;
            trial.result["intercept_x_position"] = cursorPosition.x;
            trial.result["intercept_x_velocity"] = velocity.x;
            trial.result["intercept_z_velocity"] = velocity.z;
            trial.result["intercept_time"] = Time.time;

            session.Invoke("EndCurrentTrial", trial.settings.GetFloat("post_intercept_buffer_duration"));

            if (hit & trial.settings.GetString("block_type") != "Practice") score.Increment();

            activeTarget = null;

            feedback.ShowFeedback(outcome);

            startZone.ClearTimeout();
        }
    }
}
