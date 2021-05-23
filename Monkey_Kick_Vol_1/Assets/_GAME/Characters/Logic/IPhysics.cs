//===== IPHYSICS =====//
/*
5/22/21
Description:
- An interface that holds basic physics

Author: Merlebirb
*/

using UnityEngine;

public interface IPhysics
{
    Vector3 Movement(Vector2 _movement, float currentSpeed, float yVal);
    bool OnGround();
    void CheckIfGravityShouldApply(Rigidbody _rb);
    void UpdatePhysicsCount(bool additionalChecks);
    void ClearPhysicsCount();
    int GetStepsSinceLastGrounded();
    void SetStepsSinceLastGrounded(int newSteps);
    int GetStepsSinceLastAerial();
    void SetStepsSinceLastAerial(int newSteps);
}