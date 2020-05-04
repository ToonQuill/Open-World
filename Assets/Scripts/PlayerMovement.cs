using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform character;

    public float verticalInput, horizontalInput, jumpInput;
    Rigidbody avatarBody;

    public float speedDampTime = 0.01f;
    public float characterVel = 10f;

    Vector3 velocity = Vector3.zero;

    public float numberofJumpsRemaining = 1;
    public float avatarGravity = 0.75f;
    public float jumpVel = 25f;
    public float distToGround = 0.1f;

    public Transform cameraPoint;

    //powerups
    public bool hasJump = false;

    // misc
    private Animator anim;

    Quaternion targetRotation;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    private void Start()
    {
        targetRotation = cameraPoint.rotation;
        avatarBody = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        getInput();
        movementManager(verticalInput);
        rotationManager(horizontalInput);
        Jump();
        avatarBody.velocity = cameraPoint.TransformDirection(velocity);
        character.rotation = cameraPoint.rotation;
    }

    void getInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetAxisRaw("Jump");
    }

    void movementManager(float vertical)
    {

        resetAnimations();

        if (Mathf.Abs(verticalInput) > 0)
        {
            velocity.z = characterVel * verticalInput;
        }
        else
        {
            velocity.z = 0;
        }
        if (Mathf.Abs(horizontalInput) > 0)
        {
            velocity.x = characterVel * horizontalInput;
        }
        else
        {
            velocity.x = 0;
        }

        if (verticalInput > 0)
        {
            anim.SetBool("Forwards", true);
            anim.SetBool("Backwards", false);
        }
        if (verticalInput < 0)
        {
            anim.SetBool("Backwards", true);
            anim.SetBool("Forwards", false);
        }
        if (verticalInput == 0)
        {
            anim.SetBool("Backwards", false);
            anim.SetBool("Forwards", false);
        }
    }

    void rotationManager(float horizontal)
    {
        if (Mathf.Abs(horizontalInput) > speedDampTime)
        {
            if (horizontalInput < 0)
            {
                anim.SetBool("leftStrafe", true);
                anim.SetBool("rightStrafe", false);
            }
            if (horizontalInput > 0)
            {
                anim.SetBool("rightStrafe", true);
                anim.SetBool("leftStrafe", false);
            }
            if (horizontalInput == 0)
            {
                anim.SetBool("rightStrafe", false);
                anim.SetBool("leftStrafe", false);
            }
        }
    }

    private void resetAnimations()
    {
        anim.SetBool("Backwards", false);
        anim.SetBool("Forwards", false);
        anim.SetBool("leftStrafe", false);
        anim.SetBool("rightStrafe", false);
        anim.SetBool("punching", false);
    }

    void Update()
    {

    }

    // hey buddy, you wanna jump?
    bool onGround()
    {
        return Physics.Raycast(character.position, Vector3.down, distToGround);
    }

    void Jump()
    {
        //initial jump
        if (jumpInput == 1 && onGround() && numberofJumpsRemaining != 0 && hasJump)
        {
            anim.SetBool("jump", true);
            velocity.y = jumpVel;
            numberofJumpsRemaining--;
        }
        //landing
        else if (onGround())
        {
            anim.SetBool("landing", true);
            anim.SetBool("jump", false);
            anim.SetBool("doubleJump", false);
            velocity.y = 0;
            numberofJumpsRemaining = 2;
        }
        //descent
        else
        {
            anim.SetBool("landing", false);
            anim.SetBool("doubleJump", false);
            velocity.y -= avatarGravity;
            if (numberofJumpsRemaining != 0)
            {
                numberofJumpsRemaining = 2;
            }
            if (velocity.y < 0)
            {
                velocity.y = 0;
            }
        }
    }

    void FootL()
    {

    }

    void FootR()
    {

    }

    void Land()
    {

    }
}

