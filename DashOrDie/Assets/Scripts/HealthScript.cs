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
        else if (Health == 0)
        {           
            oneDiamond.SetActive(false);
            noDiamonds.SetActive(true);

            gameOver = true;

            if (gameOver == true && playedgameOverSound == false)
            {
                FindObjectOfType<AudioManagerScript>().Play("GameOver");
                PC.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                PC.GetComponent<Animator>().enabled = false;
                PC.GetComponent<PlayerController>().enabled = false;
                playedgameOverSound = true;
            }

            Invoke("Restart", 3);
        }

        if (recovery == true)
        {
            recoveryTime -= Time.deltaTime;

            if (recoveryTime <= 0.0f)
            {
                recovery = false;
                recoveryTime = 1.0f;
            }
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
