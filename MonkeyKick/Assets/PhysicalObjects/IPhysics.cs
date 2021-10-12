// Merle Roji
// 10/5/21

using UnityEngine;

namespace MonkeyKick.PhysicalObjects
{
    public interface IPhysics
    {
        float GetMoveSpeed();
        float GetJumpHeight();
        float GetHeight();
        void Movement(Vector2 movementInput, float currentSpeed);
        void Movement(Vector2 movementInput, float currentSpeed, Transform faceDirection);
        void TurnOffGravity();
        void Jump();
        bool OnGround();
        void ObeyGravity();
        void CountPhysicsSteps();
        bool SnapToGround();
    }
}