using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public int checkpointNumber = 0; //Keeps track of what checkpoint the player is currently on in the level.
    public bool playerHasDiedOnce = false;
    public int timesHit = 0;

    public float timer = 1001f;
    public bool dontResetTimer = false;
    public bool endTimer = false;
    public bool finishedBeforeTimeLimit = true;

    void Awake()
    {
        if (instance == null)
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
