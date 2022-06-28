// Merle Roji 6/28/22

using UnityEngine;
using UnityEngine.InputSystem;

namespace MonkeyKick.Characters
{
    /// <summary>
    /// Controls player movement during the overworld portions
    /// 
    /// Notes:
    /// - make sure to add Game Manager
    /// - make sure to add Game State functionality
    /// </summary>

    public class PlayerOverworldMovement : CharacterOverworldMovement
    {
        private PlayerControls _controls;
        private InputAction _walk;
        private InputAction _jump;

        public override void Awake()
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

        public override void Update()
        {
            base.Update();

            CheckInput();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            Movement();
        }

        private void OnEnable()
        {
            _controls?.Overworld.Enable();

            if (_physics.OnGround()) _physics.ResetMovement();
        }

        private void OnDisable()
        {
            _controls?.Overworld.Disable();

            if (_physics.OnGround()) _physics.ResetMovement();
        }

        private void CheckInput()
        {
            if (_controls == null) return;

            // jump
            if (_jump.triggered && _physics.OnGround()) _physics.Jump();
        }

        private void Movement()
        {
            if (_physics == null) return;

            // normalize the movement input
            _movement.Normalize();
            _isMoving = Mathf.Abs(_movement.x) > 0f || Mathf.Abs(_movement.y) > 0f;

            if (_cameraDirection) _physics.Movement(_movement, _physics.MoveSpeed, _cameraDirection); // if a camera exists, use it's direction
            else _physics.Movement(_movement, _physics.MoveSpeed); // if camera doesnt exist default is Vector3.forward
        }
    }
}
