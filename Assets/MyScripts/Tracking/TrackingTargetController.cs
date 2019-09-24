using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;
using System;

[RequireComponent(typeof(AudioSource))]

public class TrackingTargetController : MonoBehaviour
{
    [SerializeField] TrajectoryController trajectory;

    // Experiment management
    [SerializeField] TrackingExperimentManager experiment;

    // UXF
    [SerializeField] Session session;
    
    // Objects
    Collider sphereCollider;
    MeshRenderer sphereRenderer;
    ParticleSystem[] orb;
    AudioSource audioData;

    // Constants
    static float twoPI = Mathf.PI * 2f;
    static float PIovertwo = Mathf.PI / 2f;

    // Booleans
    bool taskStarted = false;

    void Awake()
    {
        sphereCollider = GetComponent<Collider>();
        sphereRenderer = GetComponent<MeshRenderer>();
        audioData = GetComponent<AudioSource>();
        orb = GetComponentsInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!taskStarted)
        {
            StartCoroutine(MoveAfterDelay(session.nextTrial.block));
        }
    
        ChangeTargetColour("green");
    }

    private void OnTriggerExit(Collider other)
    {
        ChangeTargetColour("red");
    }

    IEnumerator MoveAfterDelay(Block block)
    {
        taskStarted = true;

        yield return new WaitForSeconds(1f);

        foreach (Trial trial in session.trials)
        {
            experiment.StartNextTrial();
            
            float t = 0f;
            
            while (t < twoPI) 
            {  
                if(experiment.settings.thirdDimension)
                {
                    Vector3 newPos = trajectory.Coordinates3D(experiment.settings.input, t);
                    transform.localPosition = newPos;
                }
                else
                {
                    Vector3 newPos = trajectory.Coordinates2D(experiment.settings.input, t);
                    transform.localPosition = newPos;
                }

                t += Time.deltaTime * experiment.settings.speed;
                yield return null;
            }

            trial.End();
            ResetTargetPosition(0f);
        }
    }
 
    void ChangeTargetColour(string colour)
    {
        switch(colour)
        {
            case "red":
                foreach (ParticleSystem ps in orb) {
                    ps.startColor = new Color(255, 0, 5, 255);;
                }
                break;

            case "green":
                foreach (ParticleSystem ps in orb) {
                    ps.startColor = new Color(3, 255, 0, 255);
                }
                break;
        }
    }

    void ResetTargetPosition(float position)
    {
        audioData.Play(0);
        
        if(experiment.settings.thirdDimension)
        {
            transform.localPosition = trajectory.Coordinates3D(experiment.settings.input, position);
        }
        else
        {
            transform.localPosition = trajectory.Coordinates2D(experiment.settings.input, position);
        }
    }

    public void EndBehaviour(Trial endedTrial)
    {
        if (endedTrial == session.lastTrial)
        {
            session.End();
        }
    }
}


