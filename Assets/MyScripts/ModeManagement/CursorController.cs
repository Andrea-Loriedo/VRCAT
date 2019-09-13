using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] AimingResultsController results;
    Rigidbody rb;
    public Transform target;
    Vector3 currPos;
    Vector3 prevPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        prevPos = transform.position;
    }

    void FixedUpdate()
    {
        currPos = transform.position;
        // try
        //     {
        //         if(rb != null)
        //         results.RecordVelocity(rb);
        //     }
        //     catch (System.FormatException e)
        //     {
        //         ;
        //     }
        results.RecordVelocity(currPos, prevPos);
        prevPos = currPos;
    }

    void Update()
    {
        Vector3 newPos = target.position;
        transform.position = newPos;        
    }
}
