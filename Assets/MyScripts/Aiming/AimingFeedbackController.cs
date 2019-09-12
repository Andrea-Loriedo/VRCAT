using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimingFeedbackController : MonoBehaviour
{

    [SerializeField] UXF.Session session;
    [SerializeField] Text feedbackText;

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
        feedbackText.text = "";
    }

}

public enum TargetStatus
{
    Hit, Miss
}