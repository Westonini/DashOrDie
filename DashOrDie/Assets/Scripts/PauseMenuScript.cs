using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject transition;
    public Animator transitionAnim;

    private PlayerController PC;

    void Start()
    {
        PC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //If the player presses the Escape button...
        {
            if (gameIsPaused) //If the game is paused then it'll resume the game..
            {
                Resume();
            }
            else //Else the game will be paused.
            {
                Pause();
            }
        }
    }

    public void Resume() //Resumes the game by setting timescale to 1f, turning off the PauseMenuUI, and changing the gameIsPaused boolean to false. Also unfreezes player.
    {
        PC.GetComponent<Rigidbody2D>().isKinematic = false;
        PC.GetComponent<PlayerController>().enabled = true;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause() //Pauses the game by setting timescale to 0f, turning on the PauseMenuUI, and changing the gameIsPaused boolean to true. Also freezes player.
    {
        PC.GetComponent<Rigidbody2D>().isKinematic = true;
        PC.GetComponent<PlayerController>().enabled = false;

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu() //Starts the load menu transition and calls LoadMenu2() after 2 seconds.
    {
        Time.timeScale = 0.01f;
        transition.SetActive(true);
        transitionAnim.SetBool("FadeIn", false);
        transitionAnim.SetBool("FadeOut", true);

        Invoke("LoadMenu2", 0.025f);
    }

    public void LoadMenu2() //Loads the MainMenu
    {
        Time.timeScale = 1f;
        FindObjectOfType<AudioManagerScript>().Stop("Music");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() //Starts the quit game transition and calls QuitGame2() after 2 seconds.
    {
        Time.timeScale = 0.01f;
        transition.SetActive(true);
        transitionAnim.SetBool("FadeIn", false);
        transitionAnim.SetBool("FadeOut", true);

        Invoke("QuitGame2", 0.025f);
    }

    void QuitGame2() //Quits the game
    {
        Time.timeScale = 1f;
        FindObjectOfType<AudioManagerScript>().Stop("Music");
        Application.Quit();
    }

    public void ClickSound() //Plays a sound when the button is clicked.
    {
        FindObjectOfType<AudioManagerScript>().Play("ButtonClick");
    }
}
