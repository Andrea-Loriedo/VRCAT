using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FeedbackController : MonoBehaviour
{

    public UXF.Session session;
    public Text feedbackText;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Clear();
    }

    public void ShowFeedback(Outcome outcomeType)
    {
        float delay = 0f;
        if (outcomeType == Outcome.Hit)
        {
            feedbackText.text = "Hit!";
            delay = session.CurrentTrial.settings.GetFloat("feedback_time_short");
        }
        else if (outcomeType == Outcome.Miss)
        {
            feedbackText.text = "Missed!";
            delay = session.CurrentTrial.settings.GetFloat("feedback_time_short");
        }
        else if (outcomeType == Outcome.TooEarly)
        {
            feedbackText.text = session.CurrentTrial.settings.GetString("timeout_message");;
            delay = session.CurrentTrial.settings.GetFloat("feedback_time_long");
        }

        Invoke("Clear", delay);
    }


    public void Clear()
    {
        feedbackText.text = "";
    }

}


public enum Outcome
{
    Hit, Miss, TooEarly
}