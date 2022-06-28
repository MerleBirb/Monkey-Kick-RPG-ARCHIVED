// Merle Roji 6/28/22

using UnityEngine;

namespace MonkeyKick.Characters
{
    /// <summary>
    /// Controls character physics in and out of battle.
    /// 
    /// Notes:
    /// 
    /// </summary>

    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class CharacterPhysics : MonoBehaviour
    {
        #region PRIVATE FIELDS

        [SerializeField] private float _moveSpeed = 1;
        public float MoveSpeed { get => _moveSpeed; }
        [SerializeField] private float _jumpHeight = 5;
        public float JumpHeight { get => _jumpHeight; }
        [SerializeField] private Rigidbody _rigidbody;
        public Rigidbody Rigidbody { get => _rigidbody; }
        [SerializeField] private CapsuleCollider _collider;
        public float Height { get => _collider.height; }
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _maxGroundAngle = 60f; // for slope angles
        private RaycastHit _hitGround; // hit of whatever is grounding the character
        private Vector3 _currentGravity; // new gravity
        private Vector3 _groundNormal; // normal of whatever is grounding the character

        #endregion

        #region METHODS

        public void Movement(Vector2 movementInput, float currentSpeed)
        {
            // apply movement speed to the movement input
            float xMove = movementInput.x * currentSpeed;
            float zMove = movementInput.y * currentSpeed;

            _rigidbody.velocity = new Vector3(xMove, _rigidbody.velocity.y, zMove); // set velocity
        }

        public void Movement(Vector2 movementInput, float currentSpeed, Transform faceDirection)
        {
            Vector3 forward = faceDirection.forward; // save forward direction of the pov
            forward.y = 0f; // dont need the y
            forward.Normalize();

            Vector3 right = faceDirection.right; // save right direction of the pov
            right.y = 0f; // dont need the y
            right.Normalize();

            Vector3 desiredVelocity = ((forward * movementInput.y) + (right * movementInput.x)) * currentSpeed;
            _rigidbody.velocity = new Vector3(desiredVelocity.x, _rigidbody.velocity.y, desiredVelocity.z);
        }

        public void ResetMovement()
        {
            _rigidbody.velocity = Vector3.zero;
        }

        public void TurnOffGravity()
        {
            _rigidbody.useGravity = false;
        }

        public void Jump()
        {
            _rigidbody.velocity += new Vector3(0f, _jumpHeight, 0f);
        }

        public void Jump(float height)
        {
            _rigidbody.velocity += new Vector3(0f, height, 0f);
        }

        public bool OnGround()
        {
            float adjustHeight = (_collider.height / 2f) + 0.1f;
            return Physics.Raycast(_rigidbody.position, -Vector3.up, out _hitGround, adjustHeight, _groundLayer); // raycast down, true if object is ground layer, store hit
        }

        public void ObeyGravity()
        {
            if (_maxGroundAngle > Vector3.Angle(_hitGround.normal, -Physics.gravity.normalized)) _groundNormal = _hitGround.normal;

            if (!OnGround()) _currentGravity = Physics.gravity; // normal gravity, when not grounded
            else if (OnGround()) _currentGravity = -_groundNormal * Physics.gravity.magnitude; // gravity perpendicular to a slope

            _rigidbody.AddForce(_currentGravity, ForceMode.Acceleration);
        }

        #endregion
    }
}
