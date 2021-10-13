// Merle Roji
// 10/5/21

using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.Managers;
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

        [Header("If this player is the leader of the Party, enable this.")]
        public bool isLeader = false;
        const string ENEMY_TAG = "Enemy";

        #region UNITY METHODS

        public override void Awake()
        {
            base.Awake();

            if (GameManager.InOverworld())
            {
                // create a new instance of controls
                InputSystem.pollingFrequency = 180;
                _controls = new PlayerControls();

                // set controls
                _move = _controls.Overworld.Move;
                _jump = _controls.Overworld.Jump;
                _move.performed += ctx => _movement = ctx.ReadValue<Vector2>();
            }
        }

        public override void Update()
        {
            base.Update();

            if (GameManager.InOverworld())
            {
                if (_controls != null) CheckInput();
            }           
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (GameManager.InOverworld())
            {
                if (_physics != null) Movement();
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            // running into an enemy to start battle
            if (GameManager.InOverworld() && col.CompareTag(ENEMY_TAG))
            {
                _physics?.ResetMovement(); // zero current velocity
                GameManager.InitiateBattle();
            }
        }

        public override void OnEnable()
        {
            base.OnEnable();

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

            PressedJump();
        }

        private void Movement()
        {
            // normalize the movement input
            _movement.Normalize();
            _isMoving = Mathf.Abs(_movement.x) > 0f || Mathf.Abs(_movement.y) > 0f;

            if (direction) _physics.Movement(_movement, _physics.GetMoveSpeed(), direction); // if a camera exists, use it's direction
            else _physics.Movement(_movement, _physics.GetMoveSpeed()); // if camera doesnt exist default is Vector3.forward
        }

        private void PressedJump()
        {
            if (_hasPressedJump)
            {
                if (_physics.OnGround())
                {
                    _physics.Jump();
                    _hasPressedJump = false;
                }

            }
        }

        #endregion
    }
}

