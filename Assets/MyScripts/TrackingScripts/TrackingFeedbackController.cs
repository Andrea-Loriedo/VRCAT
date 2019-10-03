using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackingFeedbackController : MonoBehaviour
{

    [SerializeField] UXF.Session session;
    [SerializeField] Text feedbackText;

    void Awake()
    {
        Clear();
    }

    public void ShowFeedback(Status status)
    {
       switch(status)
       {
           case Status.Close:
           feedbackText.text = "Perfect!";
           break;
           case Status.Far:
           feedbackText.text = "Get closer to the target";
           break;
       }
    }

    public void Clear()
    {
        feedbackText.text = "";
    }

}

public enum Status
{
    Close, Far
}