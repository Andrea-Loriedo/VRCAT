using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRInput;

public class HapticsController : MonoBehaviour
{
    public IEnumerator vibrateLoopRoutine;
    
    static Controller currentController;

    string defaultHand = "Right";

    void Start()
    {
        vibrateLoopRoutine = VibrateLoop(0.1f, false);
    }

    public IEnumerator Vibrate(float delayTime)
    {
        OVRInput.SetControllerVibration (1, 1, currentController);
        yield return new WaitForSeconds(delayTime);
        OVRInput.SetControllerVibration (0, 0, currentController);
    }

    public IEnumerator VibrateLoop(float delayTime, bool vibrate)
    {
        while(vibrate)
        {
            OVRInput.SetControllerVibration (1, 1, currentController);
            yield return new WaitForSeconds(delayTime);
            OVRInput.SetControllerVibration (0, 0, currentController);
            yield return new WaitForSeconds(delayTime);
        }

        while(!vibrate)
        {
            OVRInput.SetControllerVibration (0, 0, currentController);
        }

        yield return null;
    }

    public void StopVibration()
    {
        OVRInput.SetControllerVibration (0, 0, currentController);
    }

    public void SetController(string hand)
    {
		if(hand == "Left")
	    {
		    currentController = OVRInput.Controller.LTouch;
	    }	
        else
        {
            currentController = OVRInput.Controller.RTouch;
        }
    }
}
