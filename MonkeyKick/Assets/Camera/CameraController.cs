// Merle Roji
// 10/6/21

using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.Controls;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.Cameras
{
    public class CameraController : MonoBehaviour
    {
        #region CONTROLS

        private CameraControls _controls;
        private InputAction _orbitX;
        private float _orbitCameraX;

        #endregion

        #region CAMERA

        public Transform Target;
        public float AngleY = 30f;
        public float RotationSmoothing = 10f;
        public float RotationSensitivity = 7f;
        public float Distance = 5f;

        private Vector3 _angle = new Vector3();
        private Quaternion _oldRotation = new Quaternion();

        public Vector2 CurrentRotation { get { return _angle; } }

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            InputSystem.pollingFrequency = 180;

            // new instance of camera controls
            _controls = new CameraControls();
            _orbitX = _controls.Overworld.RotationX;
            _orbitX.performed += ctx => _orbitCameraX = ctx.ReadValue<float>();
        }

        private void Start()
        {
            _oldRotation = transform.rotation;
            _angle.y = AngleY;
        }

        private void Update()
        {
            CheckInput();
        }

        private void LateUpdate()
        {
            RotateCamera();
        }

        private void OnEnable()
        {
            _controls.Overworld.Enable();
        }

        private void OnDisable()
        {
            _controls.Overworld.Disable();
        }

        #endregion

        #region METHODS

        private void CheckInput()
        {
            PhysicsQoL.ClampAngle(ref _angle);
            _angle.x += _orbitCameraX * RotationSensitivity;
        }

        private void RotateCamera()
        {
            if (Target)
            {
                Quaternion angleRotation = Quaternion.Euler(_angle.y, _angle.x, 0); // swap x and y
                Quaternion currentRotation = Quaternion.Lerp(_oldRotation, angleRotation, Time.deltaTime * RotationSmoothing);

                _oldRotation = currentRotation;

                transform.position = Target.position - currentRotation * Vector3.forward * Distance;
                transform.LookAt(Target.position, Vector3.up);
            }
        }

        #endregion
    }
}
