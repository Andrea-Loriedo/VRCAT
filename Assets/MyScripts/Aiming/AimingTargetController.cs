using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using UnityEngine.Events;

// [RequireComponent(typeof(AudioSource))]

public class AimingTargetController : MonoBehaviour
{
    // AudioSource audioData;

    // Objects
    Collider sphereCollider;
    MeshRenderer sphereMesh;
    ParticleSystem orb;

    // Coroutines
    IEnumerator MoveTargetRoutine;

    //UXF
    public Session session;

    [SerializeField] float speed = 1f; // sphere speed in units per second, can be set from the inspector view

    private void Awake()
    {
        // audioData = GetComponent<AudioSource>();
        sphereCollider = GetComponent<SphereCollider>();
        sphereMesh = GetComponent<MeshRenderer>();
        orb = GetComponentInChildren<ParticleSystem>();
        TurnOff();
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
        MoveTargetRoutine = MoveTarget(0.5f); // move the target after triggering it for over 0.5 seconds
        StartCoroutine(MoveTargetRoutine);
        // audioData.Play(0);
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(MoveTargetRoutine);
    }

    IEnumerator MoveTarget(float delayTime)
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
        // disable collider so that sphere can't be hit while moving
        sphereCollider.enabled = false;

        // start the next trial
        session.BeginNextTrial();
        Debug.LogFormat("Starting trial: {0}", session.currentTrialNum);

        Vector3 currPos = transform.localPosition;
        Vector3 diff = nextPosition - currPos;

        while (diff.magnitude > 0.001)
        {
            transform.localPosition = Vector3.MoveTowards(currPos, nextPosition, speed * Time.deltaTime);
            currPos = transform.localPosition;
            diff = nextPosition - currPos;
            // Debug.LogFormat("New position = {0}. Diff is {1}", currPos, diff.magnitude);
            yield return null; // continue running next frame
        }

        // enable collider so that sphere can be hit again
        sphereCollider.enabled = true;
    }

    public void EndBehaviour(Trial endedTrial)
    {
        if (endedTrial == session.lastTrial)
        {
            session.End();
        }
        else if (session.nextTrial.numberInBlock == 1)
        {
            TurnOff();
            MoveToStartPosition();
        }
        else
        {
            MoveToNextPoint();
        }
    }

    void MoveToNextPoint()
    {
        float x = (float)session.nextTrial.settings["target_x"];
        float y = (float)session.nextTrial.settings["target_y"];
        float z = (float)session.nextTrial.settings["target_z"];
        
        StartCoroutine(MoveToNextPosition(new Vector3(x, y, z)));
    }

    public void MoveToStartPosition()
    {
        float x = (float)session.nextTrial.block.settings["start_x"];
        float y = (float)session.nextTrial.block.settings["start_y"];
        float z = (float)session.nextTrial.block.settings["start_z"];

        transform.localPosition = new Vector3(x, y, z);
    }
}

