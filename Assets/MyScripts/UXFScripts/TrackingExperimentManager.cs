using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class TrackingExperimentManager : MonoBehaviour
{
    // UXF
    [SerializeField] Session session;
    [HideInInspector]
    public TrackingBlockSettings trackingSettings;

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
        trackingSettings.speed = (float) session.currentTrial.settings["speed"]; 
        trackingSettings.showTrajectory = (bool) session.currentTrial.settings["show_trajectory"];
        trackingSettings.input.A = (float) session.currentTrial.settings["A"]; 
        trackingSettings.input.B = (float) session.currentTrial.settings["B"]; 
        trackingSettings.input.C = (float) session.currentTrial.settings["C"]; 
        trackingSettings.input.q = (float) session.currentTrial.settings["q"]; 
        trackingSettings.input.p = (float) session.currentTrial.settings["p"]; 
        trackingSettings.input.r = (float) session.currentTrial.settings["r"]; 
        trajectory.ApplyBlockSettings(trackingSettings);
    }

    public TrackingBlockSettings GetBlockSettings()
    {
        return trackingSettings;
    }
}

/// <summary>
/// A, B and C control the amplitude of the trajectory - q, p and r control its complexity
/// </summary>
public struct TrackingBlockSettings
{
    public struct TrajectoryInput
    {
        public float  A, B, C, q, p, r;
    };

    public float speed;
    public bool showTrajectory, thirdDimension;
    public TrajectoryInput input;
}