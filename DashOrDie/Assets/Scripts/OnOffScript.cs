﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffScript : MonoBehaviour
{
    public GameObject firstObject;
    public GameObject secondObject;

    public Animator firstObjectAnim;
    public Animator firstObjectAnim2;

    private void Start()
    {
      
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") //If the player touches the object this script is attached to, turn off the first sprite if there is one by first fading it out, and also enable the second object.
        {
            Invoke("DisableSprite", 3);
            secondObject.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;

            if (firstObjectAnim != null)
            {
                firstObjectAnim.SetBool("FadeOut", true);
            }
            else
            {
                DisableSprite();
            }

            if (firstObjectAnim2 != null)
            {
                firstObjectAnim2.SetBool("FadeOut", true);
            }
        }
    
    }

    void DisableSprite()
    {
        firstObject.SetActive(false);
    }
}
