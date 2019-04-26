using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private HealthScript HS;

    // Start is called before the first frame update
    void Awake()
    {
        HS = GameObject.Find("Diamonds/Hitpoints").GetComponent<HealthScript>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && HS.Health < 3) //If the pickup object touches the player and the player's hp is less than 3...
        {
            if (HS.Health == 2)
            {
                HS.firstDiamondAnim.SetBool("1stDiamondLost", false);
                HS.firstDiamondAnim.SetBool("1stDiamondGained", true);
            }
            else if (HS.Health == 1)
            {
                HS.secondDiamondAnim.SetBool("2ndDiamondLost", false);
                HS.secondDiamondAnim.SetBool("2ndDiamondGained", true);
            }

            FindObjectOfType<AudioManagerScript>().Play("Pickup");
            HS.Health += 1;
            Destroy(transform.parent.gameObject);
        }
    }
}
