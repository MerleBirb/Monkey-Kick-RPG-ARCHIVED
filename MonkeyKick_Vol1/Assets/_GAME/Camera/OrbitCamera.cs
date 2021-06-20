//===== ORBIT CAMERA =====//
/*
6/19/21
Description:
- A simpler version of the cinemachine plug in.

Author: Merlebirb
Special thanks to Catlike Coding!
*/

using UnityEngine;
using UnityEngine.InputSystem;

namespace MonkeyKick.CameraTools
{
    [RequireComponent(typeof(Camera))]
    public class OrbitCamera : MonoBehaviour
    {
        private Vector3 _focusPoint;
        private CameraControls _input;
        private InputAction _orbitCamera;
        private Vector2 _orbitInput;
        private Vector2 _orbitAngles = new Vector2(22.5f, 0f);

        [SerializeField] private Transform focus = default;
        [SerializeField, Min(0f)] private float focusRadius = 1f;
        [SerializeField, Range(1f, 20f)] private float distance = 5f;
        [SerializeField, Range(0f, 1f)] private float focusCentering = 0.5f;
        [SerializeField, Range(1f, 360f)] private float rotationSpeed = 90f; // degrees per second
        [SerializeField, Range(-89f, 89f)] private float minVerticalAngle = 0f;
        [SerializeField, Range(-89f, 89f)] private float maxVerticalAngle = 45f;

        private void Awake()
        {
            InputSystem.pollingFrequency = 180;

            _input = new CameraControls();
            _orbitCamera = _input.Overworld.OrbitCamera;
            _orbitCamera.performed += context => _orbitInput = context.ReadValue<Vector2>();

            _focusPoint = focus.position;
            transform.localRotation = Quaternion.Euler(_orbitAngles);
        }

        private void LateUpdate()
        {
            UpdateFocusPoint();

            Quaternion lookRotation;
            if(ManualRotation())
            {
                ConstrainAngles();
                lookRotation = Quaternion.Euler(_orbitAngles);
            }
            else
            {
                lookRotation = transform.localRotation;
            }

            OrbitAndLookAtFocus();
        }

        private void OrbitAndLookAtFocus()
        {
            Quaternion lookRotation = Quaternion.Euler(_orbitAngles);
            Vector3 lookDirection = transform.forward;
            Vector3 lookPosition = _focusPoint - (lookDirection * distance);
            transform.SetPositionAndRotation(lookPosition, lookRotation);
        }

        private bool ManualRotation()
        {
            Vector2 unreversedOrbitInput = new Vector2(_orbitInput.y, -_orbitInput.x); // to unreverse the camera control
            const float e = 0.1f;

            if (_orbitInput.x < -e || _orbitInput.x > e || _orbitInput.y < -e || _orbitInput.y > e)
            {
                _orbitAngles += rotationSpeed * Time.unscaledDeltaTime * unreversedOrbitInput;
                return true;
            }

            return false;
        }

        private void ConstrainAngles()
        {
            _orbitAngles.x = Mathf.Clamp(_orbitAngles.x, minVerticalAngle, maxVerticalAngle);

            if (_orbitAngles.y < 0f) { _orbitAngles.y += 360f; }
            else if (_orbitAngles.y >= 360f) { _orbitAngles.y -= 360f; }
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

                if (focusDistance > 0.01f && focusCentering > 0f) { time = Mathf.Pow(1f - focusCentering, Time.fixedDeltaTime); }
                if (focusDistance > focusRadius) { time = Mathf.Min(time, focusRadius / focusDistance); }

                _focusPoint = Vector3.Lerp(targetPoint, _focusPoint, time);
            }
            else
            {
                _focusPoint = targetPoint;
            }
        }

        private void OnValidate()
        {
            if (maxVerticalAngle < minVerticalAngle)
            {
                maxVerticalAngle = minVerticalAngle;
            }
        }

        private void OnEnable()
        {
            _input.Overworld.Enable();
        }

        private void OnDisable()
        {
            _input.Overworld.Disable();
        }
    }
}
