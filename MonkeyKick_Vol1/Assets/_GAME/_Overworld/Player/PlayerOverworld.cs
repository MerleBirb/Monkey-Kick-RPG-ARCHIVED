//===== PLAYER OVERWORLD =====//
/*
5/11/21
Description: 
- Contains player movement logic for the overworld game state.

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.QoL;
using MonkeyKick.CameraTools;

namespace MonkeyKick.Overworld
{
    public class PlayerOverworld : CharacterOverworld
    {
        #region CONTROLS

        private InputAction move;
        private InputAction sprint;
        private InputAction jump;

        private bool hasPressedJump = false;
        private bool hasPressedSprint = false;
        private bool isSprinting = false;

        [SerializeField] private float sprintSpeed; // moveSpeed while sprint is pressed
        [SerializeField] private float jumpHeight;

        #endregion

        #region ANIMATIONS

        const string IDLE = "Idle";

        #endregion

        private PlayerControls input;

        public override void Awake()
        {
            base.Awake();

            InputSystem.pollingFrequency = 180;

            input = new PlayerControls();

            move = input.Overworld.Move;
            jump = input.Overworld.Jump;
            sprint = input.Overworld.Sprint;

            move.performed += context => movement = context.ReadValue<Vector2>();
        }

        public override void Update()
        {
            base.Update();

            if (input != null) CheckForPlayerInput();
            if (_anim != null) AnimatePlayer();
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

        private void AnimatePlayer()
        {
            _anim.SetFloat("xDirection", movement.x);
            _anim.SetFloat("zDirection", movement.y);

            if (!_isMoving) { AnimQoL.PlayAnimation(_anim, _currentAnim, IDLE); }

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
                    rb.velocity += new Vector3(0f, jumpHeight, 0f);
                }

                hasPressedJump = false;
            }

        }

        private void OnEnable()
        {
            input.Overworld.Enable();

            hasPressedJump = false;
            hasPressedSprint = false;
            isSprinting = false;
        }

        private void OnDisable()
        {
            input.Overworld.Disable();

            hasPressedJump = false;
            hasPressedSprint = false;
            isSprinting = false;
        }
    }
}