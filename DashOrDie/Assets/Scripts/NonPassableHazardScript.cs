using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPassableHazardScript : MonoBehaviour
{
    private PlayerController PC;

    private HealthScript HS;

    private float knockbackTime = 0.475f;

    public bool nonPassableRecovery = false;

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
                knockbackTime = 0.475f;
                PC.GetComponent<PlayerController>().enabled = true;
            }
        }
    }

    //If the player gets hit by a hazard (while not recovering, and health is greater than 0) they will get knocked back, their dash will end, and set the playerHurt bool on the HealthScript to true.
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && HS.recovery2 == false && HS.recovery == false && HS.Health > 0)
        {
            HS.playerHurt = true;
            nonPassableRecovery = true;
            EndDash();
            gettingKnockedback = true;

            if (PC.facingRight == true && PC.horizontalInput > 0)
            {
                PC.rb.velocity = new Vector2(-2.5f, 8);

            }

            else if (PC.facingRight == false && PC.horizontalInput < 0)
            {
                PC.rb.velocity = new Vector2(2.5f, 8);

            }

            else
            {
                PC.rb.velocity = new Vector2(0.5f, 8);

            }

        }
    }

    void EndDash() //Ends dash early
    {
        PC.dashIsActive = false;
        PC.dashIsHorizontal = false;
        PC.dashTimeElapsed = 0f;
        PC.trail.SetActive(false);
        PC.trailR.Clear();
        PC.trail2.SetActive(false);
        PC.trailR2.Clear();
        PC.animator.SetBool("IsDashing", false);
        PC.animator.SetBool("IsDashingDown", false);
        PC.animator.SetBool("IsDashingUp", false);
        PC.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}


