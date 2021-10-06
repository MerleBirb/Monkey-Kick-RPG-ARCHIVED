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
            _move.performed += ctx => _movement = ctx.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void OnEnable()
        {
            _controls.Overworld.Enable();
        }

        private void OnDisable()
        {
            _controls.Overworld.Disable();
        }

        #endregion

        #region METHODS

        private void Movement()
        {
            // normalize the movement input
            _movement.Normalize();

            if (!direction) _physics.Movement(_movement, _rb, moveSpeed, direction); // if a camera exists, use it's direction
            else _physics.Movement(_movement, _rb, moveSpeed, direction);
        }

        #endregion
    }
}

