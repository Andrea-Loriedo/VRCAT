using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticsController : MonoBehaviour
{
    public IEnumerator vibrateLoopRoutine;

    void Start()
    {
        vibrateLoopRoutine = VibrateLoop(0.1f, false);
    }

    public IEnumerator Vibrate(float delayTime)
    {
        OVRInput.SetControllerVibration (1, 1, OVRInput.Controller.RTouch);
        yield return new WaitForSeconds(delayTime);
        OVRInput.SetControllerVibration (0, 0, OVRInput.Controller.RTouch);
    }

    public IEnumerator VibrateLoop(float delayTime, bool vibrate)
    {
        while(vibrate)
        {
            OVRInput.SetControllerVibration (1, 1, OVRInput.Controller.RTouch);
            yield return new WaitForSeconds(delayTime);
            OVRInput.SetControllerVibration (0, 0, OVRInput.Controller.RTouch);
            yield return new WaitForSeconds(delayTime);
        }

        while(!vibrate)
        {
            OVRInput.SetControllerVibration (0, 0, OVRInput.Controller.RTouch);
        }

        yield return null;
    }

    public void StopVibration()
    {
        OVRInput.SetControllerVibration (0, 0, OVRInput.Controller.RTouch);
    }
}
