using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalColliderController : MonoBehaviour
{
    [SerializeField] TrackingFeedbackController feedback;

    Status status;

    bool onTarget;

    // Start is called before the first frame update
    void Start()
    {
        onTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        feedback.ShowFeedback(status);
    }

    void OnTriggerEnter()
    {
        onTarget = true;
    }

    void OnTriggerExit()
    {
        onTarget = false;
    }

    void CheckDistance()
    {
        status = onTarget ? Status.Close : Status.Far;
    }
}

