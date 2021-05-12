//===== PLAYER MOVEMENT =====//
/*
5/11/21
Description: 
- Contains player physics and movement logic for the overworld scenes.

*/

using UnityEngine;
using UnityEngine.InputSystem;

namespace Merlebirb.Characters
{

    public class PlayerMovement : MonoBehaviour
    {
        #region PHYSICS

        private Vector2 movement; // stores the input of the player to a vector 2
        private Vector2 lastMovement;

        [SerializeField] private float moveSpeed;
        [SerializeField] private float sprintSpeed; // moveSpeed while sprint is pressed

        [SerializeField] private float jumpHeight;

        [SerializeField] private float radius = 0.55f; // for raycasts, ground check, etc
        [SerializeField] public LayerMask groundLayer;
        private bool OnGround => Physics.Raycast(transform.position, Vector3.down, radius, groundLayer);
        private int stepsSinceLastGrounded = 0;
        private int stepsSinceLastJumped = 0;

        #endregion

        #region COMPONENTS

        private PlayerInput input; // stores the controls
        private Rigidbody rb;
        private Animator animator;

        #endregion
        
        #region CONTROLS

        const string MOVE = "Move";
        const string SPRINT = "Sprint";
        const string JUMP = "Jump";

        private bool isMoving = false;
        private bool hasPressedJump = false;
        private bool isJumping = false;
        private bool hasPressedSprint = false;
        private bool isSprinting = false;

        #endregion

        private void Awake()
        {
            InputSystem.pollingFrequency = 180;
        }

        private void MovementStart()
        {
            input = GetComponent<PlayerInput>();
            rb = GetComponent<Rigidbody>();
        }

        private void MovementUpdate()
        {
            CheckForPlayerInput();
        }

        private void MovementFixedUpdate()
        {

        }

        private void CheckForPlayerInput()
        {
            input.actions.FindAction(MOVE).performed += context => movement = context.ReadValue<Vector2>();
            hasPressedJump |= input.actions.FindAction("Jump").WasPressedThisFrame();
        }

        private void OnEnable()
        {
            //input.enabled = true;
        }
        
        private void OnDisable()
        {
            //input.enabled = false;
        }
    }
    
}
