using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLaser : MonoBehaviour
{
    public GameObject laser;
    public GameObject laserLight;
    public GameObject laserBlock;
    public GameObject laser2;
    public GameObject laserLight2;
    private bool laserIsActive = true;
    private bool laser2IsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        Flash(); //Call the Flash() function as soon as the object this script is attached to is active.
    }

    void Flash() //Changes the laser every x amount of seconds depending on the invoke. Instead of doing GameObject.SetActive() it's necessary to enable/disable everything except the object's hazard script or else it'll cause a bug.
    {
        if (laserIsActive == true)
        {
            laser.GetComponent<SpriteRenderer>().enabled = false;
            laser.GetComponent<BoxCollider2D>().enabled = false;
            laser.GetComponent<Animator>().enabled = false;
            laser2.GetComponent<SpriteRenderer>().enabled = true;
            laser2.GetComponent<BoxCollider2D>().enabled = true;
            laser2.GetComponent<Animator>().enabled = true;

            laserIsActive = false;
            laser2IsActive = true;

            laserLight.SetActive(false);
            laserLight2.SetActive(true);

            if (laserBlock != null) //Used for Blacklasers
            {
                laserBlock.SetActive(false);
            }
        }
        else if (laser2IsActive == true)
        {
            laser.GetComponent<SpriteRenderer>().enabled = true;
            laser.GetComponent<BoxCollider2D>().enabled = true;
            laser.GetComponent<Animator>().enabled = true;
            laser2.GetComponent<SpriteRenderer>().enabled = false;
            laser2.GetComponent<BoxCollider2D>().enabled = false;
            laser2.GetComponent<Animator>().enabled = false;

            laserIsActive = true;
            laser2IsActive = false;

            laserLight.SetActive(true);
            laserLight2.SetActive(false);

            if (laserBlock != null) //Used for Blacklasers
            {
                laserBlock.SetActive(true);
            }
        }

        Invoke("Flash", 1);
    }
}
