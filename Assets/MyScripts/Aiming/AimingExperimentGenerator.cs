using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class AimingExperimentGenerator : MonoBehaviour
{
    public void Generate(Session session)
    {
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

}
