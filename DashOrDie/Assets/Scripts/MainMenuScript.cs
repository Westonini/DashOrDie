using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject transition;
    public GameObject FrozenMM;
    public GameObject MM;

    public Animator transitionanim;

    void Start() //At the start of the scene, the scene is unclickable and the fade in transition happens.
    {
        transition.SetActive(true);
        transitionanim.SetBool("FadeIn", true);
        FrozenMM.SetActive(true);
        MM.SetActive(false);
        Invoke("EnableMM", 1.25f);
    }
    public void StartGame() //When the "Start Game" button is clicked, make the scene unclickable and do a fade out transition.
    {
        transition.SetActive(true);
        transitionanim.SetBool("FadeOut", true);
        FrozenMM.SetActive(true);
        MM.SetActive(false);
        Invoke("StartGame2", 2f);
    }

    public void QuitGame() //When the "Start Game" button is clicked, make the scene unclickable and do a fade out transition.
    {
        transition.SetActive(true);
        transitionanim.SetBool("FadeOut", true);
        FrozenMM.SetActive(true);
        MM.SetActive(false);
        Invoke("QuitGame2", 2f);
    }

    public void EnableMM() //Enables the main menu to be clickable.
    {
        FindObjectOfType<AudioManagerScript>().Play("MM_Music");

        transitionanim.SetBool("FadeIn", false);
        transition.SetActive(false);
        FrozenMM.SetActive(false);
        MM.SetActive(true);
    }

    void StartGame2() //Loads the next scene
    {
        FindObjectOfType<AudioManagerScript>().Pause("MM_Music");
        FindObjectOfType<AudioManagerScript>().Play("Music");
        SceneManager.LoadScene(1);
    }
    void QuitGame2() //Quits the game
    {
        FindObjectOfType<AudioManagerScript>().Pause("MM_Music");
        Application.Quit();
    }

    public void ClickSound() //Plays a sound when the button is clicked.
    {
        FindObjectOfType<AudioManagerScript>().Play("ButtonClick");
    }

}
