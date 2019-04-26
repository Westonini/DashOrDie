using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCinematic : MonoBehaviour
{
    public GameObject BB;
    public GameObject blackMenu;

    public Animator animator;
    public Animator glassAnimator;
    public Animator blackMenuAnimator;
    public GameObject pedestalGlass;
    public GameObject diamond;
    public GameObject warningFlash;
    public GameObject exclamationPoint;

    public GameObject thoughtBubbleDot1;
    public GameObject thoughtBubbleDot2;
    public GameObject thoughtBubble;
    public GameObject thoughtBubbleDiamond;

    public GameObject escapeText;

    public GameObject trail;

    public GameObject props;

    public AudioSource audioSource;

    public ParticleSystem dashParticle;

    [HideInInspector]
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine("IntroScene"); //Starts the intro cinematic when the scene starts.

        AudioSource audioSource = GetComponent<AudioSource>();

        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }

    // Update is called once per frame
    void Update()
    {
        //Press escape to skip cutscene
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Tutorial");
        }
    }

    IEnumerator IntroScene() //Intro cinematic
    {
        blackMenu.SetActive(true);
        blackMenuAnimator.SetBool("FadeInActive", true);
        yield return new WaitForSeconds(1.5f);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        blackMenuAnimator.SetBool("FadeInActive", false);
        blackMenu.SetActive(false);
        animator.SetBool("IsFalling", true);
        yield return new WaitForSeconds(0.85f);
        FindObjectOfType<AudioManagerScript>().Play("Landing");
        yield return new WaitForSeconds(0.15f);
        animator.SetBool("IsFalling", false);     

        yield return new WaitForSeconds(0.75f);
        rb.velocity = Vector2.up * 5;
        FindObjectOfType<AudioManagerScript>().Play("AirJump");
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.up * 5;
        FindObjectOfType<AudioManagerScript>().Play("AirJump");
        yield return new WaitForSeconds(1f);
        thoughtBubbleDot1.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        thoughtBubbleDot2.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        thoughtBubble.SetActive(true);
        thoughtBubbleDiamond.SetActive(true);

        yield return new WaitForSeconds(2f);
        thoughtBubbleDot1.SetActive(false);
        thoughtBubbleDot2.SetActive(false);
        thoughtBubble.SetActive(false);
        thoughtBubbleDiamond.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        animator.SetFloat("Speed", 1);
        rb.velocity = Vector2.left * 2;
        yield return new WaitForSeconds(1.2f);
        animator.SetFloat("Speed", 0);
        rb.velocity = Vector2.left * 0;
        yield return new WaitForSeconds(0.5f);
        glassAnimator.SetBool("LiftGlass", true);
        yield return new WaitForSeconds(0.75f);
        pedestalGlass.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        FindObjectOfType<AudioManagerScript>().Play("Jump");
        animator.SetBool("IsJumping", true);
        rb.velocity = new Vector2(-2, 13);
        yield return new WaitForSeconds(1.1f);
        animator.SetBool("IsJumping", false);
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.15f);
        animator.SetBool("IsGrabbing", true);
        FindObjectOfType<AudioManagerScript>().Play("Pickup");
        yield return new WaitForSeconds(0.2f);
        diamond.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("IsGrabbing", false);

        yield return new WaitForSeconds(0.75f);
        warningFlash.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        audioSource.Play();
        rb.velocity = Vector2.up * 7;
        FindObjectOfType<AudioManagerScript>().Play("AirJump");
        exclamationPoint.SetActive(true);
        yield return new WaitForSeconds(1f);
        exclamationPoint.SetActive(false);
        yield return new WaitForSeconds(1.25f);
        transform.localScale += new Vector3(10.0f, 0, 0);
        animator.SetBool("IsJumping", true);
        FindObjectOfType<AudioManagerScript>().Play("Jump");
        rb.velocity = new Vector2(3, 10);
        yield return new WaitForSeconds(1.25f);
        rb.velocity = new Vector2(0, 0);
        animator.SetBool("IsJumping", false);
        dashParticle.Emit(20);
        trail.SetActive(true);
        animator.SetBool("IsDashing", true);
        rb.velocity = Vector2.right * 32;
        FindObjectOfType<AudioManagerScript>().Play("Dash");

        yield return new WaitForSeconds(2f);
        blackMenu.SetActive(true);
        blackMenuAnimator.SetBool("FadeOutActive", true);
        yield return new WaitForSeconds(1.5f);
        escapeText.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Tutorial");
    }
}
