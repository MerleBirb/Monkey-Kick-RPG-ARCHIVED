//===== CHARACTER OVERWORLD =====//
/*
5/23/21
Description:
- Abstract class to hold general information for all character logic in the overworld game state.

Author: Merlebirb
*/

using UnityEngine;

public class CharacterOverworld : MonoBehaviour
{
    [SerializeField] protected GameStateData Game;

    protected Vector2 movement;
    [SerializeField] protected float moveSpeed;

    protected IPhysics _physics;
    protected Rigidbody rb;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _physics = GetComponent<IPhysics>();
    }

    public virtual void Update()
    {
        if (!Game.CompareGameState(GameStates.Overworld)) { this.enabled = false; }

        if (_physics != null) _physics.CheckIfGravityShouldApply(rb);
    }

    public virtual void FixedUpdate()
    {
        if (_physics != null)
        {
            ApplyPhysics(moveSpeed);
        }
    }
    public void ApplyPhysics(float speed)
    {
        rb.velocity = _physics.Movement(movement, speed, rb.velocity.y);
            
        _physics.UpdatePhysicsCount(SnapToGround());
        _physics.ClearPhysicsCount();
    }
    public bool SnapToGround()
    {
        float speed = rb.velocity.magnitude;

        if (_physics.GetStepsSinceLastGrounded() > 1 || _physics.GetStepsSinceLastAerial() <= 3)
        {
            return false;
        }

        if (!Physics.Raycast(rb.position, -Vector3.up, out RaycastHit hit, 1f, -1))
        {
            return false;
        }

        float dot = Vector3.Dot(rb.velocity, hit.normal);
        if (dot > 0f)
        {
            rb.velocity = (rb.velocity - hit.normal * dot).normalized * speed;
        }

        return true;
    }
}