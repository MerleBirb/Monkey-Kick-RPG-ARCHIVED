// Merle Roji
// 10/6/21

using UnityEngine;
using MonkeyKick;

namespace MonkeyKick.Cameras
{
    public class CameraDirection : MonoBehaviour
    {
        #region CAMERA

        protected CameraController _camera;

        #endregion

        #region FACING

        protected Facing _facing = Facing.Down;

        public virtual Facing Facing { get { return _facing; } }
        public virtual Vector2 Angle { get { return _camera.CurrentRotation; } }

        #endregion

        #region UNITY METHODS

        public virtual void Awake()
        {
            _camera = GetComponent<CameraController>();
        }

        public virtual void LateUpdate()
        {
            CheckFace();
        }

        #endregion

        #region METHODS

        public virtual void CheckFace()
        {
            float rX = _camera.CurrentRotation.x;
            float x = Mathf.Abs(rX);

            if (x < 22.5f) _facing = Facing.Up;
            else if (x < 67.5f) _facing = rX < 0 ? Facing.UpLeft : Facing.UpRight; // if less than 0, left, if more than 0, right
            else if (x < 112.5f) _facing = rX < 0 ? Facing.Left : Facing.Right;
            else if (x < 157.5f) _facing = rX < 0 ? Facing.DownLeft : Facing.DownRight;
            else _facing = Facing.Down;
        }

        #endregion
    }
}
