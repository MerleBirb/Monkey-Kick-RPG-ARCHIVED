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

    public class PlayerMovement : KinematicBody
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

            private Node camera;
            private AnimationPlayer anims;

            #endregion

        // Initializes the node.
        public override void _Ready()
        {
            camera = GetNode<Spatial>("CamBase");
            anims = GetNode<AnimationPlayer>("Graphics/AnimationPlayer");
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
            Vector3 movementVector = new Vector3();

            movementVector.x = (float)xMove;
            movementVector.z = (float)zMove;
            movementVector = movementVector.Normalized();
            movementVector *= (float)moveSpeed;

            movementVector.y = yVel;
            MoveAndSlide(movementVector, Vector3.Up);

            if (Mathf.Abs(movementVector.x) > 0 || Mathf.Abs(movementVector.z) > 0)
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
            isGrounded = IsOnFloor();

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
    }
}
    
