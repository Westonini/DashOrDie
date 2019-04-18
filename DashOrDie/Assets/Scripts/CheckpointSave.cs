using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSave : MonoBehaviour
{
    private CheckpointManager CM;

    void Awake()
    {
        try
        {
            CM = GameObject.FindWithTag("CM").GetComponent<CheckpointManager>();
        }
        catch
        {
            CM = null;
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision) //If the player touches a GameObject with a "CheckpointX" tag, change the checkpointNumber in the CheckpointManager Script.
    {
        if (collision.tag == "Checkpoint1")
        {
            CM.checkpointNumber = 1;
        }
        if (collision.tag == "Checkpoint2")
        {
            CM.checkpointNumber = 2;
        }
        if (collision.tag == "Checkpoint3")
        {
            CM.checkpointNumber = 3;
        }
        if (collision.tag == "Checkpoint4")
        {
            CM.checkpointNumber = 4;
        }
        if (collision.tag == "Checkpoint5")
        {
            CM.checkpointNumber = 5;
        }
    }


}
