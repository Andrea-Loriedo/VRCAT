using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentGenerator : MonoBehaviour
{
    
    public void GenerateExperiment(UXF.Session session)
    {
        float preLaunchMin = session.settings.GetFloat("prelaunch_duration_min");
        float preLaunchMax = session.settings.GetFloat("prelaunch_duration_max");

        // practice trials. two of each speed (not shuffled)
        UXF.Block practice = session.CreateBlock();
        practice.settings.SetValue("block_type", "Practice");

        foreach (float speed in session.settings.GetFloatList("speeds"))
        {
            for (int i = 0; i < 2; i++)
            {
                UXF.Trial trial = practice.CreateTrial();
                trial.settings.SetValue("target_width", 0.07f);
                trial.settings.SetValue("target_height", 0.05f);
                trial.settings.SetValue("target_speed", speed);
                trial.settings.SetValue("target_x_position", 0f);
                trial.settings.SetValue("repeat_num", i + 1);
                trial.settings.SetValue("prelaunch_duration", preLaunchMax);
            }
        }

        // assessment trials. repeats of each combination
        UXF.Block assessment = session.CreateBlock();
        assessment.settings.SetValue("block_type", "Assessment");

        foreach (float speed in session.settings.GetFloatList("speeds"))
        {
            foreach (float height in session.settings.GetFloatList("heights"))
            {
                foreach (float width in session.settings.GetFloatList("widths"))
                {
                    foreach (float position in session.settings.GetFloatList("positions"))
                    {
                        for (int i = 0; i < session.settings.GetInt("repeats"); i++)
                        {
                            UXF.Trial trial = assessment.CreateTrial();
                            trial.settings.SetValue("target_width", width);
                            trial.settings.SetValue("target_height", height);
                            trial.settings.SetValue("target_speed", speed);
                            trial.settings.SetValue("target_x_position", position);
                            trial.settings.SetValue("repeat_num", i + 1);

                            float preLaunch = Random.Range(preLaunchMin, preLaunchMax);
                            trial.settings.SetValue("prelaunch_duration", preLaunch);
                        }
                    }
                }
            }
        }

        // shuffle the assessment block
        if (session.settings.GetBool("fixed_randomisation"))
        {
            int randomSeed = session.settings.GetInt("random_seed");
            UXF.Extensions.Shuffle(assessment.trials, new System.Random(randomSeed));
            Random.InitState(randomSeed); // for unity random values
        }
        else
        {
            UXF.Extensions.Shuffle(assessment.trials);
        }

    }


    public void EndIfLastTrial(UXF.Trial trial)
    {
        if (trial == trial.session.LastTrial)
        {
            trial.session.End();
        }
    }

}
