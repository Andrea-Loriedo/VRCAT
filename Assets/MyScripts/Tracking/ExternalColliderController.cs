using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalColliderController : MonoBehaviour
{
    [SerializeField] TrackingFeedbackController feedback;

    Status status;

    // Scripts
    HapticsController haptics;

    bool onTarget;

    // Start is called before the first frame update
    void Start()
    {
        haptics = GetComponent<HapticsController>();
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
        StartCoroutine(haptics.VibrateLoop(0.1f, true));
    }

    void OnTriggerExit()
    {
        onTarget = false;
        StopAllCoroutines();
        haptics.StopVibration();
    }

    void CheckDistance()
    {
        status = onTarget ? Status.Close : Status.Far;
    }
}

