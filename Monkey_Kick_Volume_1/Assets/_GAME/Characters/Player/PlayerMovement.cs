using UnityEngine;
using UnityEngine.InputSystem;

namespace Merlecode.Player
{
    /// ----- PLAYER MOVEMENT ----- ///
    /* Description:
     * 
     * Manipulates the rigidbody of the player using the input system to allow the player
     * to move.
     * 
     * - Merle, 2/13/2021
     * 
     * */

    public class PlayerMovement : MonoBehaviour
    {
        #region PHYSICS
        [SerializeField] private Vector2 movement = new Vector2(0f, 0f); // stores the movement inputs of the player
        [SerializeField, Min(0.1f)] private float moveSpeed = 1f;
        [SerializeField, Min(0.1f)] private float jumpHeight = 1f;
        private bool inputJump = false;
        [SerializeField] private float radius = 0.55f;
        public LayerMask groundLayer;
        [SerializeField] private bool OnGround => Physics.Raycast(transform.position, Vector3.down, radius, groundLayer);
        private int stepsSinceLastGrounded = 0;
        private int stepsSinceLastJumped = 0;

        #endregion

        #region COMPONENTS
        private PlayerInput playerInput;
        private Rigidbody rb;

        #endregion

        /// <summary>
        /// Start() code but in a new function, added to Start() in PlayerManager
        /// </summary>
        public void MovementStart() 
        {
            rb = GetComponent<Rigidbody>();
            playerInput = GetComponent<PlayerInput>();
        }

        /// <summary>
        /// Awake() code but in a new function, added to Awake() in PlayerManager
        /// </summary>
        public void MovementAwake()
        {
            InputSystem.pollingFrequency = 180;
        }

        /// <summary>
        /// Update() code but in a new function, added to Update() in PlayerManager
        /// </summary>
        public void MovementUpdate() 
        {
            CheckInput();
            CheckGround();
        }

        /// <summary>
        /// FixedUpdate() code but in a new function. added to FixedUpdate() in PlayerManager
        /// </summary>
        public void MovementFixedUpdate() 
        {
            Movement();
            PhysicsUpdate();
            ClearPhysics();
        }

        /// <summary>
        /// Checks for input from the player
        /// </summary>
        private void CheckInput()
        {
            playerInput.actions.FindAction("Move").performed += ctx => movement = ctx.ReadValue<Vector2>(); // ctx = context
            inputJump |= playerInput.actions.FindAction("Jump").WasPressedThisFrame();
        }

        /// <summary>
        /// Makes the player move 
        /// </summary>
        private void Movement()
        {
            float xMove = movement.x * moveSpeed;
            float zMove = movement.y * moveSpeed; // convert the y into a z to clear up confusion

            Vector3 movement3D = new Vector3(xMove, rb.velocity.y, zMove);
            rb.velocity = movement3D;

            CheckJump();
        }

        /// <summary>
        /// checks to see if the jump is valid / available
        /// </summary>
        private void CheckJump()
        {
            if (inputJump)
            {
                Jump();

                inputJump = false;
            }
        }

        /// <summary>
        /// the player jumps upward
        /// </summary>
        private void Jump()
        {
            if (OnGround)
            {
                stepsSinceLastJumped = 0;
                rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            }
        }

        /// <summary>
        /// prevent player from sliding and other annoying gravity bugs 
        /// </summary>
        private void CheckGround() 
        {
            if (OnGround)
            {
                rb.useGravity = false;
            }
            else
            {
                rb.useGravity = true;
            }
        }

        /// <summary>
        /// updates important physics variables
        /// </summary>
        private void PhysicsUpdate()
        {
            stepsSinceLastGrounded++;
            stepsSinceLastJumped++;

            Debug.Log("Steps since last grounded: " + stepsSinceLastGrounded);
            Debug.Log("Steps since last jumped: " + stepsSinceLastJumped);

            if (OnGround || SnapToGround())
            {
                stepsSinceLastGrounded = 0;
            }
        }

        /// <summary>
        /// Snaps the player to the ground if they go off a ramp while grounded
        /// </summary>
        private bool SnapToGround()
        {
            float speed = rb.velocity.magnitude;

            if (stepsSinceLastGrounded > 1 || stepsSinceLastJumped <= 2)
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

        /// <summary>
        /// sets all variables back to their defaults to prevent memory leaks 
        /// </summary>
        private void ClearPhysics()
        {
            if (stepsSinceLastGrounded > 10)
            {
                stepsSinceLastGrounded = 10;
            }

            if(stepsSinceLastJumped > 10)
            {
                stepsSinceLastJumped = 10;
            }

            inputJump = false;
        }
    }
}
