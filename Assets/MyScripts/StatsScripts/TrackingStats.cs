using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class TrackingStats : MonoBehaviour
{
    public UIStatistic meanDistanceStat;
   
    public void ShowTrackingStatistics(UXF.Session session)
    {
        if (!session.settings.GetBool("show_stats")) return;
        
        UXF.Block block1 = session.GetBlock(1);
        UXF.Block block2 = session.GetBlock(2);
        UXF.Block block3 = session.GetBlock(3);
        UXF.Block block4 = session.GetBlock(4);
        UXF.Block block5 = session.GetBlock(5);

        List<UXF.Block> blocks = new List<UXF.Block>();

        blocks.Add(block1);
        blocks.Add(block2);
        blocks.Add(block3);
        blocks.Add(block4);
        blocks.Add(block5);

        float distanceCum = 0f;
        float averageDistance = 0f;
        int trialCount = 0;

        foreach (UXF.Block block in blocks)
        {
            foreach (UXF.Trial trial in block.trials)
            {
                if (trial.status != UXF.TrialStatus.Done) return;

                float cursorDistance = (float) trial.result["mean_distance"];
                distanceCum += cursorDistance;
                trialCount++;
            }
        }
        
        meanDistanceStat.UpdateStatistic(
            "Average distance", string.Format("{0} cm", distanceCum/trialCount)
        );

        gameObject.SetActive(true);
    }

}

