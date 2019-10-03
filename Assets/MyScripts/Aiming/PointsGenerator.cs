using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UXF;

public class PointsGenerator : MonoBehaviour {

    [SerializeField] FileIOManager fiom;
    [SerializeField] InputFieldManager inputFieldPrefab;
    Session session;

    // pentagram
    float _penta_x;
    float _penta_y;
    float _angle = 90;
    float speed = 1f;
    float size = 1f; 
    bool feedback = true;
    bool stats = true;
    public float radius = 0.3f;
    public int trials;

    // convert from polar to cartesian coordinates
    void ConvertToCartesian(float r, float angle)
    {
        r = radius;
        angle = _angle;

        _penta_x = r * Mathf.Cos(angle);
        _penta_y = r * Mathf.Sin(angle);
    }

    [ContextMenu("Generate new points")]
    
    public void GenerateNewPoints()
    {   
        // dictionary to store the lists of points for each block
        var dict = new Dictionary<string, object>();

        Vector3 block1Start = new Vector3(-0.1344248f, 0.2681976f, 0f);
        Vector3 block2Start = new Vector3(-0.09209739f, 0.07442875f, 0f);
        Vector3 block3Start = new Vector3(-0.1430611f, 0.07717561f, -0.114103f);

        var startPts = new List<List<float>>();

        startPts.Add(
            new List<float>()
            {
                block1Start.x, block1Start.y, block1Start.z
            }
        );

        startPts.Add(
            new List<float>()
            {
                block2Start.x, block2Start.y, block2Start.z
            }
        );

        startPts.Add(
            new List<float>()
            {
                block3Start.x, block3Start.y, block3Start.z
            }
        );

        // Default to 3 trials per block on an empty inputfield
        trials = string.IsNullOrEmpty(inputFieldPrefab.SetTrialsPerBlock()) ? 3 : int.Parse(inputFieldPrefab.SetTrialsPerBlock());

        var pts1 = new List<List<float>>();
        // pentagram 2D pattern 
        for (int i = 0; i < trials; i++)
        {
            _angle += 2 * ((2 * Mathf.PI)/5); 

            ConvertToCartesian(radius, _angle);

            pts1.Add(
                new List<float>()
                {
                    _penta_x, _penta_y, 0f
                }
            );
        }

        var pts2 = new List<List<float>>();
        
        // random 2D pattern  
        for (int i = 0; i < trials; i++)
        {
            Vector2 randomPos = 0.3f * Random.insideUnitCircle;
            pts2.Add(
                new List<float>()
                {
                    randomPos.x, randomPos.y, 0f
                }
            );
        }

        var pts3 = new List<List<float>>();
        // random 3D pattern  
        for (int i = 0; i < trials; i++)
        {
            Vector3 randomPos = 0.3f * Random.insideUnitSphere;
            pts3.Add(
                new List<float>()
                {
                    randomPos.x, randomPos.y, randomPos.z
                }
            );
        }
    
        // add points for each block to dictionary
        dict.Add("target_speed", speed);
        dict.Add("target_size", size);
        dict.Add("show_feedback", feedback);
        dict.Add("show_stats", stats);
        dict.Add("starting_points", startPts);
        dict.Add("block1_points", pts1);
        dict.Add("block2_points", pts2);
        dict.Add("block3_points", pts3);

        var fileInfo = new WriteFileInfo()
        {   // path to coordinates file
            basePath = Application.streamingAssetsPath,
            paths = new string[] { "aiming_settings.json" }
        };

        // convert dictionaries to json and write to file "generated_points.json"
        fiom.WriteJson(dict, fileInfo);

    }
}
