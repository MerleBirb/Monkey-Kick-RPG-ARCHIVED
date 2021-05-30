//===== PLAYER OVERWORLD =====//
/*
5/11/21
Description: 
- Contains player movement logic for the overworld game state.

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.InputSystem;

namespace MonkeyKick.Character
{
    public class PlayerOverworld : CharacterOverworld
    {
        #region CONTROLS

        private const string Move = "Move";
        private const string Sprint = "Sprint";
        private const string Jump = "Jump";

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
            input.SwitchCurrentActionMap("Overworld");

            move = input.actions.FindAction(Move);
            jump = input.actions.FindAction(Jump);
            sprint = input.actions.FindAction(Sprint);

            move.performed += context => movement = context.ReadValue<Vector2>();
        }

        public override void Update()
        {
            base.Update();

            if (input != null) CheckForPlayerInput();
        }

        public override void FixedUpdate()
        {
            if (physics != null)
            {
                float _currentSpeed;

                if (!isSprinting) _currentSpeed = moveSpeed;
                else _currentSpeed = sprintSpeed;

                ApplyPhysics(_currentSpeed);
            }
        }

        private void CheckForPlayerInput()
        {
            hasPressedJump = jump.triggered;
            hasPressedSprint = sprint.triggered;

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
                if (physics.OnGround())
                {
                    physics.SetStepsSinceLastAerial(0);
                    rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
                }

                hasPressedJump = false;
            }

        }

        private void OnEnable()
        {
            ResetControls();
        }

        private void OnDisable()
        {
            ResetControls();
        }

        private void ResetControls()
        {
            hasPressedJump = false;
            hasPressedSprint = false;
            isSprinting = false;
        }
    }
}