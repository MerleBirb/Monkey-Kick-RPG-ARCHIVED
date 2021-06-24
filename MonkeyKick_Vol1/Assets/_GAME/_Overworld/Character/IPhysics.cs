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
        Vector3 Movement(Vector2 movement, float currentSpeed, float yVal);
        Vector3 Movement(Vector2 movement, float currentSpeed, float yVal, Transform newDirection);
        bool OnGround();
        void CheckIfGravityShouldApply(Rigidbody rb);
        void UpdatePhysicsCount(bool additionalChecks);
        void ClearPhysicsCount();
        int GetStepsSinceLastGrounded();
        void SetStepsSinceLastGrounded(int newSteps);
        int GetStepsSinceLastAerial();
        void SetStepsSinceLastAerial(int newSteps);
    }
}