//===== PLAYER OVERWORLD =====//
/*
5/11/21
Description: 
- Contains player movement logic for the overworld game state.

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOverworld : CharacterOverworld
{   
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

    [SerializeField] private float sprintSpeed; // moveSpeed while sprint is pressed
    [SerializeField] private float jumpHeight;

    #endregion
    
    private PlayerInput input;

    public override void Awake()
    {
        base.Awake();

        input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        InputSystem.pollingFrequency = 180;
        
        move = input.actions.FindAction(MOVE);
        jump = input.actions.FindAction(JUMP);
        sprint = input.actions.FindAction(SPRINT);
        
        move.performed += context => movement = context.ReadValue<Vector2>();
    }
    
    public override void Update()
    {
        base.Update();
        
        if (input != null) CheckForPlayerInput();
    }

    public override void FixedUpdate()
    {   
        if (_physics != null)
        {
            float currentSpeed;

            if (!isSprinting) currentSpeed = moveSpeed;
            else currentSpeed = sprintSpeed;

            ApplyPhysics(currentSpeed);
        }
    }

    private void CheckForPlayerInput()
    {
        hasPressedJump |= jump.WasPressedThisFrame();
        hasPressedSprint |= sprint.WasPressedThisFrame();

        ToggleSprint();
        PressedJump();
    }

    private void ToggleSprint()
    {
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

    private void PressedJump()
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

    private void OnEnable()
    {
        input.enabled = true;
    }

    private void OnDisable()
    {
        input.enabled = false;
    }
}