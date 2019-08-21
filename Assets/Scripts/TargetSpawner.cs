using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public InterceptCheck interceptCheck;
    public Target targetPrefab;

    public void Spawn(UXF.Trial trial)
    {
        StartCoroutine(SpawnSequence(trial));
    }

    IEnumerator SpawnSequence(UXF.Trial trial)
    {
        float width = trial.settings.GetFloat("target_width");
        float height = trial.settings.GetFloat("target_height");
        float speed = trial.settings.GetFloat("target_speed");
        float position = trial.settings.GetFloat("target_x_position");
        float movementDuration = trial.settings.GetFloat("target_movement_duration");

        Target newTarget = Instantiate(targetPrefab, transform);
        newTarget.Regenerate(width, height);       
        interceptCheck.activeTarget = newTarget;

        Vector3 newPos = Vector3.zero;
        newPos.x = position;
        newPos.y = movementDuration * speed;
        newTarget.transform.localPosition = newPos;

        yield return new WaitForSeconds(trial.settings.GetFloat("prelaunch_duration"));

        newTarget.Launch(speed);
    }


    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit");
    }

}
