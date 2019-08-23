using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UXF;

public class TrackingSessionGenerator : MonoBehaviour
{   
    public void GenerateExperiment(Session session)
    {
        int numTrials = session.settings.GetInt("trials_per_block");

        //  Retrieve settings for each block from .json file
        Dictionary<string, object> Block1Settings = (Dictionary<string, object>)session.settings["block_1_settings"];
        Dictionary<string, object> Block2Settings = (Dictionary<string, object>)session.settings["block_2_settings"];
        Dictionary<string, object> Block3Settings = (Dictionary<string, object>)session.settings["block_3_settings"];
        Dictionary<string, object> Block4Settings = (Dictionary<string, object>)session.settings["block_4_settings"];
        Dictionary<string, object> Block5Settings = (Dictionary<string, object>)session.settings["block_5_settings"];
       
        // Create the experiment blocks
        Block block1 = session.CreateBlock(numTrials);
        Block block2 = session.CreateBlock(numTrials);
        Block block3 = session.CreateBlock(numTrials);
        Block block4 = session.CreateBlock(numTrials);
        Block block5 = session.CreateBlock(numTrials);

        // Assign the relevant settingsto each block
        AssignBlockSettings(Block1Settings, block1);
        AssignBlockSettings(Block2Settings, block2);
        AssignBlockSettings(Block3Settings, block3);
        AssignBlockSettings(Block4Settings, block4);
        AssignBlockSettings(Block5Settings, block5);
    }

    void AssignBlockSettings(Dictionary<string, object> settings, Block block)
    {
        float speed = System.Convert.ToSingle(settings["speed"]);
        float A = System.Convert.ToSingle(settings["A"]);
        float B = System.Convert.ToSingle(settings["B"]);
        float C = System.Convert.ToSingle(settings["C"]);
        float q = System.Convert.ToSingle(settings["q"]);
        float p = System.Convert.ToSingle(settings["p"]);
        float r = Convert.ToSingle(settings["r"]);
        bool showTrajectory = (bool) settings["show_trajectory"];
        bool third_dimension = (bool) settings["3D_Mode"];
    
        block.settings["speed"] = speed;
        block.settings["A"] = A;
        block.settings["B"] = B;
        block.settings["C"] = C;
        block.settings["q"] = q;
        block.settings["p"] = p;
        block.settings["r"] = r;
        block.settings["show_trajectory"] = showTrajectory;
        block.settings["3D_Mode"] = third_dimension;
	}
}