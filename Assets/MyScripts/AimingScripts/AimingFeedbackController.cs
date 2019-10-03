using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UXF;
using UXFExamples;

public class AimingFeedbackController : MonoBehaviour
{

    [SerializeField] UXF.Session session;
    [SerializeField] Text feedbackText;
    [SerializeField] AimingExperimentManager experiment;

    void Awake()
    {
        Clear();
    }

    public void ShowFeedback(TargetStatus status)
    {
        switch(status)
        {
            case TargetStatus.Miss:
            feedbackText.text = "Catch the sphere";
            break;
            case TargetStatus.Hit:
            feedbackText.text = "Well done!";
            break;
        }
    }

    public void Clear()
    {
        Debug.LogFormat("Clear feedback");
        feedbackText.text = "";
    }

}

public enum TargetStatus
{
    Hit, Miss
}