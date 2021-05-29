//===== CHARACTER PHYSICS =====//
/*
5/22/21
Description:
- Physics that a character specifically uses

Author: Merlebirb
*/

using UnityEngine;

namespace Merlebirb.CharacterLogic
{
    public class CharacterPhysics : MonoBehaviour, IPhysics
    {
        private bool isMoving = false;
        [SerializeField] private float radius = 0.55f; // for raycasts, ground check, etc
        [SerializeField] private LayerMask groundLayer;

        private int stepsSinceLastGrounded = 0;
        private int stepsSinceLastAerial = 0;

        public Vector3 Movement(Vector2 _movement, float _currentSpeed, float _yVal)
        {
            float _xMove = _movement.x * _currentSpeed;
            float _zMove = _movement.y * _currentSpeed;

            Vector3 _movement3D = new Vector3(_xMove, _yVal, _zMove);

            isMoving = Mathf.Abs(_movement.x) > 0 || Mathf.Abs(_movement.y) > 0;

            return _movement3D;
        }

        public bool OnGround()
        {
            return Physics.Raycast(transform.position, Vector3.down, radius, groundLayer);
        }

        public void CheckIfGravityShouldApply(Rigidbody _rb)
        {
            _rb.useGravity = !OnGround();
        }

        public void UpdatePhysicsCount(bool _additionalChecks)
        {
            // useful for updating how long player has been on ground or air
            stepsSinceLastGrounded++;
            stepsSinceLastAerial++;

            if (OnGround() || _additionalChecks)
            {
                stepsSinceLastGrounded = 0;
            }
        }
        public void ClearPhysicsCount()
        {
            if (stepsSinceLastGrounded > 10) { stepsSinceLastGrounded = 10; }
            if (stepsSinceLastAerial > 10) { stepsSinceLastAerial = 10; }
        }

        public int GetStepsSinceLastGrounded() { return stepsSinceLastGrounded; }
        public void SetStepsSinceLastGrounded(int newSteps) { stepsSinceLastGrounded = newSteps; }
        public int GetStepsSinceLastAerial() { return stepsSinceLastAerial; }
        public void SetStepsSinceLastAerial(int newSteps) { stepsSinceLastAerial = newSteps; }
    }
}