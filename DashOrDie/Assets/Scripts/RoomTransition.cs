using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    private TransitionScript TS;

    public GameObject currentRoom;
    public GameObject nextRoom;

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
            TS.BeginTransitionOut();
            Invoke("RoomChange", 3);
        }
    }

    void RoomChange()
    {
        currentRoom.SetActive(false);
        nextRoom.SetActive(true);
        TS.BeginTransitionIn();
    }
}
