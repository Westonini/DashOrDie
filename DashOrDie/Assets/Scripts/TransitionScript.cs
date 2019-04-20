using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    public GameObject transition;
    public Animator transitionAnim;

    private PlayerController PC;
    public Animator playerAnim;

    private LevelManager LM;
    public GameObject room1;
    public GameObject room2;
    public GameObject room3;
    public GameObject room4;
    public GameObject room5;

    void Awake()
    {
        try //Try to get the LevelManager Script from an object tagged "LM". Otherwise set LM to null.
        {
            LM = GameObject.FindWithTag("LM").GetComponent<LevelManager>();
        }
        catch
        {
            LM = null;
        }
        
    }
    void Start() 
    {
        BeginTransitionIn(); //calls this function at the start of the scene.
        
        if (LM != null)
        {
            if (LM.checkpointNumber == 1) //If the first checkpoint was already reached (found by referencing the CheckpointManager Script) then start at room2.
            {
                room1.SetActive(false);
                room2.SetActive(true);
            }
            if (LM.checkpointNumber == 2) //If the second checkpoint was already reached (found by referencing the CheckpointManager Script) then start at room3.
            {
                room1.SetActive(false);
                room3.SetActive(true);
            }
            if (LM.checkpointNumber == 3) //If the third checkpoint was already reached (found by referencing the CheckpointManager Script) then start at room4.
            {
                room1.SetActive(false);
                room4.SetActive(true);
            }
            if (LM.checkpointNumber == 4) //If the fourth checkpoint was already reached (found by referencing the CheckpointManager Script) then start at room5.
            {
                room1.SetActive(false);
                room5.SetActive(true);
            }
        }


    }

    public void BeginTransitionIn() //Freezes player and transitions in.
    {
        try //If there's no player gameobject skip this code.
        {
            PC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            PC.GetComponent<Animator>().enabled = false;
            PC.GetComponent<PlayerController>().enabled = false;
        }
        catch
        {

        }

        transition.SetActive(true);
        transitionAnim.SetBool("FadeOut", false);
        transitionAnim.SetBool("FadeIn", true);
        Invoke("RemoveTransition", 1.25f);
    }

    public void BeginTransitionOut() //Freezes player and transitions out.
    {
        try //If there's no player gameobject skip this code.
        {
            PC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            PC.GetComponent<Animator>().enabled = false;
            PC.GetComponent<PlayerController>().enabled = false;
        }
        catch
        {

        }

        transition.SetActive(true);
        transitionAnim.SetBool("FadeIn", false);
        transitionAnim.SetBool("FadeOut", true);
    }

    void RemoveTransition() //Enables the player to move after the transition ends. 
    {
        try //If there's no player gameobject skip this code.
        {
            PC.GetComponent<Animator>().enabled = true;
            PC.GetComponent<PlayerController>().enabled = true;
        }
        catch
        {

        }

        transition.SetActive(false);
    }
}
