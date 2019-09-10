using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class TrackingExperimentManager : MonoBehaviour
{
    // UXF
    [SerializeField] Session session;
    [HideInInspector]
    public BlockSettings settings;

    // Game
    [SerializeField] TrajectoryController trajectory;

    // Booleans
    bool sessionHasEnded;
    bool newBlockStart;

    void Start () {
        gameObject.SetActive(true);
    }

    public void StartNextTrial()
    {
        session.nextTrial.Begin();
        settings.speed = (float) session.currentTrial.settings["speed"]; 
        settings.showTrajectory = (bool) session.currentTrial.settings["show_trajectory"];
        settings.input.A = (float) session.currentTrial.settings["A"]; 
        settings.input.B = (float) session.currentTrial.settings["B"]; 
        settings.input.C = (float) session.currentTrial.settings["C"]; 
        settings.input.q = (float) session.currentTrial.settings["q"]; 
        settings.input.p = (float) session.currentTrial.settings["p"]; 
        settings.input.r = (float) session.currentTrial.settings["r"]; 
        trajectory.ApplyBlockSettings(settings);
    }

    public BlockSettings GetBlockSettings()
    {
        return settings;
    }
}

/// <summary>
/// A, B and C control the amplitude of the trajectory - q, p and r control its complexity
/// </summary>
public struct BlockSettings
{
    public struct TrajectoryInput
    {
        public float  A, B, C, q, p, r;
    };

    public float speed;
    public bool showTrajectory, thirdDimension;
    public TrajectoryInput input;
}