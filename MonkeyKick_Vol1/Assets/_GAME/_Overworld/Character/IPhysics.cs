//===== INTERFACE PHYSICS =====//
/*
5/22/21
Description:
- An interface that holds basic physics

Author: Merlebirb
*/

using UnityEngine;

namespace MonkeyKick.Overworld
{
    public interface IPhysics
    {
        Vector3 Movement(Vector2 _movement, float _currentSpeed, float _yVal);
        Vector3 Movement(Vector2 _movement, float _currentSpeed, float _yVal, Transform newDirection);
        bool OnGround();
        void CheckIfGravityShouldApply(Rigidbody _rb);
        void UpdatePhysicsCount(bool _additionalChecks);
        void ClearPhysicsCount();
        int GetStepsSinceLastGrounded();
        void SetStepsSinceLastGrounded(int _newSteps);
        int GetStepsSinceLastAerial();
        void SetStepsSinceLastAerial(int _newSteps);
    }
}