// Merle Roji
// 10/5/21

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.QualityOfLife;
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
            CheckInput();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            Movement();
        }

        private void OnTriggerEnter(Collider col)
        {
            // running into an enemy to start battle
            if (col.CompareTag(TagsQoL.ENEMY_TAG))
            {
                CharacterMovement enemyMove = col.GetComponent<CharacterMovement>(); // save enemy movement
                CharacterBattle enemy = col.GetComponent<CharacterBattle>(); // save enemy battle
                Vector3 betweenPos = new Vector3(col.transform.position.x - 0.25f, col.transform.position.y + 5f, col.transform.position.z - 8f);

                // make a new list of all combatants
                List<CharacterBattle> fighters = new List<CharacterBattle>();
                fighters.Add(GetComponent<CharacterBattle>());
                fighters.Add(enemy);

                _physics?.ResetMovement(); // zero current velocity
                gameManager.InitiateBattle(betweenPos, fighters);

                // battle start on both the player and the enemy
                InvokeOnBattleStart();
                enemyMove.InvokeOnBattleStart();
            }
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
            if (_controls == null) return;

            _hasPressedJump = _jump.triggered;

            PressedJump();
        }

        private void Movement()
        {
            if (_physics == null) return;

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

