// Merle Roji
// 10/5/21

using UnityEngine;

namespace MonkeyKick.PhysicalObjects
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class CharacterPhysics : MonoBehaviour, IPhysics
    {
        [SerializeField] private float moveSpeed = 1;
        [SerializeField] private float jumpHeight = 5;
        [SerializeField] private Rigidbody rb;

        public float GetMoveSpeed() => moveSpeed;
        public float GetJumpHeight() => jumpHeight;
        public float GetHeight() => col.height;
        public Rigidbody GetRigidbody() => rb;

        [SerializeField] private CapsuleCollider col;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float maxGroundedAngle = 60f; // for slope angles

        private RaycastHit _hitGround; // hit of whatever is grounding the character
        private Vector3 _currentGravity; // new gravity
        private Vector3 _groundNormal; // normal of whatever is grounding the character

        // private int _stepsSinceLastGrounded = 0;
        // private int _stepsSinceLastAerial = 0;
        // const int _stepMin = 0;
        // const int _stepMax = 10;

        public void Movement(Vector2 movementInput, float currentSpeed)
        {
            // apply movement speed to the movement input
            float xMove = movementInput.x * currentSpeed;
            float zMove = movementInput.y * currentSpeed;

            rb.velocity = new Vector3(xMove, rb.velocity.y, zMove); // set velocity
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
            rb.velocity = new Vector3(desiredVelocity.x, rb.velocity.y, desiredVelocity.z);
        }

        public void ResetMovement()
        {
            rb.velocity = Vector3.zero;
        }

        public void TurnOffGravity()
        {
            rb.useGravity = false;
        }

        public void Jump()
        {
            rb.velocity += new Vector3(0f, jumpHeight, 0f);
        }

        public bool OnGround()
        {
            float adjustHeight = (col.height / 2f) + 0.1f;
            return Physics.Raycast(rb.position, -Vector3.up, out _hitGround, adjustHeight, groundLayer); // raycast down, true if object is ground layer, store hit
        }

        public void ObeyGravity()
        {
            if (maxGroundedAngle > Vector3.Angle(_hitGround.normal, -Physics.gravity.normalized)) _groundNormal = _hitGround.normal;

            if (!OnGround()) _currentGravity = Physics.gravity; // normal gravity, when not grounded
            else if (OnGround()) _currentGravity = -_groundNormal * Physics.gravity.magnitude; // gravity perpendicular to a slope

            rb.AddForce(_currentGravity, ForceMode.Acceleration);
        }
    }
}
