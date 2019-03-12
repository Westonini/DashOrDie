using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHoverSound : MonoBehaviour
{

    public GameObject laserSelect;
    public GameObject laserSelect2;

    public void OnPointerEnter() //Plays a sound and shows a laser highlight when a button gets hovered over.
    {
        FindObjectOfType<AudioManagerScript>().Play("Bubble");
        laserSelect.SetActive(true);
        laserSelect2.SetActive(false);
    }
}
