using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    private PlayerController PC;

    private HealthScript HS;

    private float knockbackTime = 0.38f;

    [HideInInspector]
    public bool gettingKnockedback;

    // Start is called before the first frame update
    void Start()
    {
        PC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        HS = GameObject.Find("Diamonds/Hitpoints").GetComponent<HealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gettingKnockedback == true) //player can't move while being knockedback.
        {
            PC.GetComponent<PlayerController>().enabled = false;
            knockbackTime -= Time.deltaTime;

            if (knockbackTime <= 0.0f)
            {
                gettingKnockedback = false;
                knockbackTime = 0.38f;
                PC.GetComponent<PlayerController>().enabled = true;
            }
        }
    }

    //If the player gets hit by a hazard (while not dashing, not recovering, and health is greater than 0) they will get knocked back and set the playerHurt bool on the HealthScript to true.
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && PC.dashIsActive == false && HS.recovery == false && HS.Health > 0)
        { 
            HS.playerHurt = true;

            if (PC.facingRight == true && PC.horizontalInput > 0)
            {
                gettingKnockedback = true;
                PC.rb.velocity = new Vector2(-2, 6);

            }
            else if (PC.facingRight == false && PC.horizontalInput < 0)
            {
                gettingKnockedback = true;
                PC.rb.velocity = new Vector2(2, 6);
            }
            else
            {
                gettingKnockedback = true;
                PC.rb.velocity = new Vector2(0.5f, 6);
            }
        }
    }
}

