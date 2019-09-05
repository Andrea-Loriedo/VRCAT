using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        Vector3 newPos = target.position;
        transform.position = newPos;        
    }
}
