using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UXF;

public class SessionGenerator : MonoBehaviour
{   
    public void GenerateExperiment(Session session)
    {
        int numTrials = session.settings.GetInt("trials_per_block");

        //  Retrieve settings for each block from .json file
        Dictionary<string, object> Block1Settings = (Dictionary<string, object>)session.settings["block_1_settings"];
        Dictionary<string, object> Block2Settings = (Dictionary<string, object>)session.settings["block_2_settings"];
        Dictionary<string, object> Block3Settings = (Dictionary<string, object>)session.settings["block_3_settings"];
        Dictionary<string, object> Block4Settings = (Dictionary<string, object>)session.settings["block_4_settings"];

        // Create the experiment blocks
        Block block1 = session.CreateBlock(numTrials); // Block 1 (stationary target, close)
        Block block2 = session.CreateBlock(numTrials); // Block 2 (moving target, close)
        Block block3 = session.CreateBlock(numTrials); // Block 3 (stationary target, far)
        Block block4 = session.CreateBlock(numTrials); // Block 4 (moving target, far)

        // Assign the relevant settingsto each block
        AssignBlockSettings(Block1Settings, block1);
        AssignBlockSettings(Block2Settings, block2);
        AssignBlockSettings(Block3Settings, block3);
        AssignBlockSettings(Block4Settings, block4);
    }

    void AssignBlockSettings(Dictionary<string, object> settings, Block block)
    {
	// 	string distance = System.Convert.ToString(settings["distance"]); // Either "Close" or "Far"
	// 	string mode = System.Convert.ToString(settings["target_mode"]);; // Either "Still" or "Move"

	// 	block.settings["distance"] = distance;
	// 	block.settings["target_mode"] = mode;
	}
}