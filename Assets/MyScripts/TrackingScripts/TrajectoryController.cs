using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    [SerializeField] TrackingExperimentManager experiment;
    [SerializeField] float lineResolution = 0.01f;
    LineRenderer lineRenderer;

    // Booleans
    bool trajectory3D;

    // Constants
    static float twoPI = Mathf.PI * 2f;
    static float PIovertwo = Mathf.PI / 2f;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();  
    }

    void DrawTrajectory(TrackingBlockSettings.TrajectoryInput input)
    {
        // enable line renderer
        gameObject.SetActive(true);

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
                Vector3 newPos = Coordinates2D(input, tt);
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

    public void ApplyBlockSettings(TrackingBlockSettings settings)
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
            DrawTrajectory(experiment.trackingSettings.input);
        } 

        else
        {
            gameObject.SetActive(false);
        }
    }

    public Vector3 Coordinates2D(TrackingBlockSettings.TrajectoryInput input, float t)
    {
        // https://www.desmos.com/calculator/w52gw1ycca
        float x = input.A * Mathf.Cos(input.q * (t + PIovertwo));
        float y = input.B * Mathf.Sin(input.p * (t + PIovertwo));
        return new Vector3(x, y, 0f);
    }

    public Vector3 Coordinates3D(TrackingBlockSettings.TrajectoryInput input, float t)
    {
        // https://www.geogebra.org/3d/bajwcsth
        float x = input.A * Mathf.Cos(input.q * (t + PIovertwo));
        float y = input.B * Mathf.Sin(input.p * (t + PIovertwo));
        float z = input.C * Mathf.Sin(input.r * (t + PIovertwo));
        return new Vector3(x, y, z);
    }
}
