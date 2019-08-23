using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using System;

public class TrackingTargetController : MonoBehaviour
{

    Collider sphereCollider; //
    MeshRenderer sphereRenderer;

    Color baseColor;
    public Color waitColor;
    public Color goColor;
    public LineRenderer lineRenderer;
    public GameObject trajectory;

    public float lineResolution = 0.01f;

    static float twoPI = Mathf.PI * 2f;
    static float PIovertwo = Mathf.PI / 2f;

    bool trajectory3D = false;

    //UXF
    public Session session;

    void Awake()
    {
        sphereCollider = GetComponent<Collider>();
        sphereRenderer = GetComponent<MeshRenderer>();
        baseColor = sphereRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(MoveAfterDelay(session.nextTrial.block));
    }


    IEnumerator MoveAfterDelay(Block block)
    {
        sphereCollider.enabled = false;
        sphereRenderer.material.color = waitColor;
        yield return new WaitForSeconds(1f);
        sphereRenderer.material.color = goColor;

        foreach (Trial trial in session.trials)
        { 
            float t = 0f;

            float speed = Convert.ToSingle(trial.settings["speed"]);
            float A = Convert.ToSingle(trial.settings["A"]);
            float B = Convert.ToSingle(trial.settings["B"]);
            float C  = Convert.ToSingle(trial.settings["C"]);
            float q = Convert.ToSingle(trial.settings["q"]);
            float p = Convert.ToSingle(trial.settings["p"]);
            float r = Convert.ToSingle(trial.settings["r"]);
            bool showTrajectory = (bool)trial.settings["show_trajectory"];
            bool third_dimension = (bool)trial.settings["3D_Mode"];

            TrajectoryInput ti = new TrajectoryInput()
            {
                A = A,
                B = B,
                C = C,
                q = q,
                p = p,
                r = r
            };

            if(third_dimension)
            {
                trajectory3D = true;
            }
            else
            {
                trajectory3D = false;
            }

            if (showTrajectory)
            {
                DrawTrajectory(ti);
            }
            else
            {
                trajectory.SetActive(false);
            }

            trial.Begin();
            Debug.LogFormat("Starting trial {0}", session.currentTrialNum);

            while (t < twoPI)
            {  
                if(third_dimension)
                {
                    Vector3 newPos = Coordinates3D(ti, t);
                    transform.localPosition = newPos;
                }
                else
                {
                    Vector3 newPos = CalculateCoordinates(ti, t);
                    transform.localPosition = newPos;
                }
                t += Time.deltaTime * speed;
                yield return null;
            }

            trial.End();

            Debug.LogFormat("Ended trial {0}", session.currentTrialNum);

            if(third_dimension)
            {
                transform.localPosition = Coordinates3D(ti, 0f);
            }
            else
            {
                transform.localPosition = CalculateCoordinates(ti, 0f);
            }
        }

        // turn off line renderer
        sphereRenderer.enabled = false;
        yield return new WaitForSeconds(1f);

        sphereRenderer.enabled = true;
        sphereRenderer.material.color = baseColor;
        sphereCollider.enabled = true;
    }

    Vector3 CalculateCoordinates(TrajectoryInput ti, float t)
    {
        // https://www.desmos.com/calculator/w52gw1ycca
        float x = ti.A * Mathf.Cos(ti.q * (t + PIovertwo));
        float y = ti.B * Mathf.Sin(ti.p * (t + PIovertwo));
        return new Vector3(x, y, 0f);
    }

    Vector3 Coordinates3D(TrajectoryInput ti, float t)
    {
        // https://www.geogebra.org/3d/bajwcsth
        float x = ti.A * Mathf.Cos(ti.q * (t + PIovertwo));
        float y = ti.B * Mathf.Sin(ti.p * (t + PIovertwo));
        float z = ti.C * Mathf.Sin(ti.r * (t + PIovertwo));
        return new Vector3(x, y, z);
    }

    void DrawTrajectory(TrajectoryInput ti)
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
                Vector3 newPos = Coordinates3D(ti, tt);
                positions.Add(newPos);
            }
            else
            {
                Vector3 newPos = CalculateCoordinates(ti, tt);
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
}

/// <summary>
/// A, B and C control the amplitude of the trajectory - q, p and r control its complexity
/// </summary>
struct TrajectoryInput
{
    public float A, B, C, q, p, r;
}