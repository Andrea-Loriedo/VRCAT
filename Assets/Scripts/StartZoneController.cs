using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartZoneController : MonoBehaviour
{   
    [ColorUsage(true, true)]
    public Color mainColor = Color.red;
    [ColorUsage(true, true)]
    public Color readyColor = new Color(1f, 0.5f, 0f); // orange
    [ColorUsage(true, true)]
    public Color goColor = Color.green;
    public UXF.Session session;
    public StartZoneState state;
    public MeshRenderer rend;
    public UnityEvent onEnter;
    public UnityEvent onGo;
    public FeedbackController feedback;

    Material mat;

    Coroutine runningSequence;



    void Awake()
    {
        mat = rend.material;
        SetState(StartZoneState.Preparing);
    }


    IEnumerator RunSequence()
    {   
        bool nextTrialExists = true;
        float delayTime;
        try
        {
            delayTime = session.NextTrial.settings.GetFloat("hold_duration");
        }
        catch (UXF.NoSuchTrialException)
        {
            nextTrialExists = false;
            delayTime = float.PositiveInfinity;
        }

        if (nextTrialExists)
        {
            GetReady();
            yield return new WaitForSeconds(delayTime);
            Go();
        }
    }

    void SetState(StartZoneState newState)
    {
        state = newState;

        // modify colour based on state
        switch (state)
        {
            // could be dictionary
            case StartZoneState.Waiting:
                mat.SetColor("_EmissionColor", mainColor);
                mat.EnableKeyword("_EMISSION"); 
                break;
            case StartZoneState.GetReady:
                mat.SetColor("_EmissionColor", readyColor);
                mat.EnableKeyword("_EMISSION"); 
                onEnter.Invoke();
                break;
            case StartZoneState.Go:
                mat.SetColor("_EmissionColor", goColor);
                mat.EnableKeyword("_EMISSION"); 
                onGo.Invoke();
                break;
        }
    }

    public void ResetToNormal()
    {
        Debug.Log("Resetting");
        SetState(StartZoneState.Waiting);
    }

    void GetReady()
    {
        Debug.Log("Get ready...");
        SetState(StartZoneState.GetReady);
    }

    void Go()
    {
        Debug.Log("Go!");
        SetState(StartZoneState.Go);

        // now begin the next trial.
        session.BeginNextTrial();
    }

    public void Enter()
    {
        ClearTimeout();
        switch (state)
        {
            case StartZoneState.Waiting:
                // begin the sequence
                runningSequence = StartCoroutine(RunSequence());
                break;
        }	
    }
    public void Leave()
    {
        Debug.Log("Leaving");
        switch (state)
        {
            case StartZoneState.GetReady:
                StopCoroutine(runningSequence);
                SetState(StartZoneState.Waiting);
                break;
            case StartZoneState.Go:
                try { Invoke("Timeout", session.NextTrial.settings.GetFloat("timeout_duration")); }
                catch (UXF.NoSuchTrialException) { }
                break;
        }	
    }

    public void ClearTimeout()
    {
        CancelInvoke("Timeout");
    }

    void Timeout()
    {
        session.CurrentTrial.End();
        session.CurrentTrial.result["outcome"] = Outcome.TooEarly;
        feedback.ShowFeedback(Outcome.TooEarly);
    }

}


public enum StartZoneState
{
    Preparing, Waiting, GetReady, Go
}