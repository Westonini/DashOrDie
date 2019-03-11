using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHoverSound : MonoBehaviour
{
    public void OnPointerEnter()
    {
        FindObjectOfType<AudioManagerScript>().Play("Bubble");
    }
}
