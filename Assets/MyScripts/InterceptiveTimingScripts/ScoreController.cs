using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreController : MonoBehaviour
{
    Text text;
    int score = 0;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        text = GetComponent<Text>();
        SetText();
    }


    public void Increment()
    {
        score++;      
        SetText();  
    }

    void SetText()
    {
        text.text = string.Format("{0} point{1}", score, score == 1 ? "" : "s");
    }

    public void Hide()
    {
        text.enabled = false;
    }

    public void Show()
    {
        text.enabled = true;
    }


    public void Setup(UXF.Session session)
    {
        try
        {
            text.enabled = session.NextTrial.settings.GetString("block_type") == "Assessment";
        }
        catch (UXF.NoSuchTrialException) { }
    }

    public void Setup(UXF.Trial trial)
    {
        try
        {
            text.enabled = trial.session.NextTrial.settings.GetString("block_type") == "Assessment";
        }
        catch (UXF.NoSuchTrialException) { }
    }

}
