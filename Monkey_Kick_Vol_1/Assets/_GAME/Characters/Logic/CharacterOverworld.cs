//===== CHARACTER OVERWORLD =====//
/*
5/23/21
Description:
- Abstract class to hold general information for all character logic in the overworld game state.

Author: Merlebirb
*/

using UnityEngine;
using Merlebirb.Managers;

namespace Merlebirb.CharacterLogic
{
    public class CharacterOverworld : MonoBehaviour
    {
        [SerializeField] protected GameStateData Game;

        protected Vector2 movement;
        [SerializeField] protected float moveSpeed;

        protected IPhysics physics;
        protected Rigidbody rb;

        public virtual void Awake()
        {
            if (!Game.CompareGameState(GameStates.Overworld)) { this.enabled = false; }
            else
            {
                rb = GetComponent<Rigidbody>();
                physics = GetComponent<IPhysics>();
            }
        }

        public virtual void Update()
        {
            if (!Game.CompareGameState(GameStates.Overworld)) { this.enabled = false; }

            physics?.CheckIfGravityShouldApply(rb);
        }

        public virtual void FixedUpdate()
        {
            if (physics != null)
            {
                ApplyPhysics(moveSpeed);
            }
        }
        public void ApplyPhysics(float _speed)
        {
            rb.velocity = physics.Movement(movement, _speed, rb.velocity.y);

            physics.UpdatePhysicsCount(SnapToGround());
            physics.ClearPhysicsCount();
        }
        public bool SnapToGround()
        {
            float _speed = rb.velocity.magnitude;

            if (physics.GetStepsSinceLastGrounded() > 1 || physics.GetStepsSinceLastAerial() <= 3)
            {
                return false;
            }

            if (!Physics.Raycast(rb.position, -Vector3.up, out RaycastHit hit, 1f, -1))
            {
                return false;
            }

            float _dot = Vector3.Dot(rb.velocity, hit.normal);
            if (_dot > 0f)
            {
                rb.velocity = (rb.velocity - (hit.normal * _dot)).normalized * _speed;
            }

            return true;
        }
    }
}