// Merle Roji
// 10/6/21

using System;
using UnityEngine;
using MonkeyKick.QualityOfLife;
using MonkeyKick.Cameras;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public class SpriteBillboard : MonoBehaviour
    {
        public CameraDirection CamDirection;
        private Camera _mainCam;
        private Facing _facing = Facing.Down;
        private Animator _anim;
        private CharacterMovement _character;
        private int _offset = 4;

        #region ANIMATIONS

        private string _currentState = "";

        // Idle
        const string IDLE_UP = "Idle0";
        const string IDLE_UPRIGHT = "Idle1";
        const string IDLE_RIGHT = "Idle2";
        const string IDLE_DOWNRIGHT = "Idle3";
        const string IDLE_DOWN = "Idle4";
        const string IDLE_DOWNLEFT = "Idle5";
        const string IDLE_LEFT = "Idle6";
        const string IDLE_UPLEFT = "Idle7";

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
        }

        #endregion

        #region METHODS

        private void Billboard()
        {
            transform.rotation = _mainCam.transform.rotation;
        }

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
