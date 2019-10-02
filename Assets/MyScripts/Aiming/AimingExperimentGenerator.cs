using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class AimingExperimentGenerator : MonoBehaviour
{
    public void Generate(Session session)
    {
        var settings = new Dictionary<string, object>();

        settings.Add("target_speed", session.settings.GetFloat("target_speed"));
        settings.Add("target_size", session.settings.GetFloat("target_size"));
        settings.Add("show_feedback", session.settings.GetBool("show_feedback"));

        List<object> startingPoints = (List<object>)session.settings["starting_points"];
        List<object> pointsBlock1 = (List<object>)session.settings["block1_points"];
        List<object> pointsBlock2 = (List<object>)session.settings["block2_points"];
        List<object> pointsBlock3 = (List<object>)session.settings["block3_points"];

        // create 3 blocks
        Block block1 = session.CreateBlock();
        Block block2 = session.CreateBlock();
        Block block3 = session.CreateBlock();

        SetStartCoordinates((List<object>) startingPoints[0], block1);
        SetStartCoordinates((List<object>) startingPoints[1], block2);
        SetStartCoordinates((List<object>) startingPoints[2], block3);

        AssignBlockSettings(settings, block1);
        AssignBlockSettings(settings, block2);
        AssignBlockSettings(settings, block3);

        foreach (List<object> point in pointsBlock1)
        {
           SetBlockCoordinates(point, block1);
        }

        foreach (List<object> point in pointsBlock2)
        {
           SetBlockCoordinates(point, block2);
        }

        foreach (List<object> point in pointsBlock3)
        {
           SetBlockCoordinates(point, block3);
        }
    }

        void SetStartCoordinates(List<object> startPoint, Block block)
        {
            float x = System.Convert.ToSingle(startPoint[0]);
            float y = System.Convert.ToSingle(startPoint[1]);
            float z = System.Convert.ToSingle(startPoint[2]);

            block.settings["start_x"] = x;
            block.settings["start_y"] = y;
            block.settings["start_z"] = z;
            
        }

        void SetBlockCoordinates(List<object> point, Block block)
        {
            float x = System.Convert.ToSingle(point[0]);
            float y = System.Convert.ToSingle(point[1]);
            float z = System.Convert.ToSingle(point[2]);

            Trial newTrial = block.CreateTrial();

            newTrial.settings["target_x"] = x;
            newTrial.settings["target_y"] = y;
            newTrial.settings["target_z"] = z;
            
        }

        void AssignBlockSettings(Dictionary<string, object> settings, Block block)
        {
            float speed = System.Convert.ToSingle(settings["target_speed"]);
            float size = System.Convert.ToSingle(settings["target_size"]);
            bool feedback = (bool) settings["show_feedback"];
        
            block.settings["target_speed"] = speed;
            block.settings["target_size"] = size;
            block.settings["show_feedback"] = feedback;
	    }

}
