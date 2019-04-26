using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStartAndSpeedValuesScript : MonoBehaviour
{
    private Animator anim;

    [Range(0, 1)]
    public float animStartTime; //Sets the start time for the animation.

    [Range(1, 5)]
    public float animSpeed = 1; //Sets the speed for the animation.

    public string animationName; //In order to work, the animation's name needs to be here in the inspector.

    void Start()
    {
        anim = GetComponent<Animator>();  //Gets the animator component from the gameobject

        if (animationName != null)
        {
            anim.speed = animSpeed;
            anim.Play(animationName, 0, animStartTime);
        }
    }
}
