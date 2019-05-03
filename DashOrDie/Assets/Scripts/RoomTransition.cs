using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    private TransitionScript TS;

    private HealthScript HS;

    private LevelManager LM;

    public GameObject currentRoom;
    public GameObject nextRoom;
    public GameObject levelCompleteUI;
    public GameObject levelCompleteCamera;



    void Awake()
    {
        //Try to get the TransitionScript script by locating the GameplayCanvas object.
        try
        {
            TS = GameObject.Find("GameplayCanvas").GetComponent<TransitionScript>();
        }
        catch
        {
            TS = null;
        }

        //Get the HealthScript by locating the Diamonds/Hitpoints object.
        try
        {
            HS = GameObject.Find("Diamonds/Hitpoints").GetComponent<HealthScript>();
        }
        catch
        {
            HS = null;
        }

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") //If there's something in "nextRoom" in the inspector, call BeginTransitionOut() from the TransitionScript then invoke RoomChange().
                                       //If there isn't something in "nextRoom" in the inspector, call BeginTransitionOut() from the TransitionScript then invoke TurnOnLevelCompleteUI().
        {
            if (nextRoom != null)
            {
                TS.BeginTransitionOut();
                Invoke("RoomChange", 3);               
            }
            else
            {
                TS.BeginTransitionOut();              
                Invoke("TurnOnLevelCompleteUI", 1.5f);
            }

        }
    }

    void RoomChange() //Disables the current room and enables the next room. Calls BeginTransitionIn() from the TransitionScript.
    {
        LM.playerHealth = HS.Health; //Once the player reaches the end of the room, set the playerHealth in the LevelManager Script to whatever the health currently is.
        currentRoom.SetActive(false);
        nextRoom.SetActive(true);
        TS.BeginTransitionIn();
    }

    void TurnOnLevelCompleteUI() //Disables the current room and enables the LevelCompleteUI and its Camera. Also plays a win sound.
    {
        LM.playerHealth = 3; //Once the player reaches the end of the level, set the playerHealth in the LevelManager Script to the default (3).
        levelCompleteUI.SetActive(true);
        levelCompleteCamera.SetActive(true);
        currentRoom.SetActive(false);
        FindObjectOfType<AudioManagerScript>().Play("Win");
    }
}
