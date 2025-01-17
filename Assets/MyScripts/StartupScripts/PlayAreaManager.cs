﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UXF;

public class PlayAreaManager : MonoBehaviour {
    
	public float refHeight = 1.6f;
	string defaultHand = "Right";
	
	public GameObject cursor;
	public Transform cursorTransform;
	public Transform leftController;
	public CursorController controller;
	public HapticsController haptics;
	TrailRenderer cursorTrail;
	Scene currentScene;
	string sceneName;

	void Start ()
	{
		cursorTrail = cursor.GetComponent<TrailRenderer>();
		currentScene = SceneManager.GetActiveScene();
		sceneName = currentScene.name;
	}

	public void ScaleWorkspace(Session session)
	{
		try
		{
		    cursorTrail.enabled = false;
		}
	    catch (UnityEngine.MissingComponentException)
		{

		}

		float height;

		try
		{
			height = System.Convert.ToSingle(session.participantDetails["Height"]);
			Debug.LogFormat("Height set to participant eye level = {0}", height);
		}
	    catch (System.NullReferenceException)
		{
			// during quick start mode, there are no participant details, so we get null reference exception
			// set the participant height to refHeight in quick start mode
			Debug.LogFormat("Height set to default {0}", refHeight);
			height = refHeight;
		}		


       float ratio = height / refHeight;
	   float workspaceCenterRelativeHeight = 0.9f;

	   transform.localScale = new Vector3(ratio, ratio, ratio);
	   transform.localPosition = new Vector3(0f, workspaceCenterRelativeHeight * height, 0f);

	   cursorTransform.localScale *= ratio;
	   
	   // resize cursorTrail

		try
		{
			cursorTrail.enabled = true;
		}
	    catch (UnityEngine.MissingComponentException)
		{
			
		}
    }

	public void RepositionPlayArea(Session session)
	{
		float height;

		try
		{
			height = System.Convert.ToSingle(session.participantDetails["Height"]);
			Debug.LogFormat("Height set to participant eye level = {0}", height);
		}
	    catch (System.NullReferenceException)
		{
			// during quick start mode, there are no participant details, so we get null reference exception
			// set the participant height to refHeight in quick start mode
			Debug.LogFormat("Height set to default {0}", refHeight);
			height = refHeight;
		}		


       float ratio = height / refHeight;
	   float workspaceCenterRelativeHeight = 0.8f;

	   transform.localPosition = new Vector3(0f, workspaceCenterRelativeHeight * height, 0f);

    }

	public void CheckForHandSwap(Session session)
	{
		string hand;

		try
		{
			hand = System.Convert.ToString(session.participantDetails["Writing hand"]);
		}
	    catch (System.NullReferenceException)
		{
			// during quick start mode, there are no participant details, so we get null reference exception
			// set the participant handedness to "defaultHand" in quick start mode
			Debug.LogFormat("Handedness set to default: {0} handed", defaultHand);
			hand = defaultHand;
		}	

		if(hand == "Left")
	    {
		    SwapHands(leftController);
	    }	

		haptics.SetController(hand);
	}

	void SwapHands(Transform newParent)
    {
        // Sets "newParent" as the new parent of the cursor GameObject. Makes the cursor keep its local orientation rather than its global orientation.
        // cursorTransform.transform.SetParent(newParent, false);

		if (sceneName == "InterceptiveTimingScene") 
		{
			FixedHeight fixedHeight = GetComponentInChildren<FixedHeight>();
		    fixedHeight.target = newParent;
		}
		else
		{
			controller.target = newParent;
		}

    }

	public void TurnOff()
	{
		gameObject.SetActive(false);
	}
}