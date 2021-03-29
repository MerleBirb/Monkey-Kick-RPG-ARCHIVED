using Godot;
using System;

namespace Merlebirb.PlayableCharacter
{
    //===== PLAYER MOVEMENT SCRIPT =====//
    /*
    by: Merlebirb 3/22/2021

    Description: The Player's movement and physics logic. Allows the player to move, jump, fall, etc.

    P.S. thank you @ Miziziziz for the awesome 3d movement tutorial, this script is based off of his.
    */

    public class PlayerMovement : Spatial
    {
            #region Constants

            const string FORWARDS = "moveForward";
            const string BACKWARDS = "moveBack";
            const string RIGHT = "moveRight";
            const string LEFT = "moveLeft";

            #endregion

            #region Physics variables

            private int xMove;
            private int zMove;
            private int lastXMove = 0;
            private int lastZMove = 0;
            [Export] public int moveSpeed = 12;
            [Export] public int jumpForce = 30;
            [Export] public float gravity = 0.98f;
            [Export] public float maxFallSpeed = 30f;
            private float yVel = 0f;
            private bool isGrounded;
            private bool isMoving;
            private bool isJumping;

            #endregion

            #region Godot Components

            private KinematicBody kb;
            private AnimationPlayer anims;
            private AnimationTree animTree;
            private AnimationNodeStateMachinePlayback animState;

            #endregion

        // Initializes the node.
        public override void _Ready()
        {
            kb = GetParent<KinematicBody>();
            anims = GetNode<AnimationPlayer>("Graphics/AnimationPlayer");
            animTree = GetNode<AnimationTree>("Graphics/AnimationTree");
            animState = (AnimationNodeStateMachinePlayback)animTree.Get("parameters/playback");
        }  

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(float delta)
        {
            GetInput();

        }

        // Called every frame. 'delta' is the elapsed time since the previous frame. Better for physics.
        public override void _PhysicsProcess(float delta)
        {
            Movement();
            Gravity();
            Animations();
        }
      
        // Gets the input from the player
        private void GetInput()
        {
            // movement with no acceleration
            if (Input.IsActionPressed(FORWARDS))
            {
                zMove = -1;
            }
            else if (Input.IsActionPressed(BACKWARDS))
            {
                zMove = 1;
            }
            else
            {
                zMove = 0;
            }
            
            if (Input.IsActionPressed(RIGHT))
            {
                xMove = 1;
            }
            else if (Input.IsActionPressed(LEFT))
            {
                xMove = -1;
            }
            else
            {
                xMove = 0;
            }
        }

        // movement logic
        private void Movement()
        {
            Vector3 movement = new Vector3();

            movement.x = (float)xMove;
            movement.z = (float)zMove;
            movement = movement.Normalized();
            movement *= (float)moveSpeed;

            movement.y = yVel;
            kb.MoveAndSlide(movement, Vector3.Up, true);

            if (Mathf.Abs(movement.x) > 0 || Mathf.Abs(movement.z) > 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }

        // gravity logic
        private void Gravity()
        {
            isGrounded = kb.IsOnFloor();

            yVel -= gravity;

            GetJump();

            if (isGrounded && yVel <= 0f)
            {
                yVel = -0.1f;
            }

            if (yVel < -maxFallSpeed)
            {
                yVel = -maxFallSpeed;
            }
        }

        // jumping logic
        private void GetJump()
        {
            if (isGrounded && Input.IsActionJustPressed("jump"))
            {
                isJumping = true;
                yVel = jumpForce;
            }
        }

        // animations
        private void Animations()
        {
            if (isMoving)
            {
                lastXMove = xMove;
                lastZMove = zMove;
            }

            // animation
            if (!isMoving) // idle
            {
                Vector2 idleAnimVector = new Vector2((float)lastXMove, (float)lastZMove);
                animTree.Set("parameters/Idle/blend_position", idleAnimVector);
                animState.Travel("Idle");
            }
            else // running
            {
                Vector2 runAnimVector = new Vector2((float)xMove, (float)zMove);
                animTree.Set("parameters/Run/blend_position", runAnimVector);
                animState.Travel("Run");
            }
        }
    }
}
    
