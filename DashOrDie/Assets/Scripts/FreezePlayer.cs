using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePlayer : MonoBehaviour
{

    private PlayerController PC;
    private Rigidbody2D rb;

    void Awake()
    {
        PC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
    // Start is called before the first frame update

    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            rb = PC.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
        }
    }
}
