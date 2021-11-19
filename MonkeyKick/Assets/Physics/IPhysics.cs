// Merle Roji
// 10/5/21

using UnityEngine;

namespace MonkeyKick.CustomPhysics
{
    public interface IPhysics
    {
        float GetMoveSpeed();
        float GetJumpHeight();
        float GetHeight();
        Rigidbody GetRigidbody();
        void Movement(Vector2 movementInput, float currentSpeed);
        void Movement(Vector2 movementInput, float currentSpeed, Transform faceDirection);
        void ResetMovement();
        void TurnOffGravity();
        void Jump();
        void Jump(float height);
        bool OnGround();
        void ObeyGravity();
    }
}
