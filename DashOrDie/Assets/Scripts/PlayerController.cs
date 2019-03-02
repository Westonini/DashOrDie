using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables

    [Space]
    [Header("Movement & Jump Settings:")]
    public int movementSpeed;
    private float horizontalInput;

    private Rigidbody2D rb;

    public int jumpHeight;
    public int extraJumpCount;
    private int extraJumpCountReset;
    public bool isJumping;

    public int dashPower;
    public bool dashIsActive;
    public bool dashIsOnCooldown;
    public float dashDuration;
    public float dashCooldownDuration;
    private float dashTimeElapsed = 0f;
    private float dashCooldownTime = 0f;
    private bool buttonDownDashRight;
    private bool buttonDownDashLeft;
    private bool buttonDownDashUp;
    private bool buttonDownDashDown;
    private bool dashIsHorizontal;
    private bool dashIsDownward;
    private bool dashIsUpward;
    private bool dashedUpOnce;

    [Space]
    [Header("Ground Check Settings:")]
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    [Space]
    [Header("Other:")]
    public Animator animator;

    public GameObject trail;

    private bool facingRight = true;

    void Start()
    {
        //References RigidBody2D to variable rb
        rb = GetComponent<Rigidbody2D>();

        //Sets extraJumpCountReset to the value of extraJumpCount so it can later be reset back to the original value.
        extraJumpCountReset = extraJumpCount;
    }


    void Update()
    {
        //If the player moves, the walk animation plays
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
       
        //Ground check. If the ground layer is touching the radius (which is an empty object placed under the player) then it's grounded.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        //If the player presses the jump key AND they have jumps avaliable, they'll jump depending on the value of the jumpHeight variable. Plays jumping animation.
        if (Input.GetButtonDown("Jump") && extraJumpCount != 0 && isGrounded == false && dashIsActive == false)
        {
            rb.velocity = Vector2.up * jumpHeight;
            extraJumpCount--;
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsMidAirJumping", true);
            //isJumping = true;
            //Invoke("IsJumpingTurnOff", 0.05f);
        }
        else if (Input.GetButtonDown("Jump") && isGrounded == true && dashIsActive == false)
        {
            rb.velocity = Vector2.up * jumpHeight;
            animator.SetBool("IsJumping", true);
            isJumping = true;
            Invoke("IsJumpingTurnOff", 0.05f);
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

            dashedUpOnce = false;
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

            if (dashIsHorizontal == true)
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
            

            if (dashTimeElapsed >= dashDuration)
            {
                dashIsActive = false;
                dashIsHorizontal = false;
                dashIsDownward = false;
                dashIsUpward = false;
                dashTimeElapsed = 0f;
                trail.SetActive(false);
                animator.SetBool("IsDashing", false);
                animator.SetBool("IsDashingDown", false);
                animator.SetBool("IsDashingUp", false);
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
        if (Input.GetButtonDown("Dash") && facingRight == true && horizontalInput > 0 && dashIsActive == false && dashIsOnCooldown == false)
        {
            buttonDownDashRight = true;
            trail.SetActive(true);
        }
        else if (Input.GetButtonDown("Dash") && facingRight != true && horizontalInput < 0 && dashIsActive == false && dashIsOnCooldown == false)
        {
            buttonDownDashLeft = true;
            trail.SetActive(true);
        }
       else if (Input.GetButtonDown("Dash") && Input.GetButton("UpArrow") && dashIsActive == false && dashIsOnCooldown == false && dashedUpOnce == false)
        {
            buttonDownDashUp = true;
            trail.SetActive(true);
        }
        else if (Input.GetButtonDown("Dash") && Input.GetButton("DownArrow") && dashIsActive == false && dashIsOnCooldown == false && isGrounded == false)
        {
            buttonDownDashDown = true;
            trail.SetActive(true);
        }


        
    }
    void FixedUpdate()
    {
        //Sets horizontalInput to horizontal movement, or left and right movement
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //Moves the player if they press the left or right arrow
        if (dashIsActive == false)
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

        }
        
        if (buttonDownDashLeft == true)
        {
            rb.velocity = Vector2.left * dashPower;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            dashIsActive = true;
            dashIsHorizontal = true;
            buttonDownDashLeft = false;
        }

        if (buttonDownDashUp == true)
        {
            rb.velocity = Vector2.up * dashPower;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            dashIsActive = true;
            dashIsUpward = true;
            buttonDownDashUp = false;
            dashedUpOnce = true;

        }

        if (buttonDownDashDown == true)
        {
            rb.velocity = Vector2.down * dashPower;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            dashIsActive = true;
            dashIsDownward = true;
            buttonDownDashDown = false;
        }
    }

    void Flip()
    {
        //Flip function used if the player is looking left. Flips the character's sprite.
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void IsJumpingTurnOff()
    {
        //This is later needed to invoke because since the player starts off grounded, it would reset immediately unless there's a timer.
        isJumping = false;
    }
}

