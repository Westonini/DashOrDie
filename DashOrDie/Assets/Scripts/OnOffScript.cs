using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffScript : MonoBehaviour
{
    public GameObject firstObject;
    public GameObject secondObject;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            firstObject.SetActive(false);
            secondObject.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
