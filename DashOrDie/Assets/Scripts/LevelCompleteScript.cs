using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScript : MonoBehaviour
{
    public GameObject levelCompleteMenu;
    public GameObject transition;
    public Animator transitionanim;
    public GameObject pauseMenu;

    void Start()
    {
        if (pauseMenu.activeInHierarchy == true)
        {
            pauseMenu.SetActive(false);
        }
    }

    public void NextLevel() 
    {
        FindObjectOfType<AudioManagerScript>().Play("ButtonClick");
        transition.SetActive(true);
        transitionanim.SetBool("FadeOut", true);
        levelCompleteMenu.SetActive(false);
        Invoke("NextLevel2", 0.035f);
    }

    public void NextLevel2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMainMenu()
    {
        FindObjectOfType<AudioManagerScript>().Play("ButtonClick");
        transition.SetActive(true);
        transitionanim.SetBool("FadeOut", true);
        levelCompleteMenu.SetActive(false);
        Invoke("ReturnToMainMenu2", 0.035f);
    }

    public void ReturnToMainMenu2()
    {
        FindObjectOfType<AudioManagerScript>().Stop("Music");
        SceneManager.LoadScene("MainMenu");
    }
}
