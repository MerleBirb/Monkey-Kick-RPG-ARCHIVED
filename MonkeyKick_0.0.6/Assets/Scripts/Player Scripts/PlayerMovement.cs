﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// PLAYER MOVEMENT SCRIPT ///
    /// This script handles how player characters interact with physics, move, and collide!
    /// thank you @ Catlike Coding for the awesome tutorial on this! I've been looking for help on
    /// perfect movement everywhere ;v; 10/9/2020

    /// VARIABLES ///
    /// input variables
    // boolean for whether jump has been pressed or not
    private bool pressedJump;
    // the space the player's inputs come from (world space or camera space)
    [SerializeField]
    private Transform playerInputSpace = default;

    /// speed variables
    // maximum speed variable: changes how fast the object is moving
    [SerializeField]
    private float maxSpeed = 10f;
    // the velocity variable stores a vector that... well controls the velocity!
    // theres also a desiredVelocity variable here!
    private Vector3 velocity, desiredVelocity;
    // maximum acceleration variable, storing a value for the max acceleration
    [SerializeField]
    private float maxAcceleration = 10f, maxAirAcceleration = 1f;
    // the maximum snap speed stores how fast you can go until you dont snap... defaults to highest value
    [SerializeField, Range(0f, 100f)]
    private float maxSnapSpeed = 100f;
    // the height of the jump!
    [SerializeField]
    private float jumpHeight = 2f;
    // integer that holds maximum air jumps (if there ever are more than 1 jumps)
    [SerializeField]
    private int maxAirJumps = 0;
    private int jumpPhase;

    /// collision variables
    // the rigidbody contains unity's physics!
    private Rigidbody rb;
    // max ground angle checks to see the angle when collision is ground or not. theres one for slopes / stairs too!
    [SerializeField, Range(0f, 90f)] // measured by degrees
    private float maxGroundAngle = 45f, maxStairsAngle = 60f;
    // some crazy equation to calculate slope stuff... dont ask please i have no idea how it works
    private float minGroundDotProduct, minStairsDotProduct;
    // stores the normal of the player collision, including steep inclines
    private Vector3 contactNormal, steepNormal;
    // checking if the player is grounded or not by how many ground points its touching, including steep surfaces
    private int groundContactCount, steepContactCount;
    // check to see if the player is touching the ground
    private bool OnGround => groundContactCount > 0;
    // another check for steeper ground!
    private bool OnSteepGround => steepContactCount > 0;
    // both for debugging purposes and snapping to ground purposes, counts the steps since last grounded, and steps since last jumped
    private int stepsSinceLastGrounded, stepsSinceLastJump;
    // how close the ground is before the player snaps to it?
    [SerializeField, Min(0f)]
    private float probeDistance = 1f;
    // stores what specific layers to not ignore.
    [SerializeField]
    private LayerMask probeMask = -1, stairsMask = -1;

    /// custom gravity & physics variables
    // time to make custom axises, ive gone off the deep end
    private Vector3 upAxis, rightAxis, forwardAxis;

    /// FUNCTIONS
    /// Awake is called when the object activates, or turns on
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        OnSlopeValidate();
    }

    /// Update is called once per frame
    private void Update()
    {
        CheckInput();
    }

    /// FixedUpdate is called once per frame at a fixed framerate
    private void FixedUpdate()
    {
        UpdateState();
        CheckMovement();
        CleanState();
    }

    /// CheckInput takes the input from the user and applies it to the player's movements
    private void CheckInput()
    {
        // grounded movement
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        if (playerInputSpace)
        {
            rightAxis = ProjectDirectionOnPlane(playerInputSpace.right, upAxis);
            forwardAxis = ProjectDirectionOnPlane(playerInputSpace.forward, upAxis);
        }
        else
        {
            rightAxis = ProjectDirectionOnPlane(Vector3.right, upAxis);
            forwardAxis = ProjectDirectionOnPlane(Vector3.forward, upAxis);
        }

        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;

        // jumping input
        pressedJump |= Input.GetButtonDown("A_BUTTON"); // note... what the hell kind of operator is |= ?!
    }

    /// CheckMovement calculates the movement of the player. Self explanatory
    private void CheckMovement()
    {
        // grounded movement
        AdjustVelocity();

        // jumping 
        CheckJump();

        // total velocity sent back to the rigidbody
        rb.velocity = velocity;
    }

    /// CheckJump... checks to see if the input makes the object hop up! what else do you want me to say?
    private void CheckJump()
    {
        Vector3 gravity = CustomGravity.GetGravity(rb.position, out upAxis);

        if (pressedJump)
        {
            pressedJump = false;
            Jump(gravity);
        }

        velocity += gravity * Time.deltaTime;
    }

    /// Jump is the jumping action
    private void Jump(Vector3 gravity)
    {
        Vector3 jumpDirection;

        if (OnGround)
        {
            jumpDirection = contactNormal;
        }
        else if (OnSteepGround)
        {
            jumpDirection = steepNormal;
            jumpPhase = 0;
        }
        else if (maxAirJumps > 0 && jumpPhase <= maxAirJumps)
        {
            if (jumpPhase == 0)
            {
                jumpPhase = 1;
            }

            jumpDirection = contactNormal;
        }
        else
        {
            return;
        }

        stepsSinceLastJump = 0;
        jumpPhase += 1;

        float jumpSpeed = Mathf.Sqrt(2f * gravity.magnitude * jumpHeight);
        jumpDirection = (jumpDirection + upAxis).normalized;
        float alignedSpeed = Vector3.Dot(velocity, jumpDirection);
        if (alignedSpeed > 0f)
        {
            jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);
        }
        velocity += jumpDirection * jumpSpeed;
        
    }

    /// UpdateState checks and changes the current state of the player, whether grounded or in air or mid jump or etc
    private void UpdateState()
    {
        stepsSinceLastGrounded += 1;
        stepsSinceLastJump += 1;
        velocity = rb.velocity;

        if (OnGround || SnapToGround() || CheckSteepContacts())
        {
            stepsSinceLastGrounded = 0;
            if (stepsSinceLastJump > 1)
            {
                jumpPhase = 0;
            }
            if (groundContactCount > 1)
            {
                contactNormal.Normalize();
            }
        }
        else
        {
            contactNormal = upAxis;
        }
    }

    /// ClearState resets the state to clean up the state
    private void CleanState()
    {
        groundContactCount = steepContactCount = 0;

        if (maxSpeed == maxSnapSpeed)
        {
            maxSnapSpeed += 1;
        }

        contactNormal = steepNormal = Vector3.zero;
    }

    /// AdjustVelocity changes the velocity depending on the slope the player is on
    private void AdjustVelocity()
    {
        Vector3 xAxis = ProjectDirectionOnPlane(rightAxis, contactNormal);
        Vector3 zAxis = ProjectDirectionOnPlane(forwardAxis, contactNormal);

        float currentX = Vector3.Dot(velocity, xAxis);
        float currentZ = Vector3.Dot(velocity, zAxis);

        float acceleration = OnGround ? maxAcceleration : maxAirAcceleration;
        float maxSpeedChange = acceleration * Time.deltaTime;

        float newX = Mathf.MoveTowards(currentX, desiredVelocity.x, maxSpeedChange);
        float newZ = Mathf.MoveTowards(currentZ, desiredVelocity.z, maxSpeedChange);

        velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
    }

    /// OnCollisionEnter sets stuff when collision is entered with the desired object
    private void OnCollisionEnter(Collision collision)
    {
        CheckCollision(collision);
    }

    /// OnCollsinionStay sets stuff when collision is entered and it stays that way with the desired object
    private void OnCollisionStay(Collision collision)
    {
        CheckCollision(collision);
    }

    /// CheckCollision checks collision using vectors and rays to determine whether the character is grounded or not, if theyr'e
    /// hugging a wall or not, etc
    private void CheckCollision(Collision collision)
    {
        float minDot = GetMinDot(collision.gameObject.layer);
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            float upDot = Vector3.Dot(upAxis, normal);
            if (upDot >= minDot)
            {
                groundContactCount += 1;
                contactNormal += normal;
            }
            else if (upDot > -0.01f)
            {
                steepContactCount += 1;
                steepNormal += normal;
            }
        }
    }

    /// OnSlopeValidate plugs in the maxGroundAngle into cosine
    private void OnSlopeValidate()
    {
        minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
        minStairsDotProduct = Mathf.Cos(maxStairsAngle * Mathf.Deg2Rad);
    }

    /// gets the min of the dot, self explanatory, i know i say that a lot but its true
    private float GetMinDot(int layer)
    {
        return (stairsMask & (1 << layer)) == 0 ? minGroundDotProduct : minStairsDotProduct;
    }

    /// ProjectOnContactPlane helps the player move up and down the slope more accurately
    private Vector3 ProjectDirectionOnPlane(Vector3 direction, Vector3 normal)
    {
        return (direction - normal * Vector3.Dot(direction, normal)).normalized;
    }

    /// SnapToGround is for objects to not go flying off a slope
    private bool SnapToGround()
    {
        if (stepsSinceLastGrounded > 1 || stepsSinceLastJump <= 2)
        {
            return false;
        }

        float speed = velocity.magnitude;
        if (speed > maxSnapSpeed)
        {
            return false;
        }

        if (!Physics.Raycast(rb.position, -upAxis, out RaycastHit hit, probeDistance, probeMask))
        {
            return false;
        }

        float upDot = Vector3.Dot(upAxis, hit.normal);
        if (upDot < GetMinDot(hit.collider.gameObject.layer))
        {
            return false;
        }

        groundContactCount = 1;
        contactNormal = hit.normal;

        float dot = Vector3.Dot(velocity, hit.normal);
        if (dot > 0f)
        {
            velocity = (velocity - hit.normal * dot).normalized * speed;
        }

        return true;
    }

    /// CheckSteepContacts checks to see if the player is in some real steep collision
    private bool CheckSteepContacts()
    {
        if (steepContactCount > 1)
        {
            steepNormal.Normalize();
            float upDot = Vector3.Dot(upAxis, steepNormal);
            if (upDot >= minGroundDotProduct)
            {
                steepContactCount = 0;
                groundContactCount = 1;
                contactNormal = steepNormal;
                return true;
            }
        }

        return false;
    }
}
