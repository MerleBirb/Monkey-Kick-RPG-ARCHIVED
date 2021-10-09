// Merle Roji
// 10/5/21

using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.Controls;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public class PlayerMovement : CharacterMovement
    {
        #region CONTROLS

        private PlayerControls _controls;
        private InputAction _move;
        private InputAction _jump;

        private bool _hasPressedJump = false;

        #endregion

        #region UNITY METHODS

        public override void Awake()
        {
            base.Awake();

            // create a new instance of controls
            InputSystem.pollingFrequency = 180;
            _controls = new PlayerControls();

            // set controls
            _move = _controls.Overworld.Move;
            _jump = _controls.Overworld.Jump;
            _move.performed += ctx => _movement = ctx.ReadValue<Vector2>();
        }

        private void Update()
        {
            if (_controls != null) CheckInput();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (_physics != null) Movement();
            if (_controls != null) CheckInput(); 
        }

        private void OnEnable()
        {
            _controls?.Overworld.Enable();
        }

        private void OnDisable()
        {
            _controls?.Overworld.Disable();
        }

        #endregion

        #region METHODS

        private void CheckInput()
        {
            _hasPressedJump = _jump.triggered;

            Jump();
        }

        private void Movement()
        {
            // normalize the movement input
            _movement.Normalize();
            _isMoving = Mathf.Abs(_movement.x) > 0f || Mathf.Abs(_movement.y) > 0f;

            if (direction) _physics.Movement(_movement, _physics.GetMoveSpeed(), direction); // if a camera exists, use it's direction
            else _physics.Movement(_movement, _physics.GetMoveSpeed()); // if camera doesnt exist default is Vector3.forward

             // if no input, dont move
        }

        private void Jump()
        {
            if (_hasPressedJump)
            {
                if (_physics.OnGround())
                {
                    _physics.Jump();
                }

                _hasPressedJump = false;
            }
        }

        #endregion
    }
}

