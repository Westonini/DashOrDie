﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSave : MonoBehaviour
{
    private LevelManager LM;

    void Awake()
    {
        try //Try to get the LevelManager script from an object with a tag "LM". Otherwise set LM to null.
        {
            LM = GameObject.FindWithTag("LM").GetComponent<LevelManager>();
        }
        catch
        {
            LM = null;
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision) //If the player touches a GameObject with a "CheckpointX" tag, change the checkpointNumber in the CheckpointManager Script.
    {
        if (collision.tag == "Checkpoint1")
        {
            LM.checkpointNumber = 1;
        }
        if (collision.tag == "Checkpoint2")
        {
            LM.checkpointNumber = 2;
        }
        if (collision.tag == "Checkpoint3")
        {
            LM.checkpointNumber = 3;
        }
        if (collision.tag == "Checkpoint4")
        {
            LM.checkpointNumber = 4;
        }
        if (collision.tag == "Checkpoint5")
        {
            LM.checkpointNumber = 5;
        }
        if (collision.tag == "LevelFinish") //Resets various things in the LevelManager Script if the player reaches the end of the level.
        {
            LM.checkpointNumber = 0;
            LM.endTimer = true;
            LM.dontResetTimer = false;
            LM.timer = 1001f;
        }
    }


}
