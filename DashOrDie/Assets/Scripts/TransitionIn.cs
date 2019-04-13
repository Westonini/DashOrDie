using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionIn : MonoBehaviour
{
    public GameObject transition;
    public Animator transitionAnim;

    private PlayerController PC;
    public Animator playerAnim;

    void Start() //At the start of the scene freeze the player and do a fade in transition.
    {
        PC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        PC.GetComponent<Rigidbody2D>().isKinematic = true;
        PC.GetComponent<Animator>().enabled = false;
        PC.GetComponent<PlayerController>().enabled = false;

        transition.SetActive(true);
        transitionAnim.SetBool("FadeIn", true);
        Invoke("RemoveTransition", 1.25f);


    }

    void RemoveTransition() //Enables the player to move after the transition ends. 
    {
        PC.GetComponent<Rigidbody2D>().isKinematic = false;
        PC.GetComponent<Animator>().enabled = true;
        PC.GetComponent<PlayerController>().enabled = true;

        transition.SetActive(false);
    }
}
