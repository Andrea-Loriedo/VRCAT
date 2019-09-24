using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class AimingTargetController : MonoBehaviour
{
    [SerializeField] AimingFeedbackController feedback;
    TargetStatus status;

    // Objects
    Collider sphereCollider;
    MeshRenderer sphereMesh;
    ParticleSystem orb;
    AudioSource audioData;

    // Scripts 
    HapticsController haptics;

    // Coroutines
    IEnumerator TargetEnterRoutine;

    //UXF
    [SerializeField] Session session;

    // sphere speed in units per second, can be set from the inspector view
    [SerializeField] float speed = 1f; 

    bool onTarget;

    private void Awake()
    {
        haptics = GetComponent<HapticsController>();
        sphereCollider = GetComponent<SphereCollider>();
        sphereMesh = GetComponent<MeshRenderer>();
        audioData = GetComponent<AudioSource>();
        orb = GetComponentInChildren<ParticleSystem>();
        TurnOff();
        onTarget = false;
    }

    void Update()
    {
        CheckTargetStatus();
        feedback.ShowFeedback(status);
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        TargetEnterRoutine = TargetEnter(0.5f); // move the target after triggering it for over 0.5 seconds
        StartCoroutine(haptics.Vibrate(0.2f));
        StartCoroutine(TargetEnterRoutine);
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(TargetEnterRoutine);
    }

    void CheckTargetStatus()
    {
        status = onTarget ? TargetStatus.Hit : TargetStatus.Miss;
    }

    void MoveToNextPoint()
    {
        // fetch the target 3D position for the next trial
        float x = (float)session.nextTrial.settings["target_x"];
        float y = (float)session.nextTrial.settings["target_y"];
        float z = (float)session.nextTrial.settings["target_z"];
        
        // move target to the next position
        StartCoroutine(MoveToNextPosition(new Vector3(x, y, z)));
    }

    public void MoveToStartPosition()
    {
        // fetch the initial position for the current block
        float x = (float)session.nextTrial.block.settings["start_x"];
        float y = (float)session.nextTrial.block.settings["start_y"];
        float z = (float)session.nextTrial.block.settings["start_z"];

        // move target to its initial position
        transform.localPosition = new Vector3(x, y, z);
    }

    IEnumerator TargetEnter(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // wait for the input delay before moving the target

        // before first trial
        if (session.currentTrialNum == 0)
        {
            // manually move to the first trial start point
            MoveToNextPoint();
        }
        else if (session.currentTrial == session.lastTrial)
        {
            session.EndCurrentTrial();
        }
        else if (session.nextTrial.numberInBlock == 1 && !session.inTrial)
        {
            MoveToNextPoint();
        }
        else
        {
            session.EndCurrentTrial();
        }
    }

    IEnumerator MoveToNextPosition(Vector3 nextPosition)
    {
        audioData.Play(0);
        
        onTarget = true;
        // disable collider so that sphere can't be hit while moving
        sphereCollider.enabled = false;

        // start the next trial
        session.BeginNextTrial();

        Vector3 currPos = transform.localPosition; // current target position
        Vector3 diff = nextPosition - currPos; // 3D distance from the next target position

        // while the target hasn't reached the next position, move towards it in a straight line
        while (diff.magnitude > 0.001)
        {
            transform.localPosition = Vector3.MoveTowards(currPos, nextPosition, speed * Time.deltaTime);
            currPos = transform.localPosition; // update current target position
            diff = nextPosition - currPos;

            yield return null; // continue running next frame
        }
        // enable collider so that sphere can be hit again
        sphereCollider.enabled = true;
        onTarget = false;
    }

    public void EndBehaviour(Trial endedTrial)
    {
        if (endedTrial == session.lastTrial)
        {
            // end session if the last trial has just ended
            session.End();
        }
        else if (session.nextTrial.numberInBlock == 1)
        {
            TurnOff(); // disable the target on trial 0
            MoveToStartPosition(); // move the target to its relative initial position
        }
        else
        {
            MoveToNextPoint(); // move the target to the next trial position
        }
    }
}

