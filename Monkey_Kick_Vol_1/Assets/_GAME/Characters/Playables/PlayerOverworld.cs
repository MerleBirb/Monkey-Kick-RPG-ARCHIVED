//===== PLAYER OVERWORLD =====//
/*
5/11/21
Description: 
- Contains player physics and movement logic for the overworld game state.

*/

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerOverworld : MonoBehaviour
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

    private InputAction move;
    private InputAction sprint;
    private InputAction jump;

    private bool isMoving = false;
    private bool hasPressedJump = false;
    private bool hasPressedSprint = false;
    private bool isSprinting = false;

    #endregion

    public void AwakeOverworld()
    {
    }

    public void StartOverworld()
    {
        InputSystem.pollingFrequency = 180;
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        
        move = input.actions.FindAction(MOVE);
        jump = input.actions.FindAction(JUMP);
        sprint = input.actions.FindAction(SPRINT);
        
        move.performed += context => movement = context.ReadValue<Vector2>();
    }

    public void UpdateOverworld()
    {
        CheckForPlayerInput();
        CheckIfGravityShouldApply();
    }

    public void FixedUpdateOverworld()
    {
        Movement();
        UpdatePhysicsCount();
        ClearPhysicsCount();
    }

    private void CheckForPlayerInput()
    {
        hasPressedJump |= jump.WasPressedThisFrame();
        hasPressedSprint |= sprint.WasPressedThisFrame();

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

        if (stepsSinceLastGrounded > 1 || stepsSinceLastJumped <= 3)
        {
            return false;
        }

        if (!Physics.Raycast(rb.position, -Vector3.up, out RaycastHit hit, 1f, -1))
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
