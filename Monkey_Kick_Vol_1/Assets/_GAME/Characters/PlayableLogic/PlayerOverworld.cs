//===== PLAYER OVERWORLD =====//
/*
5/11/21
Description: 
- Contains player physics and movement logic for the overworld game state.

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOverworld : MonoBehaviour
{
    private IPhysics _physics;
    
    #region CONTROLS

    const string MOVE = "Move";
    const string SPRINT = "Sprint";
    const string JUMP = "Jump";

    private InputAction move;
    private InputAction sprint;
    private InputAction jump;

    private bool hasPressedJump = false;
    private bool hasPressedSprint = false;
    private bool isSprinting = false;

    private Vector2 movementInput; // player's input for movement
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed; // moveSpeed while sprint is pressed

    [SerializeField] private float jumpHeight;

    #endregion
    
    #region COMPONENTS

    private Rigidbody rb;
    private PlayerInput input;

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        _physics = GetComponent<IPhysics>();
    }

    private void Start()
    {
        InputSystem.pollingFrequency = 180;
        
        move = input.actions.FindAction(MOVE);
        jump = input.actions.FindAction(JUMP);
        sprint = input.actions.FindAction(SPRINT);
        
        move.performed += context => movementInput = context.ReadValue<Vector2>();
    }
    
    private void Update()
    {
        if (input != null) CheckForPlayerInput();
        if (_physics != null) _physics.CheckIfGravityShouldApply(rb);
    }

    private void FixedUpdate()
    {   
        if (_physics != null)
        {
            float currentSpeed;

            if (!isSprinting) currentSpeed = moveSpeed;
            else currentSpeed = sprintSpeed;

            rb.velocity = _physics.Movement(movementInput, currentSpeed, rb.velocity.y);

            _physics.UpdatePhysicsCount(SnapToGround());
            _physics.ClearPhysicsCount();
        }
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

    private void CheckJump()
    {
        if (hasPressedJump)
        {
            if (_physics.OnGround())
            {
                _physics.SetStepsSinceLastAerial(0);
                rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            }
            
            hasPressedJump = false;
        }
        
    }

    private bool SnapToGround()
    {
        float speed = rb.velocity.magnitude;

        if (_physics.GetStepsSinceLastGrounded() > 1 || _physics.GetStepsSinceLastAerial() <= 3)
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

    private void OnEnable()
    {
        input.enabled = true;
    }

    private void OnDisable()
    {
        input.enabled = false;
    }
}