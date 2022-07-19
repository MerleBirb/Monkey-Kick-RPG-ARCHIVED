// Merle Roji 7/10/22

using UnityEngine;
using UnityEngine.InputSystem;

namespace MonkeyKick.Characters.Players
{
    /// <summary>
    /// Controls player movement during the overworld portions
    /// 
    /// Notes:
    /// - make sure to add Game Manager
    /// - make sure to add Game State functionality
    /// </summary>

    public class PlayerOverworldPhysics : CharacterOverworldPhysics
    {
        private PlayerControls _controls;
        private InputAction _walk;
        private InputAction _jump;

        protected override void Awake()
        {
            base.Awake();

            // create a new instance of controls
            InputSystem.pollingFrequency = 180;
            _controls = new PlayerControls();

            // set controls
            _walk = _controls.Overworld.Walk;
            _jump = _controls.Overworld.Jump;
            _walk.performed += ctx => _movement = ctx.ReadValue<Vector2>();
        }

        private void Update()
        {
            CheckInput();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            CheckInputFixed();
        }

        private void OnEnable()
        {
            _controls?.Overworld.Enable();

            ResetMovement();
        }

        private void OnDisable()
        {
            _controls?.Overworld.Disable();

            ResetMovement();
        }

        private void CheckInput()
        {
            if (_controls == null) return;

            // jump
            if (_jump.triggered && OnGround()) Jump();
        }

        private void CheckInputFixed()
        {
            // normalize the movement input
            _movement.Normalize();
            _isMoving = Mathf.Abs(_movement.x) > 0f || Mathf.Abs(_movement.y) > 0f;

            if (_cameraDirection) Movement(MoveSpeed, _cameraDirection); // if a camera exists, use it's direction
            else Movement(MoveSpeed); // if camera doesnt exist default is Vector3.forward
        }
    }
}

