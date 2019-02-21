using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserScript : MonoBehaviour
{
    private PlayerController PCScript;

    // Start is called before the first frame update
    void Start()
    {
        PCScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();  //References the SceneTransitionScript from the LevelTransition GameObject.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && PCScript.dashIsActive == true)
        {
            
        }
        else if (collision.tag == "Player" && PCScript.dashIsActive == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
