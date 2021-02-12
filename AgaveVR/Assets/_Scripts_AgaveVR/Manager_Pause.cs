﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Pause : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //pause if in oculus home universal menu, and if headset not worn
        bool bPauseNow = (!OVRManager.hasInputFocus || !OVRManager.hasVrFocus) /*|| (XRDevice.userPresence!=UserPresenceState.Present)*/;

        Debug.Log("bPausenow: " + bPauseNow);

        if (bPauseNow)
        {
            Time.timeScale = 0.0f; //stops FixedUpdate

            //also need to stop all sound
            AudioListener.pause = true;
            //AudioStateMachine.instance.masterVolume = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;

            AudioListener.pause = false;
            //AudioStateMachine.instance.masterVolume = 1.35f;
        }

    }
}