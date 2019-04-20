using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    private PlayerController PC;
    private GameObject Player;
    private NonPassableHazardScript NPHS;

    private GameObject instantiatedDiamondLocation1;
    private GameObject instantiatedDiamondLocation2;
    private GameObject instantiatedDiamondLocation3;
    public GameObject diamond;

    public GameObject firstDiamond;
    public GameObject secondDiamond;
    public GameObject thirdDiamond;
    public Animator firstDiamondAnim;
    public Animator secondDiamondAnim;
    public Animator thirdDiamondAnim;


    public GameObject transition;
    public Animator transitionAnim;

    public Animator playerAnim;

    public bool playerHurt = false;
    public bool playerInstakilled = false;
    public bool recovery;
    public bool recovery2;
    public float recoveryTime = 1.0f;

    private bool gameOver = false;
    private bool playedgameOverSound = false;

    public int Health = 3;

    private LevelManager LM;
    void Awake()
    {
        //Get the NPHS Script from a LaserBlack, else return it as null
        try
        {
            NPHS = GameObject.Find("LaserBlackTypeA").GetComponent<NonPassableHazardScript>();
        }

        catch
        {
            NPHS = null;
        }

        if (NPHS == null)
        {
            try
            {
                NPHS = GameObject.Find("LaserBlackTypeB").GetComponent<NonPassableHazardScript>();
            }

            catch
            {
                NPHS = null;
            }
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
    // Start is called before the first frame update
    void Start()
    {
        PC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        Player = GameObject.FindWithTag("Player");

        instantiatedDiamondLocation1 = GameObject.Find("DiamondLocation1"); //These three locations are for where the diamonds get instantiated when the player gets hit.
        instantiatedDiamondLocation2 = GameObject.Find("DiamondLocation2");
        instantiatedDiamondLocation3 = GameObject.Find("DiamondLocation3");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHurt == true) //If player gets hurt make them immune to damage temporarily, instantiate diamonds, make the player's transparency blink, play a hurt sound, and switch the health state image.
        {
            if (NPHS != null && NPHS.nonPassableRecovery == true) //If they're hit by a NonPassableHazard do recovery2, otherwise do recovery.
            {
                recoveryTime = 0.65f;
                recovery2 = true;
                NPHS.nonPassableRecovery = false;
            }
            else
            {
                recovery = true;
            }
         
            InstantiateDiamonds();
            FindObjectOfType<AudioManagerScript>().Play("Hurt");
            LM.timesHit += 1;

            //Changes the health state image per health state.
            if (Health == 3)
            {
                StartCoroutine(Blink());
                firstDiamondAnim.SetBool("1stDiamondLost", true);
                Invoke("LoseFirstDiamond", 2);
                playerHurt = false;
            }
            else if (Health == 2)
            {
                StartCoroutine(Blink());
                secondDiamondAnim.SetBool("2ndDiamondLost", true);
                Invoke("LoseSecondDiamond", 2);
                playerHurt = false;
            }
            else if (Health == 1) //When health is equal to 1, freeze the player and play a game over sound. Then do a fade out transition in 1.5 and reset the scene after 3 seconds.
            {
                StopCoroutine(Blink());
                thirdDiamondAnim.SetBool("3rdDiamondLost", true);
                Invoke("LoseThirdDiamond", 2);

                gameOver = true;

                if (gameOver == true && playedgameOverSound == false)
                {
                    LM.playerHasDiedOnce = true;
                    FindObjectOfType<AudioManagerScript>().Play("GameOver");
                    PC.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    StartCoroutine(FadeOut());
                    PC.GetComponent<Animator>().enabled = false;
                    PC.GetComponent<PlayerController>().enabled = false;
                    playedgameOverSound = true;                  
                }

                Invoke("FadeOutTransition", 1.5f);
                Invoke("Restart", 3);

                playerHurt = false;
            }

            Health -= 1; //Removes 1 from the player's Health whenever the player is hurt.

        }


        if (recovery == true && recovery2 != true) //Player is temporarily immune to damage once they take damage.
        {
            recoveryTime -= Time.deltaTime;

            if (recoveryTime <= 0.0f)
            {
                recovery = false;
                recoveryTime = 1.0f;
            }
        }

        if (recovery2 == true) //Player is temporarily immune to damage once they take damage. (NonPassableHazard variant; shorter than regular recovery)
        {
            recoveryTime -= Time.deltaTime;

            if (recoveryTime <= 0.0f)
            {
                recovery2 = false;              
                recoveryTime = 1.0f;
            }
        }

        if (playerInstakilled == true) //Happens when the player gets hit by a hazard that can instakill the player.
        {
            Health = 0;
            InstantiateDiamonds();
            FindObjectOfType<AudioManagerScript>().Play("Hurt");
            LM.timesHit += 1;

            firstDiamondAnim.SetBool("1stDiamondLost", true);
            secondDiamondAnim.SetBool("2ndDiamondLost", true);
            thirdDiamondAnim.SetBool("3rdDiamondLost", true);

            gameOver = true;

            if (gameOver == true && playedgameOverSound == false)
            {
                LM.playerHasDiedOnce = true;
                FindObjectOfType<AudioManagerScript>().Play("GameOver");
                PC.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                StartCoroutine(FadeOut());
                PC.GetComponent<Animator>().enabled = false;
                PC.GetComponent<PlayerController>().enabled = false;
                playedgameOverSound = true;           
            }

            Invoke("FadeOutTransition", 1.5f);
            Invoke("Restart", 3);

            playerInstakilled = false;

        }
    }

    void Restart() //Restarts scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);      
    }

    void FadeOutTransition() //Does a fade out transition
    {
        transition.SetActive(true);
        transitionAnim.SetBool("FadeOut", true);
    }

    void LoseFirstDiamond()
    {
        firstDiamond.SetActive(false);
    }
    void LoseSecondDiamond()
    {
        secondDiamond.SetActive(false);
    }
    void LoseThirdDiamond()
    {
        thirdDiamond.SetActive(false);
    }

    IEnumerator FadeOut() //Causes the alpha and y-scale of the player to rapidly decrease.
    {
        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.90f);               //Decreases alpha
        //Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);    //Decreases y-scale
        Player.transform.rotation = Quaternion.Euler(0, 0, 36);         //Rotates 36 degrees

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.80f);
        //Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        Player.transform.rotation = Quaternion.Euler(0, 0, 36 * 2);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.70f);
        //Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        Player.transform.rotation = Quaternion.Euler(0, 0, 36 * 3);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.60f);
        //Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        Player.transform.rotation = Quaternion.Euler(0, 0, 36 * 4);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.50f);
        //Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        Player.transform.rotation = Quaternion.Euler(0, 0, 36 * 5);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.40f);
        //Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        Player.transform.rotation = Quaternion.Euler(0, 0, 36 * 6);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.30f);
        //Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        Player.transform.rotation = Quaternion.Euler(0, 0, 36 * 7);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.20f);
        //Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        Player.transform.rotation = Quaternion.Euler(0, 0, 36 * 8);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.10f);
        //Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        Player.transform.rotation = Quaternion.Euler(0, 0, 36 * 9);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0f);
        //Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        Player.transform.rotation = Quaternion.Euler(0, 0, 36 * 10);

        PC.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void InstantiateDiamonds() //Instantiates diamond objects and then destroys them after 3 seconds.
    {
        GameObject clone = Instantiate(diamond, instantiatedDiamondLocation1.transform.position, instantiatedDiamondLocation1.transform.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(60f, 170f));
        Destroy(clone, 3f);
        GameObject clone2 = Instantiate(diamond, instantiatedDiamondLocation2.transform.position, instantiatedDiamondLocation2.transform.rotation);
        clone2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50f, 150f));
        Destroy(clone2, 3f);
        GameObject clone3 = Instantiate(diamond, instantiatedDiamondLocation3.transform.position, instantiatedDiamondLocation3.transform.rotation);
        clone3.GetComponent<Rigidbody2D>().AddForce(new Vector2(20f, 190f));
        Destroy(clone3, 3f);
    }

    IEnumerator Blink() //Causes the alpha of the player to rapidly change from 0.75 to 0.5, which creates a blinking effect.
    {
        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.75f);
        yield return new WaitForSeconds(0.2f);
        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.5f);
        yield return new WaitForSeconds(0.2f);
        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.75f);
        yield return new WaitForSeconds(0.2f);
        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.5f);
        yield return new WaitForSeconds(0.2f);
        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.75f);
        yield return new WaitForSeconds(0.2f);
        PC.sr.color = new Color(PC.r, PC.g, PC.b, 1);
    }
}
