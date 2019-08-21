using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsPanel : MonoBehaviour
{
    public UIStatistic hitsStat;
    public UIStatistic spatialErrorStat;
    public UIStatistic timingErrorStat;
    public UIStatistic timingDirectionStat;
    public UIStatistic hitSpeedStat;
   
    

    public void ShowStatistics(UXF.Session session)
    {
        if (!session.settings.GetBool("show_stats")) return;

        UXF.Block assessmentBlock = session.GetBlock(2);

        // cum = cumulative
        int hitsCum = 0, timingDirectionCum = 0;
        float spatialErrorCum = 0f, timingErrorCum = 0f, hitSpeedCum = 0f;
        int i = 0;

        foreach (UXF.Trial trial in assessmentBlock.trials)
        {
            if (trial.status != UXF.TrialStatus.Done) return;

            Outcome outcome = (Outcome) trial.result["outcome"];

            if (outcome == Outcome.Hit) hitsCum++;
            else if (outcome == Outcome.TooEarly) continue;

            float spatialError = trial.settings.GetFloat("target_x_position") - (float) trial.result["intercept_x_position"];
            spatialErrorCum += Mathf.Abs(spatialError);

            float timingError = -1 * (float) trial.result["target_y_position"] / trial.settings.GetFloat("target_speed");
            timingErrorCum += Mathf.Abs(timingError);
            if (timingError > 0) timingDirectionCum++; // if late?

            Vector3 v = new Vector3((float) trial.result["intercept_x_velocity"], 0, (float) trial.result["intercept_z_velocity"]);
            hitSpeedCum += v.magnitude;
            i++;
        }

        int n = assessmentBlock.trials.Count;
        
        hitsStat.UpdateStatistic(
            "Hits", string.Format("{0} / {1} ({2:0.0}%)", hitsCum, n, 100f * (float) hitsCum / n)
        );

        if (i == 0) i = 1;

        spatialErrorStat.UpdateStatistic(
            "Av. spatial error", string.Format("{0:0.00} cm", 100f * spatialErrorCum / i)
        );

        timingErrorStat.UpdateStatistic(
            "Av. timing error", string.Format("{0:0.000} s", timingErrorCum / i)
        );

        timingDirectionStat.UpdateStatistic(
            "Timing error type", string.Format("Usually too {0}", ((float) timingDirectionCum / i) > 0.5f ? "late" : "early")
        );

        hitSpeedStat.UpdateStatistic(
            "Av. hit speed", string.Format("{0:0.00} cm/s", 100 * hitSpeedCum / i)
        );

        gameObject.SetActive(true);
    }

}
