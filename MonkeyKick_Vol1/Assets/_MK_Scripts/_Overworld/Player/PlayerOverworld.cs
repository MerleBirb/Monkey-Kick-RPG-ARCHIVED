//===== PLAYER OVERWORLD =====//
/*
5/11/21
Description: 
- Contains player movement logic for the overworld game state.

Author: Merlebirb
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.QoL;
using MonkeyKick.Controls;
using MonkeyKick.AudioFX;

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

        #endregion

        #region ANIMATIONS

        const string IDLE = "Idle";

        #endregion

        #region VFX PROPERTIES

        [Serializable]
        public class OverworldVFX
        {
            public ParticleSystem groundDustPrefab;
            internal bool canDust;
        }

        [SerializeField] private OverworldVFX overworldVFX;

        private ParticleSystem _groundDust;

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
        }

        private void Start()
        {
            _groundDust = Instantiate(overworldVFX.groundDustPrefab, transform);
            _groundDust.transform.position = new Vector3(transform.position.x, transform.position.y - (Stats.Height / 2f), transform.position.z);
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
            if (_movement.x != 0 || _movement.y !=0)
            {
                _anim.SetFloat("horiz", _movement.x);
            }

            /// Animations
            // idle
            if (!_isMoving) { AnimQoL.PlayAnimation(_anim, _currentAnim, IDLE); }

            /// VFX
            // landing dust
            bool notGrounded = _physics.GetStepsSinceLastGrounded() != 0 && !_physics.OnGround();
            bool justLanded = overworldVFX.canDust && _physics.OnGround();
            if (notGrounded) { overworldVFX.canDust = true; }
            if (justLanded)
            {
                Sound landSfx = AudioTable.GetSound(SFXNames.LandGeneric001);

                _groundDust.Play();
                overworldSFX.FootBasedSFX.PlayRaw(
                        landSfx.Clip,
                        landSfx.Volume
                    );
                overworldVFX.canDust = false;
            }

            /// SFX
            // step
            if (_isMoving && _physics.OnGround())
            {
                Sound stepSfx = AudioTable.GetSound(SFXNames.RockStep001);
                bool isPlayingStepSound =
                    overworldSFX.StepSFX.source.clip == stepSfx.Clip &&
                    overworldSFX.StepSFX.source.isPlaying;

                if (!isPlayingStepSound)
                {
                    overworldSFX.StepSFX.PlayRaw(
                        stepSfx.Clip,
                        stepSfx.Volume,
                        stepSfx.Pitch * (_currentSpeed / moveSpeed) * 0.5f
                    );
                }
            }
        }

        private void ToggleSprint()
        {
            if (!_isSprinting)
            {
                if (_hasPressedSprint)
                {
                    Sound dashSfx = AudioTable.GetSound(SFXNames.DashGeneric001);

                    _isSprinting = true;
                    _hasPressedSprint = false;
                    _groundDust.Play();
                    overworldSFX.FootBasedSFX.PlayRaw(
                        dashSfx.Clip,
                        dashSfx.Volume
                    );
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
                    Sound jumpSfx = AudioTable.GetSound(SFXNames.JumpGeneric001);

                    _physics.SetStepsSinceLastAerial(0);
                    _rb.velocity += new Vector3(0f, jumpHeight, 0f);
                    overworldSFX.FootBasedSFX.PlayRaw(
                        jumpSfx.Clip,
                        jumpSfx.Volume
                    );
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
            //_input.Overworld.Disable();

            _hasPressedJump = false;
            _hasPressedSprint = false;
            _isSprinting = false;
        }
    }
}