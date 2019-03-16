using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    private PlayerController PC;
    private GameObject Player;

    public GameObject fullDiamonds;
    public GameObject twoDiamonds;
    public GameObject oneDiamond;
    public GameObject noDiamonds;

    public GameObject transition;
    public Animator transitionAnim;

    public Animator playerAnim;

    public bool recovery;
    private float recoveryTime = 1.0f;

    private bool gameOver = false;
    private bool playedgameOverSound = false;

    [HideInInspector]
    public int Health = 3;

    // Start is called before the first frame update
    void Start()
    {
        PC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Changes the health state image per health state.
        if (Health == 2)
        {
            fullDiamonds.SetActive(false);
            twoDiamonds.SetActive(true);
        }
        else if (Health == 1)
        {
            twoDiamonds.SetActive(false);
            oneDiamond.SetActive(true);
        }
        else if (Health == 0) //When health is equal to 0, freeze the player and play a game over sound. Then do a fade out transition in 1.5 and reset the scene after 3 seconds.
        {           
            oneDiamond.SetActive(false);
            noDiamonds.SetActive(true);

            gameOver = true;

            if (gameOver == true && playedgameOverSound == false)
            {
                FindObjectOfType<AudioManagerScript>().Play("GameOver");
                PC.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                StartCoroutine(FadeOut());
                PC.GetComponent<Animator>().enabled = false;
                PC.GetComponent<PlayerController>().enabled = false;
                playedgameOverSound = true;
            }

            Invoke("FadeOutTransition", 1.5f);
            Invoke("Restart", 3);
        }

        if (recovery == true) //Player is temporarily immune to damage once they take damage.
        {
            recoveryTime -= Time.deltaTime;

            if (recoveryTime <= 0.0f)
            {
                recovery = false;
                recoveryTime = 1.0f;
            }
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

    IEnumerator FadeOut() //Causes the alpha and y-scale of the player to rapidly decrease.
    {
        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.90f);
        Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        //Player.transform.rotation = Quaternion.Euler(0, 0, 36);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.80f);
        Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        //Player.transform.rotation = Quaternion.Euler(0, 0, 36*2);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.70f);
        Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        //Player.transform.rotation = Quaternion.Euler(0, 0, 36*3);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.60f);
        Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        //Player.transform.rotation = Quaternion.Euler(0, 0, 36*4);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.50f);
        Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        //Player.transform.rotation = Quaternion.Euler(0, 0, 36*5);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.40f);
        Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        //Player.transform.rotation = Quaternion.Euler(0, 0, 36*6);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.30f);
        Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        //Player.transform.rotation = Quaternion.Euler(0, 0, 36*7);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.20f);
        Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        //Player.transform.rotation = Quaternion.Euler(0, 0, 36*8);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0.10f);
        Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        //Player.transform.rotation = Quaternion.Euler(0, 0, 36*9);

        yield return new WaitForSeconds(0.05f);

        PC.sr.color = new Color(PC.r, PC.g, PC.b, 0f);
        Player.transform.localScale -= new Vector3(0, 0.5f, 0.5f);
        //Player.transform.rotation = Quaternion.Euler(0, 0, 36*10);
    }
}
