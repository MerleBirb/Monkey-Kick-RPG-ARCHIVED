// Merle Roji
// 10/5/21

using UnityEngine;

namespace MonkeyKick.PhysicalObjects
{
    public interface IPhysics
    {
        void Movement(Vector2 movementInput, Rigidbody rigidbody, float currentSpeed);
        void Movement(Vector2 movementInput, Rigidbody rigidbody, float currentSpeed, Transform faceDirection);
    }
}
