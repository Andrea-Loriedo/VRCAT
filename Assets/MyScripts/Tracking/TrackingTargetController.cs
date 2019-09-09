using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using System;

public class TrackingTargetController : MonoBehaviour
{
    [SerializeField] GameObject trajectory;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float lineResolution = 0.01f;
    [SerializeField] TrackingExperimentManager experiment;

    // UXF
    [SerializeField] Session session;
    
    // Objects
    Collider sphereCollider;
    MeshRenderer sphereRenderer;
    ParticleSystem[] orb;

    // Constants
    static float twoPI = Mathf.PI * 2f;
    static float PIovertwo = Mathf.PI / 2f;

    // Booleans
    bool trajectory3D = false;
    bool taskStarted = false;

    void Awake()
    {
        sphereCollider = GetComponent<Collider>();
        sphereRenderer = GetComponent<MeshRenderer>();
        orb = GetComponentsInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!taskStarted)
        {
            StartCoroutine(MoveAfterDelay(session.nextTrial.block));
        }

        ChangeTargetColour("green");
    }

    private void OnTriggerExit(Collider other)
    {
        ChangeTargetColour("red");
    }

    IEnumerator MoveAfterDelay(Block block)
    {
        // sphereCollider.enabled = false;
        taskStarted = true;

        yield return new WaitForSeconds(1f);

        foreach (Trial trial in session.trials)
        {
            trial.Begin();
            experiment.StartNextTrial();
            Debug.LogFormat("Starting trial {0}", session.currentTrialNum);
            
            float t = 0f;
            
            while (t < twoPI)
            {  
                if(experiment.settings.thirdDimension)
                {
                    Vector3 newPos = Coordinates3D(experiment.settings.trajectory, t);
                    transform.localPosition = newPos;
                }
                else
                {
                    Vector3 newPos = CalculateCoordinates(experiment.settings.trajectory, t);
                    transform.localPosition = newPos;
                }
                t += Time.deltaTime * experiment.settings.speed;
                yield return null;
            }

            trial.End();

            Debug.LogFormat("Ended trial {0}", session.currentTrialNum);

            if(experiment.settings.thirdDimension)
            {
                transform.localPosition = Coordinates3D(experiment.settings.trajectory, 0f);
            }
            else
            {
                transform.localPosition = CalculateCoordinates(experiment.settings.trajectory, 0f);
            }
        }

        // turn off line renderer
        sphereRenderer.enabled = false;
        yield return new WaitForSeconds(1f);
        sphereRenderer.enabled = true;
    }

    public void ApplyBlockSettings(TrackingExperimentManager.BlockSettings settings)
    {
        if(settings.thirdDimension)
        {
            trajectory3D = true;
        } 
        
        else
        {
            trajectory3D = false;
        }

        if (settings.showTrajectory)
        {
            DrawTrajectory(settings.input);
        } 
        
        else
        {
            trajectory.SetActive(false);
        }
    }

    Vector3 CalculateCoordinates(TrajectoryInput input, float t)
    {
        // https://www.desmos.com/calculator/w52gw1ycca
        float x = input.A * Mathf.Cos(input.q * (t + PIovertwo));
        float y = input.B * Mathf.Sin(input.p * (t + PIovertwo));
        return new Vector3(x, y, 0f);
    }

    Vector3 Coordinates3D(TrajectoryInput input, float t)
    {
        // https://www.geogebra.org/3d/bajwcsth
        float x = input.A * Mathf.Cos(input.q * (t + PIovertwo));
        float y = input.B * Mathf.Sin(input.p * (t + PIovertwo));
        float z = input.C * Mathf.Sin(input.r * (t + PIovertwo));
        return new Vector3(x, y, z);
    }

    void DrawTrajectory(TrajectoryInput input)
    {
        // enable line renderer
        trajectory.SetActive(true);

        List<Vector3> positions = new List<Vector3>();
        positions.Capacity = Mathf.CeilToInt(twoPI / lineResolution);

        lineRenderer.positionCount = positions.Capacity;

        for (float tt = 0; tt < twoPI; tt += lineResolution)
        {
            if(trajectory3D)
            {
                Vector3 newPos = Coordinates3D(input, tt);
                positions.Add(newPos);
            }
            else
            {
                Vector3 newPos = CalculateCoordinates(input, tt);
                positions.Add(newPos);
            }
        }
        // set line renderer positions 
        for (int i = 0; i < positions.Capacity; i++)
        {
            Vector3 pos = (positions[i]);
            Vector3 traj = pos;
            lineRenderer.SetPosition(i, traj);
        }
    }

    public void EndBehaviour(Trial endedTrial)
    {
        if (endedTrial == session.lastTrial)
        {
            session.End();
        }
    }

    void ChangeTargetColour(string colour)
    {
        switch(colour)
        {
            case "red":
                foreach (ParticleSystem ps in orb) {
                    ps.startColor = new Color(255, 0, 5, 255);;
                }
                break;

            case "green":
                foreach (ParticleSystem ps in orb) {
                    ps.startColor = new Color(3, 255, 0, 255);
                }
                break;
        }
    }
}

/// <summary>
/// A, B and C control the amplitude of the trajectory - q, p and r control its complexity
/// </summary>
struct TrajectoryInput
{
    public float A, B, C, q, p, r;
}

