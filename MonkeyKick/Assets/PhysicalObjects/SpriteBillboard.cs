// Merle Roji
// 10/6/21

using UnityEngine;
using MonkeyKick.QualityOfLife;
using MonkeyKick.Cameras;

namespace MonkeyKick.PhysicalObjects
{
    public class SpriteBillboard : MonoBehaviour
    {
        public CameraDirection CamDirection;
        private Camera _mainCam;
        private Facing _facing = Facing.Down;
        private Animator _anim;

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
            int offset = _facing - CamDirection.Facing;
            if (offset < 0) offset += 8; // if offset goes negative, reset

            Facing direction = (Facing)offset;

            // change animation based on direction
            switch(direction)
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
