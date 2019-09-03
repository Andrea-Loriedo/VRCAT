using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using UnityEngine.Events;

// [RequireComponent(typeof(AudioSource))]

public class SphereController : MonoBehaviour
{
    // AudioSource audioData;
    Collider sphereCollider;
    MeshRenderer sphereMesh;

    private ParticleSystem orb;

    // Coroutines
    IEnumerator waitThenMoveRoutine;

    // Materials
    // public Material greenGlass;
    // public Material darkerGlass;

    //UXF
    public Session session;

    public float speed = 1f; //sphere speed in units per second, can be set from the inspector view

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereMesh = GetComponent<MeshRenderer>();
        // audioData = GetComponent<AudioSource>();
        gameObject.SetActive(true);
        orb = GetComponentInChildren<ParticleSystem>();
        Pause();
    }

    public void Pause()
    {
        sphereCollider.enabled = false;
        sphereMesh.enabled = false;
        TurnOff();
    }

    public void Resume()
    {
        sphereCollider.enabled = true;
        sphereMesh.enabled = true;
        TurnOn();
    }


    public void TurnOn()
    {
        gameObject.SetActive(true);
        sphereCollider.enabled = true;
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("Trigger enter");
        // Debug.LogFormat("Sphere hit by {0}", other.name);
        waitThenMoveRoutine = WaitThenMove(0.5f);
        StartCoroutine(waitThenMoveRoutine);
        // audioData.Play(0);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.LogFormat("Trigger exit");
        StopCoroutine(waitThenMoveRoutine);
        // sphereMesh.material = darkerGlass;
        orb.startColor = new Color(255, 0, 5, 255);
    }


    IEnumerator WaitThenMove(float delayTime)
    {
        // sphereMesh.material = greenGlass;
        orb.startColor = new Color(3, 255, 0, 255);
        yield return new WaitForSeconds(delayTime);
        // sphereMesh.material = darkerGlass;
        orb.startColor = new Color(255, 0, 5, 255);

        // before first trial
        if (session.currentTrialNum == 0)
        {
            // manually move to the first trial location
            MoveToNextTrialLocation();
        }
        else if (session.currentTrial == session.lastTrial)
        {
            Debug.Log("End current trial: Last in session");
            session.EndCurrentTrial();
        }
        else if (session.nextTrial.numberInBlock == 1 && !session.inTrial)
        {
            MoveToNextTrialLocation();
        }
        else
        {
            Debug.Log("End current trial");
            session.EndCurrentTrial();
        }
    }


    IEnumerator MoveTowardsLocation(Vector3 destination)
    {
        // disable collider so that sphere can't be hit while moving
        sphereCollider.enabled = false;

        // start the next trial
        session.BeginNextTrial();
        Debug.LogFormat("Starting trial: {0}", session.currentTrialNum);

        Vector3 currPos = transform.localPosition;
        Vector3 diff = destination - currPos;

        while (diff.magnitude > 0.001)
        {
            transform.localPosition = Vector3.MoveTowards(currPos, destination, speed * Time.deltaTime);
            currPos = transform.localPosition;
            diff = destination - currPos;
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
            Debug.LogFormat("Move to starting point (SCTRL)");
            MoveToStartingPoint();
        }
        else
        {
            MoveToNextTrialLocation();
        }
    }

    void MoveToNextTrialLocation()
    {
        float x = (float)session.nextTrial.settings["target_x"];
        float y = (float)session.nextTrial.settings["target_y"];
        float z = (float)session.nextTrial.settings["target_z"];
        
        StartCoroutine(MoveTowardsLocation(new Vector3(x, y, z)));
    }

    public void MoveToStartingPoint()
    {
        float x = (float)session.nextTrial.block.settings["start_x"];
        float y = (float)session.nextTrial.block.settings["start_y"];
        float z = (float)session.nextTrial.block.settings["start_z"];

        transform.localPosition = new Vector3(x, y, z);
    }
}

