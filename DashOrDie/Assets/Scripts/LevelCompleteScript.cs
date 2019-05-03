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

    private LevelManager LM;

    public bool lastLevel = false;

    [Header("TimesHit")]
    public GameObject timesHit_Flawless;
    public GameObject timesHit_One;
    public GameObject timesHit_Two;
    public GameObject timesHit_ThreeOrMore;
    [Header("PlayerHasDiedOnce")]
    public GameObject playerHasDiedOnce_YES;
    public GameObject playerHasDiedOnce_NO;
    [Header("TimeBonus")]
    public GameObject timeBonus_YES;
    public GameObject timeBonus_NO;
    [Header("Grade")]
    public GameObject grade_S;
    public GameObject grade_A;
    public GameObject grade_B;
    public GameObject grade_C;
    public GameObject grade_F;

    void Awake()
    {
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

    void Update()
    {
        //Scoring System
        if (levelCompleteMenu.activeInHierarchy == true)
        {
            //S GRADE
            if (LM.timesHit == 0 && LM.playerHasDiedOnce == false && LM.finishedBeforeTimeLimit == true) //0 times hit; 0 times died; finished before time limit
            {
                timesHit_Flawless.SetActive(true);
                playerHasDiedOnce_NO.SetActive(true);
                timeBonus_YES.SetActive(true);
                grade_S.SetActive(true);
            }

            //A GRADE
            else if (LM.timesHit == 0 && LM.playerHasDiedOnce == false && LM.finishedBeforeTimeLimit == false) //0 times hit; 0 times died; finished after time limit
            {
                timesHit_Flawless.SetActive(true);
                playerHasDiedOnce_NO.SetActive(true);
                timeBonus_NO.SetActive(true);
                grade_A.SetActive(true);
            }
            else if (LM.timesHit == 1 && LM.playerHasDiedOnce == false && LM.finishedBeforeTimeLimit == true) //1 time hit; 0 times died; finished before time limit
            {
                timesHit_One.SetActive(true);
                playerHasDiedOnce_NO.SetActive(true);
                timeBonus_YES.SetActive(true);
                grade_A.SetActive(true);
            }
            else if (LM.timesHit == 1 && LM.playerHasDiedOnce == false && LM.finishedBeforeTimeLimit == false) //1 time hit; 0 times died; finished after time limit
            {
                timesHit_One.SetActive(true);
                playerHasDiedOnce_NO.SetActive(true);
                timeBonus_NO.SetActive(true);
                grade_A.SetActive(true);
            }
            else if (LM.timesHit == 2 && LM.playerHasDiedOnce == false && LM.finishedBeforeTimeLimit == true) //2 times hit; 0 times died; finished before time limit
            {
                timesHit_Two.SetActive(true);
                playerHasDiedOnce_NO.SetActive(true);
                timeBonus_YES.SetActive(true);
                grade_A.SetActive(true);
            }
            else if (LM.timesHit == 2 && LM.playerHasDiedOnce == false && LM.finishedBeforeTimeLimit == false) //2 times hit; 0 times died; finished after time limit
            {
                timesHit_Two.SetActive(true);
                playerHasDiedOnce_NO.SetActive(true);
                timeBonus_NO.SetActive(true);
                grade_A.SetActive(true);
            }

            //B GRADE
            else if (LM.timesHit <= 4 && LM.playerHasDiedOnce == false && LM.finishedBeforeTimeLimit == true) //3-4 times hit; 0 times died; finished before time limit
            {
                timesHit_ThreeOrMore.SetActive(true);
                playerHasDiedOnce_NO.SetActive(true);
                timeBonus_YES.SetActive(true);
                grade_B.SetActive(true);
            }
            else if (LM.timesHit <= 4 && LM.playerHasDiedOnce == false && LM.finishedBeforeTimeLimit == false) //3-4 times hit; 0 times died; finished after time limit
            {
                timesHit_ThreeOrMore.SetActive(true);
                playerHasDiedOnce_NO.SetActive(true);
                timeBonus_NO.SetActive(true);
                grade_B.SetActive(true);
            }
            else if (LM.timesHit == 1 && LM.playerHasDiedOnce == true && LM.finishedBeforeTimeLimit == true) //1 time hit; 1+ times died; finished before time limit
            {
                timesHit_One.SetActive(true);
                playerHasDiedOnce_YES.SetActive(true);
                timeBonus_YES.SetActive(true);
                grade_C.SetActive(true);
            }
            else if (LM.timesHit == 2 && LM.playerHasDiedOnce == true && LM.finishedBeforeTimeLimit == true) //2 times hit; 1+ times died; finished before time limit
            {
                timesHit_Two.SetActive(true);
                playerHasDiedOnce_YES.SetActive(true);
                timeBonus_YES.SetActive(true);
                grade_C.SetActive(true);
            }

            //C GRADE
            else if (LM.timesHit == 5 && LM.playerHasDiedOnce == false && LM.finishedBeforeTimeLimit == true) //5 times hit; 0 times died; finished before time limit
            {
                timesHit_ThreeOrMore.SetActive(true);
                playerHasDiedOnce_NO.SetActive(true);
                timeBonus_YES.SetActive(true);
                grade_C.SetActive(true);
            }
            else if (LM.timesHit == 5 && LM.playerHasDiedOnce == false && LM.finishedBeforeTimeLimit == false) //5 times hit; 0 times died; finished after time limit
            {
                timesHit_ThreeOrMore.SetActive(true);
                playerHasDiedOnce_NO.SetActive(true);
                timeBonus_NO.SetActive(true);
                grade_C.SetActive(true);
            }
            else if (LM.timesHit == 1 && LM.playerHasDiedOnce == true && LM.finishedBeforeTimeLimit == false) //1 time hit; 1+ times died; finished before time limit
            {
                timesHit_One.SetActive(true);
                playerHasDiedOnce_YES.SetActive(true);
                timeBonus_NO.SetActive(true);
                grade_C.SetActive(true);
            }
            else if (LM.timesHit == 2 && LM.playerHasDiedOnce == true && LM.finishedBeforeTimeLimit == false) //2 times hit; 1+ times died; finished before time limit
            {
                timesHit_Two.SetActive(true);
                playerHasDiedOnce_YES.SetActive(true);
                timeBonus_NO.SetActive(true);
                grade_C.SetActive(true);
            }
            else if (LM.timesHit >= 3 && LM.playerHasDiedOnce == true && LM.finishedBeforeTimeLimit == true) //3+ times hit; 1+ times died; finished before time limit
            {
                timesHit_ThreeOrMore.SetActive(true);
                playerHasDiedOnce_YES.SetActive(true);
                timeBonus_YES.SetActive(true);
                grade_C.SetActive(true);
            }
            else if (LM.timesHit <= 5 && LM.playerHasDiedOnce == true && LM.finishedBeforeTimeLimit == false) //3-5 times hit; 1+ times died; finished before time limit
            {
                timesHit_ThreeOrMore.SetActive(true);
                playerHasDiedOnce_YES.SetActive(true);
                timeBonus_NO.SetActive(true);
                grade_C.SetActive(true);
            }

            //F GRADE
            else //5+ times hit; 1+ times died; finished after time limit
            {
                timesHit_ThreeOrMore.SetActive(true);
                playerHasDiedOnce_YES.SetActive(true);
                timeBonus_NO.SetActive(true);
                grade_F.SetActive(true);
            }
        }
    }

    public void NextLevel() //Turns off the LevelCompleteMenu, plays a button click sound, and starts the transition fade out. Also resets some of the LevelManager Script variables. Once the transition covers the screen, call NextLevel2()
    {
        FindObjectOfType<AudioManagerScript>().Play("ButtonClick");
        transition.SetActive(true);
        transitionanim.SetBool("FadeOut", true);
        levelCompleteMenu.SetActive(false);

        if (lastLevel == false) //If it's not the last level then transition to next level, otherwise if it is transition to main menu.
        {
            Invoke("NextLevel2", 0.035f);
        }
        else
        {
            Invoke("ReturnToMainMenu2", 0.035f);
            LM.endTimer = true;
        }
        
        LM.timesHit = 0;
        LM.playerHasDiedOnce = false;
        LM.finishedBeforeTimeLimit = true;
        LM.endTimer = false;
    }

    public void NextLevel2() //Loads next scene in build.
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);             
    }

    public void ReturnToMainMenu() //Turns off the LevelCompleteMenu, plays a button click sound, and starts the transition fade out. Also resets some of the LevelManager Script variables. Once the transition covers the screen, call ReturnToMainMenu2()
    {
        FindObjectOfType<AudioManagerScript>().Play("ButtonClick");
        transition.SetActive(true);
        transitionanim.SetBool("FadeOut", true);
        levelCompleteMenu.SetActive(false);
        Invoke("ReturnToMainMenu2", 0.035f);

        LM.timesHit = 0;
        LM.playerHasDiedOnce = false;
        LM.finishedBeforeTimeLimit = true;
    }

    public void ReturnToMainMenu2() //Loads MainMenu scene and stops the music.
    {
        FindObjectOfType<AudioManagerScript>().Stop("Music");
        SceneManager.LoadScene("MainMenu");
    }
}
