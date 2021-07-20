//===== PLAYER OVERWORLD =====//
/*
5/11/21
Description: 
- Contains player movement logic for the overworld game state.

Author: Merlebirb
*/

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.QoL;
using MonkeyKick.Controls;

namespace MonkeyKick.Overworld
{
    public class PlayerOverworld : CharacterOverworld
    {
        #region CONTROLS

        private InputAction _move;
        private InputAction _sprint;
        private InputAction _jump;

        private bool _hasPressedJump = false;
        private bool _hasPressedSprint = false;
        private bool _isSprinting = false;

        [SerializeField] private float sprintSpeed; // moveSpeed while sprint is pressed
        [SerializeField] private float jumpHeight;

        #endregion

        #region ANIMATIONS

        const string IDLE = "Idle";

        #endregion

        #region VFX PROPERTIES

        [SerializeField] private ParticleSystem groundDust;

        #endregion

        private PlayerControls _input;
        private float _currentSpeed;

        public override void Awake()
        {
            base.Awake();

            InputSystem.pollingFrequency = 180;
            _input = new PlayerControls();
            _move = _input.Overworld.Move;
            _jump = _input.Overworld.Jump;
            _sprint = _input.Overworld.Sprint;
            _move.performed += context => _movement = context.ReadValue<Vector2>();

            groundDust.transform.position = new Vector3(groundDust.transform.position.x, groundDust.transform.position.y - (Stats.Height / 2f), groundDust.transform.position.z);
        }

        public override void Update()
        {
            base.Update();

            if (_input != null) CheckForPlayerInput();
            if (_anim != null) AnimatePlayer();
        }

        public override void FixedUpdate()
        {
            if (_physics != null)
            {
                if (!_isSprinting) _currentSpeed = moveSpeed;
                else _currentSpeed = sprintSpeed;

                ApplyPhysics(_currentSpeed);
            }
        }

        private void CheckForPlayerInput()
        {
            _hasPressedJump = _jump.triggered;
            _hasPressedSprint = _sprint.triggered;

            ToggleSprint();
            PressedJump();
        }

        private void AnimatePlayer()
        {
            _anim.SetFloat("xDirection", _movement.x);
            _anim.SetFloat("zDirection", _movement.y);

            if (!_isMoving) { AnimQoL.PlayAnimation(_anim, _currentAnim, IDLE); }
        }

        private void ToggleSprint()
        {
            if (!_isSprinting)
            {
                if (_hasPressedSprint)
                {
                    _isSprinting = true;
                    _hasPressedSprint = false;
                    groundDust.Play();
                }
            }
            else
            {
                if (_hasPressedSprint)
                {
                    _isSprinting = false;
                    _hasPressedSprint = false;
                }
            }
        }

        private void PressedJump()
        {
            if (_hasPressedJump)
            {
                if (_physics.OnGround())
                {
                    _physics.SetStepsSinceLastAerial(0);
                    _rb.velocity += new Vector3(0f, jumpHeight, 0f);
                }

                _hasPressedJump = false;
            }

        }

        private void OnEnable()
        {
            _input.Overworld.Enable();

            _hasPressedJump = false;
            _hasPressedSprint = false;
            _isSprinting = false;
        }

        private void OnDisable()
        {
            _input.Overworld.Disable();

            _hasPressedJump = false;
            _hasPressedSprint = false;
            _isSprinting = false;
        }
    }
}