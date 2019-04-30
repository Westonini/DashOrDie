using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimerScript : MonoBehaviour
{
    public int timerInSeconds = 0; //Change this in the inspector in each scene to change the duration of the timer (in seconds).

    private LevelManager LM;

    void Awake()
    {
        //Get the LevelManager Script from the LM tagged object
        try
        {
            LM = GameObject.FindWithTag("LM").GetComponent<LevelManager>();
        }
        catch
        {
            LM = null;
        }
    }
    void Start()
    {
        if (LM.dontResetTimer != true)
        {
            LM.endTimer = false;

            if (timerInSeconds >= 1) //If the timer is 1 or greater, set the timer to the given time.
            {
                LM.timer = timerInSeconds;
                LM.dontResetTimer = true;
            }

            else //If the timer is below 1, end the timer.
            {
                LM.endTimer = true;
            }
        }
    }
}
