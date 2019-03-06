using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    private PlayerController PCScript;
    private GameObject diamondLocation1;
    private GameObject diamondLocation2;
    private GameObject diamondLocation3;

    private HealthScript HS;

    public GameObject diamond;

    private float knockbackTime = 0.38f;
    [HideInInspector]
    public bool gettingKnockedback;

    // Start is called before the first frame update
    void Start()
    {
        PCScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        HS = GameObject.Find("Diamonds/Hitpoints").GetComponent<HealthScript>();

        diamondLocation1 = GameObject.Find("DiamondLocation1");
        diamondLocation2 = GameObject.Find("DiamondLocation2");
        diamondLocation3 = GameObject.Find("DiamondLocation3");
    }

    // Update is called once per frame
    void Update()
    {
        if (gettingKnockedback == true) //player can't move while knockedback.
        {
            PCScript.GetComponent<PlayerController>().enabled = false;
            knockbackTime -= Time.deltaTime;

            if (knockbackTime <= 0.0f)
            {
                gettingKnockedback = false;
                knockbackTime = 0.38f;
                PCScript.GetComponent<PlayerController>().enabled = true;
            }
        }
    }

    //If the player gets hit by a hazard (while dashing and recovery are both false), make the character's alpha blink, instantiate diamonds, play a hurt sound, make them immune to damage for one second, and subtract one health state.
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && PCScript.dashIsActive == false && HS.recovery == false)
        {
            if (HS.Health > 1)
            {
                StartCoroutine(Blink());

                InstantiateDiamonds();
                FindObjectOfType<AudioManagerScript>().Play("Hurt");
                HS.recovery = true;
                HS.Health -= 1;

                if (PCScript.facingRight == true && PCScript.horizontalInput > 0)
                {
                    gettingKnockedback = true;
                    PCScript.rb.velocity = new Vector2(-2, 6);

                }
                else if (PCScript.facingRight == false && PCScript.horizontalInput < 0)
                {
                    gettingKnockedback = true;
                    PCScript.rb.velocity = new Vector2(2, 6);
                }
                else
                {
                    gettingKnockedback = true;
                    PCScript.rb.velocity = new Vector2(0, 6);
                }

            }
            else if (HS.Health == 1)
            {
                InstantiateDiamonds();
                FindObjectOfType<AudioManagerScript>().Play("Hurt");
                HS.Health -= 1;
            }
        }
    }

    private void InstantiateDiamonds() //Instantiates diamond objects and then destroys them after 3 seconds.
    {
        GameObject clone = Instantiate(diamond, diamondLocation1.transform.position, diamondLocation1.transform.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(60f, 170f));
        Destroy(clone, 3f);
        GameObject clone2 = Instantiate(diamond, diamondLocation2.transform.position, diamondLocation2.transform.rotation);
        clone2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50f, 150f));
        Destroy(clone2, 3f);
        GameObject clone3 = Instantiate(diamond, diamondLocation3.transform.position, diamondLocation3.transform.rotation);
        clone3.GetComponent<Rigidbody2D>().AddForce(new Vector2(20f, 190f));
        Destroy(clone3, 3f);
    }

    IEnumerator Blink() //Causes the alpha of the player to rapidly change from 0.75 to 0.5, which creates a blinking effect.
    {
        PCScript.sr.color = new Color(PCScript.r, PCScript.g, PCScript.b, 0.75f);
        yield return new WaitForSeconds(0.2f);
        PCScript.sr.color = new Color(PCScript.r, PCScript.g, PCScript.b, 0.5f);
        yield return new WaitForSeconds(0.2f);
        PCScript.sr.color = new Color(PCScript.r, PCScript.g, PCScript.b, 0.75f);
        yield return new WaitForSeconds(0.2f);
        PCScript.sr.color = new Color(PCScript.r, PCScript.g, PCScript.b, 0.5f);
        yield return new WaitForSeconds(0.2f);
        PCScript.sr.color = new Color(PCScript.r, PCScript.g, PCScript.b, 0.75f);
        yield return new WaitForSeconds(0.2f);
        PCScript.sr.color = new Color(PCScript.r, PCScript.g, PCScript.b, 1);
    }
}

