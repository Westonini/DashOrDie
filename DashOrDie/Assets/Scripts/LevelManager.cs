using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public int checkpointNumber = 0; //Keeps track of what checkpoint the player is currently on in the level.
    public bool playerHasDiedOnce = false; //Keeps track of if the player has died once during their playthrough of the level.
    public int timesHit = 0; //Keeps track of how many times the player has been hit during the level.

    public float timer = 1001f; //A timer that is set by the LevelTimerScript.
    public bool dontResetTimer = false; //This variable is used to ensure that if a player dies and the scene restarts, the timer wont restart as well.
    public bool endTimer = false; //Ends the timer if-statement in Update()
    public bool finishedBeforeTimeLimit = true; //Tracks if the player finished before the timer ran out.

    public int playerHealth = 3; //This int is used to carry over health from room-to-room.


    void Awake()
    {
        if (instance == null) //Makes the object not destroy itself when loading or restarting a scene.
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (endTimer != true && timer <= 1000) //Timer counts down until it reaches 0 or below. If it does reach 0 or below, set the finishedBeforeTimeLimit bool to false;
        {
            timer -= Time.deltaTime;

            if (timer <= 0.0f)
            {
                endTimer = true;
                finishedBeforeTimeLimit = false;
            }
        }
    }
}
