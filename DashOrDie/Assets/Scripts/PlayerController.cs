using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    //Variables

    [Space]
    [Header("Movement & Jump Settings:")]
    public int movementSpeed;
    public float horizontalInput;

    public int jumpHeight;
    public int extraJumpCount;
    private int extraJumpCountReset;
    public bool isJumping;

    public int dashPower;
    public bool dashIsActive;
    public bool dashIsOnCooldown;
    public float dashDuration;
    public float dashCooldownDuration;
    public float dashTimeElapsed = 0f;
    private float dashCooldownTime = 0f;
    private bool buttonDownDashRight;
    private bool buttonDownDashLeft;
    private bool buttonDownDashUp;
    private bool buttonDownDashDown;
    private bool buttonDownDashUpRight;
    private bool buttonDownDashUpLeft;
    private bool buttonDownDashDownRight;
    private bool buttonDownDashDownLeft;
    public bool dashIsHorizontal;
    private bool dashIsDownward;
    private bool dashIsUpward;
    private bool dashIsDiagonalUpRight;
    private bool dashIsDiagonalUpLeft;
    private bool dashIsDiagonalDownRight;
    private bool dashIsDiagonalDownLeft;
    //private bool dashedUpOnce;

    [Space]
    [Header("Ground/Wall/Ceiling Check Settings:")]
    public Transform topLeftGroundCheck;
    public Transform bottomRightGroundCheck;
    public bool isGrounded = false;

    public Transform topLeftWallCheck;
    public Transform bottomRightWallCheck;
    public bool isNextToWall = false;

    public Transform topLeftCeilingCheck;
    public Transform bottomRightCeilingCheck;
    public bool isTouchingCeiling = false;

    public LayerMask groundLayer;

    [Space]
    [Header("Trail:")]
    public GameObject trail;
    public TrailRenderer trailR;
    public GameObject trail2;
    public TrailRenderer trailR2;

    [Space]
    [Header("Other:")]
    public Animator animator;

    private HealthScript HS;
    private HazardScript HazS;

    [HideInInspector]
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public float r, g, b, alpha;
    public bool facingRight = true;

    void Start()
    {
        // Find HealthScript from the "Diamonds/Hitpoints" GameObject. If not found, return null.
        try
        {
            HS = GameObject.Find("Diamonds/Hitpoints").GetComponent<HealthScript>();
        }

        catch
        {
           // Debug.Log("HealthScript not found.");
            HS = null;
        }
        
        // Find HazardScript from a GameObject tagged "Hazard". If not found, return null.
        try
        {
            HazS = GameObject.FindWithTag("Hazard").GetComponent<HazardScript>();
        }
        catch
        {
            //Debug.Log("HazardScript not found.");
            HazS = null;
        }

        //References RigidBody2D to variable rb
        rb = GetComponent<Rigidbody2D>();

        //Sets extraJumpCountReset to the value of extraJumpCount so it can later be reset back to the original value.
        extraJumpCountReset = extraJumpCount;

        //Gets the object's sprite renderer and sets it to sr.
        sr = transform.GetComponent<SpriteRenderer>();

        r = sr.color.r;
        g = sr.color.g;
        b = sr.color.b;
        alpha = sr.color.a;

    }


    void Update()
    {
        //If the player moves, the walk animation plays
        if (isNextToWall == false)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        }
        else if (isNextToWall == true && isGrounded == true) //If the player is standing next to a wall, don't play walking animation.
        {
            animator.SetFloat("Speed", 0f);
        }

        //Wall check. If the Floor layer is touching the area (which is two empty objects placed next to the player) then it's next to a wall.
        isNextToWall = Physics2D.OverlapArea(topLeftWallCheck.position, bottomRightWallCheck.position, groundLayer);

        //Ground check. If the Floor layer is touching the area (which is two empty objects placed under the player) then it's grounded.
        isGrounded = Physics2D.OverlapArea(topLeftGroundCheck.position, bottomRightGroundCheck.position, groundLayer);

        //Ceiling check. If the Floor layer is touching the area (which is two empty objects placed above the player) then it's touching the ceiling.
        isTouchingCeiling = Physics2D.OverlapArea(topLeftCeilingCheck.position, bottomRightCeilingCheck.position, groundLayer);

        //If the player doesn't have 0 extra jumps, perofrm a mid-air jump. Plays mid-air jumping animation. Can't jump while dash is active.
        if (Input.GetButtonDown("Jump") && extraJumpCount != 0 && isGrounded == false && dashIsActive == false)
        {
            rb.velocity = Vector2.up * jumpHeight;
            extraJumpCount--;
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsMidAirJumping", true);

            FindObjectOfType<AudioManagerScript>().Play("AirJump");

        }
        //Initial jump from the ground. Plays jumping animation. Can't jump while dash is active.
        else if (Input.GetButtonDown("Jump") && isGrounded == true && dashIsActive == false)
        {
            animator.SetBool("IsJumping", false); //To reset the jump animation in case of animation bug.
            rb.velocity = Vector2.up * jumpHeight;
            Invoke("IsJumping", 0.02f);

            FindObjectOfType<AudioManagerScript>().Play("Jump");
        }

        //If the player isn't jumping but isn't grounding and isn't dashing, assume they're falling and play falling animation.
        if (isGrounded == false && dashIsActive == false && animator.GetBool("IsJumping") == false && animator.GetBool("IsMidAirJumping") == false)
        {
            animator.SetBool("IsFalling", true);
        }

        //Once the player touches the ground, their jump count gets reset to whatever it was originally set to.
        if (isGrounded == true)
        {
            extraJumpCount = extraJumpCountReset;
            animator.SetBool("IsFalling", false);

            //dashedUpOnce = false;
            animator.SetBool("IsDashingUp", false);
        }
        if (isGrounded == true && isJumping == false)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsMidAirJumping", false);
        }

        //If the player isn't facing right but is moving right, flip the sprite so it's facing right.
        //If the player isn't facing left but is moving left, flip the sprite so it's facing left.
        if (facingRight == false && horizontalInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && horizontalInput < 0)
        {
            Flip();
        }
    
    
       //When dash is activated, this code will run, starting the timers for dashTimeElasped and dashCooldownTime. Once dashTimeElasped is greater than dashDuration, the dash will end. Once dashCooldownTime is greater than dashCooldownDuration, the cooldown of the dash will end.
       //When dash is activated, play dashing animation.
       if (dashIsActive == true)
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsMidAirJumping", false);
            animator.SetBool("IsFalling", false);

            dashIsOnCooldown = true;
            dashTimeElapsed += Time.deltaTime;

            if (dashIsHorizontal == true || dashIsDiagonalUpRight == true || dashIsDiagonalDownLeft || dashIsDiagonalUpLeft == true || dashIsDiagonalDownRight == true)
            {
                animator.SetBool("IsDashing", true);
            }
            else if (dashIsDownward == true)
            {
                animator.SetBool("IsDashingDown", true);
            }
            else if (dashIsUpward == true)
            {
                animator.SetBool("IsDashingUp", true);
            }
            //else if (dashIsDiagonalUpRight == true || dashIsDiagonalDownLeft)
            //{
            //    animator.SetBool("IsDashingUpRightORDownLeft", true);
            //}
            //else if (dashIsDiagonalUpLeft == true || dashIsDiagonalDownRight == true)
            //{
            //    animator.SetBool("IsDashingUpLeftORDownRight", true);
            //}
            
            
            if (isGrounded == true && (dashIsDiagonalDownRight == true || dashIsDiagonalDownLeft == true))
            {
                rb.velocity = new Vector2(0, 0); //If the player touches the ground while diagonal dashing downward, stop their dash movement.
            }
            if (isNextToWall == true && (dashIsDiagonalUpRight == true || dashIsDiagonalUpLeft == true || dashIsDiagonalDownRight == true || dashIsDiagonalDownLeft == true))
            {
                rb.velocity = new Vector2(0, 0); //If the player touches the wall while diagonal dashing, stop their dash movement.
            }
            if (isTouchingCeiling == true && (dashIsDiagonalUpRight == true || dashIsDiagonalUpLeft == true || dashIsUpward == true))
            {
                rb.velocity = new Vector2(0, 0); //If the player touches the ceiling while diagonal dashing, stop their dash movement.
            }

            if (dashTimeElapsed >= dashDuration)
            {
                dashIsActive = false;
                dashIsHorizontal = false;

                if (dashIsUpward == true)
                {
                    rb.velocity = Vector2.up * (dashPower / 2);
                    dashIsUpward = false;
                }
                if (dashIsDownward == true)
                {
                    rb.velocity = Vector2.down * (dashPower / 3);
                    dashIsDownward = false;
                }
                if (dashIsDiagonalUpRight == true)
                {
                    rb.velocity = new Vector2(2, 2);
                    dashIsDiagonalUpRight = false;
                }
                if (dashIsDiagonalUpLeft == true)
                {
                    rb.velocity = new Vector2(-2, 2);
                    dashIsDiagonalUpLeft = false;
                }
                if (dashIsDiagonalDownRight == true)
                {
                    rb.velocity = new Vector2(2, -2);
                    dashIsDiagonalDownRight = false;
                }
                if (dashIsDiagonalDownLeft == true)
                {
                    rb.velocity = new Vector2(-2, -2);
                    dashIsDiagonalDownLeft = false;
                }

                dashTimeElapsed = 0f;
                trail.SetActive(false);
                trailR.Clear();
                trail2.SetActive(false);
                trailR2.Clear();
                animator.SetBool("IsDashing", false);
                animator.SetBool("IsDashingDown", false);
                animator.SetBool("IsDashingUp", false);
                //animator.SetBool("IsDashingUpRightORDownLeft", false);
                //animator.SetBool("IsDashingUpLeftORDownRight", false);

            }
        }

       //While dash is on cooldown, dash is unusable.
       if (dashIsOnCooldown == true)
        {
            dashCooldownTime += Time.deltaTime;

            if (dashCooldownTime >= dashCooldownDuration)
            {
                dashIsOnCooldown = false;
                dashCooldownTime = 0f;
            }
        }

        //If the user presses the dash key while moving AND if the dash isn't on cooldown, the boolean indicating a dash will be set to true.
        if (Input.GetButtonDown("Dash") && Input.GetButton("UpArrow") && facingRight == true && horizontalInput > 0 && dashIsOnCooldown == false && isNextToWall == false && isTouchingCeiling == false) //Dashes Up-Right
        {
            buttonDownDashUpRight = true;
            trail2.SetActive(true);
        }
        else if (Input.GetButtonDown("Dash") && Input.GetButton("UpArrow") && facingRight != true && horizontalInput < 0 && dashIsOnCooldown == false && isNextToWall == false && isTouchingCeiling == false) //Dashes Up-Left
        {
            buttonDownDashUpLeft = true;
            trail2.SetActive(true);
        }
        else if (Input.GetButtonDown("Dash") && Input.GetButton("DownArrow") && facingRight == true && horizontalInput > 0 && dashIsOnCooldown == false && isGrounded == false && isNextToWall == false) //Dashes Down-Right
        {
            buttonDownDashDownRight = true;
            trail2.SetActive(true);
        }
        else if (Input.GetButtonDown("Dash") && Input.GetButton("DownArrow") && facingRight != true && horizontalInput < 0 && dashIsOnCooldown == false && isGrounded == false && isNextToWall == false) //Dashes Down-Left
        {
            buttonDownDashDownLeft = true;
            trail2.SetActive(true);
        }
        else if (Input.GetButtonDown("Dash") && facingRight == true && horizontalInput > 0 &&  dashIsOnCooldown == false && Input.GetButton("DownArrow") == false && Input.GetButton("UpArrow") == false && isNextToWall == false) //Dashes Right
        {
            buttonDownDashRight = true;
            trail.SetActive(true);
        }
        else if (Input.GetButtonDown("Dash") && facingRight != true && horizontalInput < 0 && dashIsOnCooldown == false && Input.GetButton("DownArrow") == false && Input.GetButton("UpArrow") == false && isNextToWall == false) //Dashes Left
        {
            buttonDownDashLeft = true;
            trail.SetActive(true);
        }
        else if (Input.GetButtonDown("Dash") && Input.GetButton("UpArrow") && dashIsOnCooldown == false && Input.GetButton("LeftArrow") == false && Input.GetButton("RightArrow") == false && isTouchingCeiling == false)// && dashedUpOnce == false) //Dashes Up
        {
            buttonDownDashUp = true;
            trail.SetActive(true);
        }
        else if (Input.GetButtonDown("Dash") && Input.GetButton("DownArrow") && dashIsOnCooldown == false && isGrounded == false && Input.GetButton("LeftArrow") == false && Input.GetButton("RightArrow") == false) //Dashes Down
        {
            buttonDownDashDown = true;
            trail.SetActive(true);
        }


        if (Input.GetButton("DownArrow") && isGrounded == false) //If the player holds the down key while mid-air, they fall faster.
        {
            rb.gravityScale = 3;
        }
        else
        {
            rb.gravityScale = 2;
        }


        
    }
    void FixedUpdate()
    {
        //Sets horizontalInput to horizontal movement, or left and right movement
        if (dashIsActive == false)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }


        //Moves the player if they press the left or right arrow
        if (dashIsActive == false && (HS == null || HS.Health != 0) && (HazS == null || HazS.gettingKnockedback == false))
        {
            rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        }

        //If a dash key is registered it will do a dash and put the dash on cooldown. Freezes X or Y axis of player accordingly so they dash in a straight line.
        if (buttonDownDashRight == true)
        {
            rb.velocity = Vector2.right * dashPower;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            dashIsActive = true;
            dashIsHorizontal = true;
            buttonDownDashRight = false;
            FindObjectOfType<AudioManagerScript>().Play("Dash");
        }
        
        if (buttonDownDashLeft == true)
        {
            rb.velocity = Vector2.left * dashPower;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            dashIsActive = true;
            dashIsHorizontal = true;
            buttonDownDashLeft = false;
            FindObjectOfType<AudioManagerScript>().Play("Dash");
        }

        if (buttonDownDashUp == true)
        {
            rb.velocity = Vector2.up * dashPower;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            dashIsActive = true;
            dashIsUpward = true;
            buttonDownDashUp = false;
            //dashedUpOnce = true;
            FindObjectOfType<AudioManagerScript>().Play("Dash");
        }

        if (buttonDownDashDown == true)
        {
            rb.velocity = Vector2.down * dashPower;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            dashIsActive = true;
            dashIsDownward = true;
            buttonDownDashDown = false;
            FindObjectOfType<AudioManagerScript>().Play("Dash");
        }

        if (buttonDownDashUpRight == true)
        {
            rb.velocity = new Vector2(dashPower / 1.2f, dashPower / 1.2f);
            dashIsActive = true;
            dashIsDiagonalUpRight = true;
            buttonDownDashUpRight = false;
            FindObjectOfType<AudioManagerScript>().Play("Dash");
        }

        if (buttonDownDashUpLeft == true)
        {
            rb.velocity = new Vector2(-dashPower / 1.2f, dashPower / 1.2f);
            dashIsActive = true;
            dashIsDiagonalUpLeft = true;
            buttonDownDashUpLeft = false;
            FindObjectOfType<AudioManagerScript>().Play("Dash");
        }

        if (buttonDownDashDownRight == true)
        {
            rb.velocity = new Vector2(dashPower / 1.2f, -dashPower / 1.2f);
            dashIsActive = true;
            dashIsDiagonalDownRight = true;
            buttonDownDashDownRight = false;
            FindObjectOfType<AudioManagerScript>().Play("Dash");
        }

        if (buttonDownDashDownLeft == true)
        {
            rb.velocity = new Vector2(-dashPower / 1.2f, -dashPower / 1.2f);
            dashIsActive = true;
            dashIsDiagonalDownLeft = true;
            buttonDownDashDownLeft = false;
            FindObjectOfType<AudioManagerScript>().Play("Dash");
        }
    }

    void Flip()
    {
        //Flip function used if the player looks the other direction.
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    void IsJumping()
    {
        //This is later needed to invoke because since the player starts off grounded, it would reset immediately unless there's a timer.
        animator.SetBool("IsJumping", true);
        isJumping = true;
        Invoke("IsJumpingTurnOff", 0.05f);
    }

    void IsJumpingTurnOff()
    {
        isJumping = false;
    }
}

