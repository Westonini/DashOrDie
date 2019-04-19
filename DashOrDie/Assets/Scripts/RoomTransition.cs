using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    private TransitionScript TS;

    public GameObject currentRoom;
    public GameObject nextRoom;
    public GameObject levelCompleteUI;
    public GameObject levelCompleteCamera;

    void Awake()
    {
        try
        {
            TS = GameObject.Find("GameplayCanvas").GetComponent<TransitionScript>();
        }
        catch
        {
            TS = null;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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

    void RoomChange()
    {
        currentRoom.SetActive(false);
        nextRoom.SetActive(true);
        TS.BeginTransitionIn();
    }

    void TurnOnLevelCompleteUI()
    {
        levelCompleteUI.SetActive(true);
        levelCompleteCamera.SetActive(true);
        currentRoom.SetActive(false);
        FindObjectOfType<AudioManagerScript>().Play("Win");
    }
}
