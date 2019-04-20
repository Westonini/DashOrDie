using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimerScript : MonoBehaviour
{
    public bool thirtySecondTimer = false;
    public bool oneMinuteTimer = false;
    public bool twoMinuteTimer = false;
    public bool threeMinuteTimer = false;

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
            if (thirtySecondTimer == true) //If the oneMinuteTimer bool is enabled in the inspector the timer will be set to 60 seconds.
            {
                LM.timer = 30;
                LM.dontResetTimer = true;
            }
            else if (oneMinuteTimer == true) //If the oneMinuteTimer bool is enabled in the inspector the timer will be set to 60 seconds.
            {
                LM.timer = 60;
                LM.dontResetTimer = true;
            }
            else if (twoMinuteTimer == true) //If the twoMinuteTimer bool is enabled in the inspector the timer will be set to 120 seconds.
            {
                LM.timer = 120;
                LM.dontResetTimer = true;
            }
            else if (threeMinuteTimer == true) //If the threeMinuteTimer bool is enabled in the inspector the timer will be set to 180 seconds.
            {
                LM.timer = 180;
                LM.dontResetTimer = true;
            }
            else
            {
                LM.endTimer = true;
            }
        }
    }
}
