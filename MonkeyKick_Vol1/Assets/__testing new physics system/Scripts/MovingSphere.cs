//===== MOVING SPHERE =====//
/*
6/18/21
Description:
- tutorial stuff

Author: Merlebirb, thanks to Catlike Coding
*/

using UnityEngine;
using UnityEngine.InputSystem;

namespace MonkeyKick
{
    public class MovingSphere : MonoBehaviour
    {
        #region PUBLIC FIELDS

        #endregion

        #region PRIVATE FIELDS

        private Rigidbody _rb;
        private Vector2 _movement;
        private PlayerControls _input;
        private InputAction _move;
        private InputAction _jump;
        private bool _pressedJump;
        private Vector3 _velocity;
        private Vector3 _desiredVelocity;
        private bool _onGround;
        [SerializeField, Range(0f, 100f)] private float maxSpeed = 1f;
        [SerializeField, Range(0f, 100f)] private float maxAcceleration = 1f;
        [SerializeField, Range(0f, 20f)] private float jumpHeight = 1f;

        #endregion

        private void Awake()
        {
            InputSystem.pollingFrequency = 180;

            _input = new PlayerControls();
            _move = _input.Overworld.Move;
            _jump = _input.Overworld.Jump;

            _move.performed += context => _movement = context.ReadValue<Vector2>(); // store the player input into the _movement vector

            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Vector2 playerMovement = Vector2.ClampMagnitude(_movement, 1f);
            _desiredVelocity = new Vector3(playerMovement.x, 0f, playerMovement.y) * maxSpeed;

            _pressedJump |= _jump.triggered;
        }

        private void FixedUpdate()
        {
            _velocity = _rb.velocity; // retrieve the original velocity
            float maxSpeedChange = maxAcceleration * Time.deltaTime;

            // allows the object to control its movement, to prevent car like movement
            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, maxSpeedChange);
            _velocity.z = Mathf.MoveTowards(_velocity.z, _desiredVelocity.z, maxSpeedChange);

            if (_pressedJump)
            {
                _pressedJump = false;
                Jump();
            }

            _rb.velocity = _velocity;

            _onGround = false;
        }

        private void Jump()
        {
            if (_onGround)
            {
                _velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            }
        }

        private void OnCollisionStay()
        {
            _onGround = true;
        }

        private void OnEnable()
        {
            _input.Overworld.Enable();
        }

        private void OnDisable()
        {
            _input.Overworld.Disable();
        }
    }
}
