using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    private PlayerController PCScript;

    private HealthScript HS;


    // Start is called before the first frame update
    void Start()
    {
        PCScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        HS = GameObject.Find("Diamonds/Hitpoints").GetComponent<HealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && PCScript.dashIsActive == false && HS.recovery == false)
        {
            if (HS.Health > 0)
            {
                FindObjectOfType<AudioManagerScript>().Play("Hurt");
                HS.recovery = true;
                HS.Health -= 1;
            }

        }
    }
}
