using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightHazard : MonoBehaviour
{

    public Animator risingLavaAnim;
    public GameObject spotlights;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            risingLavaAnim.SetBool("IsRising", true);
            spotlights.SetActive(false);
            FindObjectOfType<AudioManagerScript>().Play("Warning");
        }
    }
}
