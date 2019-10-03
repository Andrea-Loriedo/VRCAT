using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedHeight : MonoBehaviour
{

    public Transform target;


    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Vector3 newPos = target.position;
        newPos.y = transform.parent.position.y;
        transform.position = newPos;        
    }

}
