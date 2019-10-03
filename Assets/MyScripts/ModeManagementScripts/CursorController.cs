using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorController : MonoBehaviour
{
    // [SerializeField] AimingResultsController results;
    public Transform target;

    Vector3 currPos;
    Vector3 prevPos;

    Scene currentScene;
	string sceneName;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
		sceneName = currentScene.name;
        prevPos = transform.position;
    }

    public Vector3 GetPosition()
    {
        currPos = transform.position;
        return currPos;
    }

    void Update()
    {
        if ((sceneName != "InterceptiveTimingScene"))
        {
            Vector3 newPos = target.position;
            transform.position = newPos;
        }        
    }
}
