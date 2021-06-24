//===== CHARACTER PHYSICS =====//
/*
5/22/21
Description:
- Physics that a character specifically uses

Author: Merlebirb
*/

using UnityEngine;

namespace MonkeyKick.Overworld
{
    public class CharacterPhysics : MonoBehaviour, IPhysics
    {
        [HideInInspector] public bool isMoving = false;
        [SerializeField] private float radius = 0.55f; // for raycasts, ground check, etc
        [SerializeField] private LayerMask groundLayer;

        private int _stepsSinceLastGrounded = 0;
        private int _stepsSinceLastAerial = 0;

        public Vector3 Movement(Vector2 movement, float currentSpeed, float yVal)
        {
            float xMove = movement.x * currentSpeed;
            float zMove = movement.y * currentSpeed;

            Vector3 desiredVelocity = new Vector3(xMove, yVal, zMove);

            return desiredVelocity;
        }

        public Vector3 Movement(Vector2 movement, float currentSpeed, float yVal, Transform newDirection)
        {
            Vector3 forward = newDirection.forward;
            forward.y = 0f;
            forward.Normalize();

            Vector3 right = newDirection.right;
            right.y = 0f;
            right.Normalize();

            Vector3 desiredVelocity = (forward * movement.y + right * movement.x) * currentSpeed;
            Vector3 newVelocity = new Vector3(desiredVelocity.x, yVal, desiredVelocity.z);
            return newVelocity;
        }

        public bool OnGround()
        {
            return Physics.Raycast(transform.position, Vector3.down, radius, groundLayer);
        }

        public void CheckIfGravityShouldApply(Rigidbody rb)
        {
            rb.useGravity = !OnGround();
        }

        public void UpdatePhysicsCount(bool additionalChecks)
        {
            // useful for updating how long player has been on ground or air
            _stepsSinceLastGrounded++;
            _stepsSinceLastAerial++;

            if (OnGround() || additionalChecks)
            {
                _stepsSinceLastGrounded = 0;
            }
        }
        public void ClearPhysicsCount()
        {
            if (_stepsSinceLastGrounded > 10) { _stepsSinceLastGrounded = 10; }
            if (_stepsSinceLastAerial > 10) { _stepsSinceLastAerial = 10; }
        }

        public int GetStepsSinceLastGrounded() { return _stepsSinceLastGrounded; }
        public void SetStepsSinceLastGrounded(int newSteps) { _stepsSinceLastGrounded = newSteps; }
        public int GetStepsSinceLastAerial() { return _stepsSinceLastAerial; }
        public void SetStepsSinceLastAerial(int newSteps) { _stepsSinceLastAerial = newSteps; }
    }
}