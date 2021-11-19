// Merle Roji
// 10/6/21

using System;
using UnityEngine;
using MonkeyKick.Managers;
using MonkeyKick.QualityOfLife;
using MonkeyKick.CustomPhysics;
using MonkeyKick.Cameras;

namespace MonkeyKick.RPGSystem.Characters
{
    public class SpriteBillboard : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        #region CAMERA

        public CameraDirection CamDirection;
        private Camera _mainCam;
        private Facing _facing = Facing.Down;
        private int _offset = 4;

        #endregion

        #region CHARACTER

        private Animator _anim;
        private CharacterMovement _character;

        #endregion

        #region ANIMATIONS

        private string _currentState = "";

        // Idle
        const string IDLE_UP = "Idle_up";
        const string IDLE_UPRIGHT = "Idle_upright";
        const string IDLE_RIGHT = "Idle_right";
        const string IDLE_DOWNRIGHT = "Idle_downright";
        const string IDLE_DOWN = "Idle_down";
        const string IDLE_DOWNLEFT = "Idle_downleft";
        const string IDLE_LEFT = "Idle_left";
        const string IDLE_UPLEFT = "Idle_upleft";

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            _anim = GetComponent<Animator>();
            _character = GetComponentInParent<CharacterMovement>();
            _mainCam = Camera.main;
            if (!CamDirection) CamDirection = _mainCam.GetComponent<CameraDirection>();
        }

        private void Update()
        {
            Billboard();
            RotateSprite();
            if (gameManager.GameState == GameStates.Overworld) OverworldAnimations();
        }

        #endregion

        #region SPRITE METHODS

        /// <summary>
        /// Sets the x-rotation of the sprite to face the camera.
        /// </summary>
        private void Billboard()
        {
            transform.rotation = _mainCam.transform.rotation;
        }

        /// <summary>
        /// Rotates the sprite around the y-axis with the camera and sets the facing value.
        /// </summary>
        private void RotateSprite()
        {
            Vector2 movement = _character.CurrentVelocity; // save the movement
            movement.Normalize(); // the vector must add up to 1
            float roundedAngle = (float)Math.Round(PhysicsQoL.AngleTo(Vector2.zero, movement), 1); // angle of the vector from (0, 0) and round
            int angleToFace = Convert.ToInt32((roundedAngle / 45f) % 7.5f); // algorithm to convert angle to 8 directions

            if (movement.x != 0f || movement.y != 0f)
            {
                _facing = angleToFace + CamDirection.Facing; // reset the offset if moving
            }

            _offset = _facing - CamDirection.Facing;
            if (_offset < 0) _offset += 8; // if offset goes negative, reset
            if (_offset > 7) _offset -= 8;     
        }

        /// <summary>
        /// Animations for the overworld.
        /// </summary>
        private void OverworldAnimations() // only in overworld
        {
            Facing direction = (Facing)_offset;

            // change animation based on direction of camera
            switch (direction)
            {
                case Facing.Up:
                    AnimationQoL.ChangeAnimation(_anim, _currentState, IDLE_UP);
                    break;
                case Facing.UpRight:
                    AnimationQoL.ChangeAnimation(_anim, _currentState, IDLE_UPRIGHT);
                    break;
                case Facing.Right:
                    AnimationQoL.ChangeAnimation(_anim, _currentState, IDLE_RIGHT);
                    break;
                case Facing.DownRight:
                    AnimationQoL.ChangeAnimation(_anim, _currentState, IDLE_DOWNRIGHT);
                    break;
                case Facing.Down:
                    AnimationQoL.ChangeAnimation(_anim, _currentState, IDLE_DOWN);
                    break;
                case Facing.DownLeft:
                    AnimationQoL.ChangeAnimation(_anim, _currentState, IDLE_DOWNLEFT);
                    break;
                case Facing.Left:
                    AnimationQoL.ChangeAnimation(_anim, _currentState, IDLE_LEFT);
                    break;
                case Facing.UpLeft:
                    AnimationQoL.ChangeAnimation(_anim, _currentState, IDLE_UPLEFT);
                    break;
            }
        }

        #endregion
    }
}
