//===== PLAYER MOVEMENT =====//
/*
5/11/21
Description: 
- Contains player physics and movement logic for the overworld scenes.

*/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region PHYSICS

    private Vector2 movement; // stores the input of the player to a vector 2
    private Vector2 lastMovement;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed; // moveSpeed while sprint is pressed

    [SerializeField] private float jumpHeight;

    [SerializeField] private float radius = 0.55f; // for raycasts, ground check, etc
    [SerializeField] public LayerMask groundLayer;
    private bool OnGround => Physics.Raycast(transform.position, Vector3.down, radius, groundLayer);
    private int stepsSinceLastGrounded = 0;
    private int stepsSinceLastJumped = 0;

    #endregion

    #region COMPONENTS

    private PlayerInput input; // stores the controls
    private Rigidbody rb;
    private Animator animator;

    #endregion
    
    #region CONTROLS

    const string MOVE = "Move";
    const string SPRINT = "Sprint";
    const string JUMP = "Jump";

    private bool isMoving = false;
    private bool hasPressedJump = false;
    private bool hasPressedSprint = false;
    private bool isSprinting = false;

    #endregion

    private void Awake()
    {
        InputSystem.pollingFrequency = 180;
    }

    public void StartMovement()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }

    public void UpdateMovement()
    {
        CheckForPlayerInput();
        CheckIfGravityShouldApply();
    }

    public void FixedUpdateMovement()
    {
        Movement();
        UpdatePhysicsCount();
        ClearPhysicsCount();
    }

    private void CheckForPlayerInput()
    {
        input.actions.FindAction(MOVE).performed += context => movement = context.ReadValue<Vector2>();
        hasPressedJump |= input.actions.FindAction(JUMP).WasPressedThisFrame();
        hasPressedSprint |= input.actions.FindAction(SPRINT).WasPressedThisFrame();

        // toggle sprint
        if (!isSprinting)
        {
            if (hasPressedSprint)
            {
                isSprinting = true;
                hasPressedSprint = false;
            }
        }
        else
        {
            if (hasPressedSprint)
            {
                isSprinting = false;
                hasPressedSprint = false;
            }
        }
    }

    private void Movement()
    {
        float currentSpeed;

        if (!isSprinting) { currentSpeed = moveSpeed; }
        else { currentSpeed = sprintSpeed; }

        float xMove = movement.x * currentSpeed;
        float zMove = movement.y * currentSpeed;

        Vector3 movement3D = new Vector3(xMove, rb.velocity.y, zMove);
        rb.velocity = movement3D; 

        if (Mathf.Abs(movement.x) > 0 || Mathf.Abs(movement.y) > 0) { isMoving = true; }
        else { isMoving = false; }

        if (hasPressedJump)
        {
            Jump();
            hasPressedJump = false;
        }
    }

    private void Jump()
    {
        if (OnGround)
        {
            stepsSinceLastJumped = 0;
            rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
        }
    }

    private void CheckIfGravityShouldApply()
    {
        if (OnGround) { rb.useGravity = false; }
        else { rb.useGravity = true; }
    }

    private void UpdatePhysicsCount()
    {
        // useful for updating how long player has been on ground or air
        stepsSinceLastGrounded++;
        stepsSinceLastJumped++;

        if (OnGround || SnapToGround())
        {
            stepsSinceLastGrounded = 0;
        }
    }

    private void ClearPhysicsCount()
    {
        if (stepsSinceLastGrounded > 10) { stepsSinceLastGrounded = 10; }
        if (stepsSinceLastJumped > 10) { stepsSinceLastJumped = 10; }

        hasPressedJump = false;
    }

    private bool SnapToGround()
    {
        float speed = rb.velocity.magnitude;

        if (stepsSinceLastGrounded > 1 || stepsSinceLastJumped <= 2)
        {
            return false;
        }

        if (!Physics.Raycast(rb.position, -Vector2.up, out RaycastHit hit, 1f, -1))
        {
            return false;
        }

        float dot = Vector3.Dot(rb.velocity, hit.normal);
        if (dot > 0f)
        {
            rb.velocity = (rb.velocity - hit.normal * dot).normalized * speed;
        }

        return true;
    }
}
