using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    private PlayerController PC;

    public GameObject fullDiamonds;
    public GameObject twoDiamonds;
    public GameObject oneDiamond;
    public GameObject noDiamonds;

    public GameObject transition;
    public Animator transitionAnim;

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
}
