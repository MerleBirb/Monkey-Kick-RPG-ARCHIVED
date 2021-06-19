//===== ORBIT CAMERA =====//
/*
6/19/21
Description:
- A simpler version of the cinemachine plug in.

Author: Merlebirb
Special thanks to Catlike Coding!
*/

using UnityEngine;

namespace MonkeyKick.CameraTools
{
    [RequireComponent(typeof(Camera))]
    public class OrbitCamera : MonoBehaviour
    {
        private Vector3 _focusPoint;
        private Vector2 orbitAngles = new Vector2(22.5f, 0f);

        [SerializeField] private Transform focus = default;
        [SerializeField, Min(0f)] private float focusRadius = 1f;
        [SerializeField, Range(1f, 20f)] private float distance = 5f;
        [SerializeField, Range(0f, 1f)] private float focusCentering = 0.5f;
        [SerializeField, Range(1f, 360f)] private float rotationSpeed = 90f; // degrees per second

        private void Awake()
        {
            _focusPoint = focus.position;
        }

        private void LateUpdate()
        {
            UpdateFocusPoint();
            OrbitAndLookAtFocus();
        }

        private void OrbitAndLookAtFocus()
        {
            Quaternion lookRotation = Quaternion.Euler(orbitAngles);
            Vector3 lookDirection = transform.forward;
            Vector3 lookPosition = _focusPoint - (lookDirection * distance);
            transform.SetPositionAndRotation(lookPosition, lookRotation);
        }

        /// <summary>
        /// If the focus radius is positive, check whether the distance between the target and current focus points is greater than the radius.
        /// If so, pull the focus toward the target until the distance matches the radius.
        /// This can be done by interpolating from target point to current point, using the radius divided by current distance as the interpolator.
        /// To make this motion appear more subtle and organic we can pull back slower as the
        /// focus approaches the center by halving a starting distance each second.
        ///<summary/>
        private void UpdateFocusPoint()
        {
            Vector3 targetPoint = focus.position;

            if (focusRadius > 0f)
            {
                float focusDistance = Vector3.Distance(targetPoint, _focusPoint);
                float time = 1f;

                if (focusDistance > 0.01f && focusCentering > 0f) { time = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime); }
                if (focusDistance > focusRadius) { time = Mathf.Min(time, focusRadius / focusDistance); }

                _focusPoint = Vector3.Lerp(targetPoint, _focusPoint, time);
            }
            else
            {
                _focusPoint = targetPoint;
            }
        }
    }
}
