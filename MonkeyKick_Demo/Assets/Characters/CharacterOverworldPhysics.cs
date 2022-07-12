// Merle Roji 7/10/22

using UnityEngine;

namespace MonkeyKick.Characters
{
    /// <summary>
    /// Controls character physics during the overworld portions
    /// 
    /// Notes:
    /// - make sure to add Game Manager
    /// - make sure to add Game State functionality
    /// </summary>

    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public abstract class CharacterOverworldPhysics : MonoBehaviour
    {
        protected Vector2 _movement; // movement from the character in xz axis saved here
        public Vector2 CurrentMovement { get => _movement; }
        protected bool _isMoving = false;
        [SerializeField] protected Transform _cameraDirection;

        [SerializeField] private float _moveSpeed = 1;
        public float MoveSpeed { get => _moveSpeed; }
        [SerializeField] private float _jumpHeight = 5;
        public float JumpHeight { get => _jumpHeight; }
        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody { get => _rigidbody; }
        private CapsuleCollider _collider;
        public CapsuleCollider Collider { get => _collider; }
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _maxGroundAngle = 60f; // for slope angles
        private RaycastHit _hitGround; // hit of whatever is grounding the character
        private Vector3 _currentGravity; // new gravity
        private Vector3 _groundNormal; // normal of whatever is grounding the character

        public virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
        }

        public virtual void FixedUpdate()
        {
            ObeyGravity();
        }

        protected void Movement(float currentSpeed)
        {
            // apply movement speed to the movement input
            float xMove = _movement.x * currentSpeed;
            float zMove = _movement.y * currentSpeed;

            _rigidbody.velocity = new Vector3(xMove, _rigidbody.velocity.y, zMove); // set velocity
        }

        protected void Movement(float currentSpeed, Transform faceDirection)
        {
            Vector3 forward = faceDirection.forward; // save forward direction of the pov
            forward.y = 0f; // dont need the y
            forward.Normalize();

            Vector3 right = faceDirection.right; // save right direction of the pov
            right.y = 0f; // dont need the y
            right.Normalize();

            Vector3 desiredVelocity = ((forward * _movement.y) + (right * _movement.x)) * currentSpeed;
            _rigidbody.velocity = new Vector3(desiredVelocity.x, _rigidbody.velocity.y, desiredVelocity.z);
        }

        protected void ResetMovement()
        {
            _rigidbody.velocity = Vector3.zero;
        }

        protected void TurnOffGravity()
        {
            _rigidbody.useGravity = false;
        }

        protected void Jump()
        {
            _rigidbody.velocity += new Vector3(0f, _jumpHeight, 0f);
        }

        protected void Jump(float height)
        {
            _rigidbody.velocity += new Vector3(0f, height, 0f);
        }

        protected bool OnGround()
        {
            float adjustHeight = (_collider.height / 2f) + 0.1f;
            return Physics.Raycast(_rigidbody.position, -Vector3.up, out _hitGround, adjustHeight, _groundLayer); // raycast down, true if object is ground layer, store hit
        }

        protected void ObeyGravity()
        {
            if (_maxGroundAngle > Vector3.Angle(_hitGround.normal, -Physics.gravity.normalized)) _groundNormal = _hitGround.normal;

            if (!OnGround()) _currentGravity = Physics.gravity; // normal gravity, when not grounded
            else if (OnGround()) _currentGravity = -_groundNormal * Physics.gravity.magnitude; // gravity perpendicular to a slope

            _rigidbody.AddForce(_currentGravity, ForceMode.Acceleration);
        }
    }
}

