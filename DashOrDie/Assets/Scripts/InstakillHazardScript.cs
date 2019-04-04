using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstakillHazardScript : MonoBehaviour
{
    private PlayerController PC;

    private HealthScript HS;

    // Start is called before the first frame update
    void Start()
    {
        PC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        HS = GameObject.Find("Diamonds/Hitpoints").GetComponent<HealthScript>();
    }

    //If the player gets hit by a hazard (while not dashing, not recovering, and health is greater than 0) they will get knocked back and set the playerHurt bool on the HealthScript to true.
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && PC.dashIsActive == false && HS.recovery == false && HS.Health > 0)
        {
            HS.playerInstakilled = true;
        }
    }
}

