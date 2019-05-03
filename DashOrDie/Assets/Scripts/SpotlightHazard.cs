using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightHazard : MonoBehaviour
{

    public GameObject lasers;
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
            lasers.SetActive(true);
            spotlights.SetActive(false);
            FindObjectOfType<AudioManagerScript>().Play("Warning");
        }
    }
}
