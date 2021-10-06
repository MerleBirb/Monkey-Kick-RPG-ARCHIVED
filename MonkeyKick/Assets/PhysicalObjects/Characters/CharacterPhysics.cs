// Merle Roji
// 10/5/21

using UnityEngine;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public class CharacterPhysics : MonoBehaviour, IPhysics
    {
        public void Movement(Vector2 movementInput, Rigidbody rigidbody, float currentSpeed)
        {
            // apply movement speed to the movement input
            float xMove = movementInput.x * currentSpeed;
            float zMove = movementInput.y * currentSpeed;

            rigidbody.velocity = new Vector3(xMove, rigidbody.velocity.y, zMove); // set velocity
        }

        public void Movement(Vector2 movementInput, Rigidbody rigidbody, float currentSpeed, Transform faceDirection)
        {
            Vector3 forward = faceDirection.forward; // save forward direction of the pov
            forward.y = 0f;
            forward.Normalize();

            Vector3 right = faceDirection.right;
            right.y = 0f;
            right.Normalize();

            Vector3 desiredVelocity = ((forward * movementInput.y) + (right * movementInput.x)) * currentSpeed;
            rigidbody.velocity = new Vector3(desiredVelocity.x, rigidbody.velocity.y, desiredVelocity.z);
        }
    }
}
