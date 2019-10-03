using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class AimingStats : MonoBehaviour
{
    public UIStatistic hitSpeedStat;
   
    public void ShowAimingStatistics(UXF.Session session)
    {
        if (!session.settings.GetBool("show_stats")) return;
        
        UXF.Block block1 = session.GetBlock(1);
        UXF.Block block2 = session.GetBlock(2);
        UXF.Block block3 = session.GetBlock(3);

        List<UXF.Block> blocks = new List<UXF.Block>();

        blocks.Add(block1);
        blocks.Add(block2);
        blocks.Add(block3);

        float speedCum = 0f;
        float meanSpeed = 0f;
        int trialCount = 0;

        foreach (UXF.Block block in blocks)
        {
            foreach (UXF.Trial trial in block.trials)
            {
                if (trial.status != UXF.TrialStatus.Done) return;

                float cursorSpeed = (float) trial.result["mean_speed"];
                speedCum += cursorSpeed;
                trialCount++;
            }
        }
        
        hitSpeedStat.UpdateStatistic(
            "Average hit speed", string.Format("{0} m/s", speedCum/trialCount)
        );

        gameObject.SetActive(true);
    }

}

