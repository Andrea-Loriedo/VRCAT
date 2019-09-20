using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    // [SerializeField] AimingResultsController results;
    public Transform target;
    Vector3 currPos;
    Vector3 prevPos;

    void Start()
    {
        prevPos = transform.position;
    }

    public Vector3 GetPosition()
    {
        currPos = transform.position;
        return currPos;
    }

    void Update()
    {
        Vector3 newPos = target.position;
        transform.position = newPos;        
    }
}
