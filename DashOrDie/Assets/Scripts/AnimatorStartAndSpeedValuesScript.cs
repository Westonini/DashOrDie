using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStartAndSpeedValuesScript : MonoBehaviour
{
    private Animator anim;

    [Range(0, 1)]
    public float animStartTime;

    [Range(1, 5)]
    public float animSpeed;

    public string animationName;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        anim.speed = animSpeed;
        anim.Play(animationName, 0, animStartTime);
    }
}
