// Merle Roji
// 10/6/21

using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.Controls;
using MonkeyKick.CustomPhysics;
using MonkeyKick.Managers;

namespace MonkeyKick.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        #region CONTROLS

        private CameraControls _controls;
        private InputAction _orbitX;
        private float _orbitCameraX;

        #endregion

        #region CAMERA VALUES

        public Transform Target;
        [Header("Angle that determines the Y perspective of the camera")]
        public float AngleY = 30f;
        [Header("Controls the smoothness and sensitivity of the camera controls")]
        public float RotationSmoothing = 10f;
        public float RotationSensitivity = 7f;
        [Header("Distance from the player")]
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

            // reset _oldRotation and _angle.y
            _oldRotation = transform.rotation;
            _angle.y = AngleY;
        }

        private void Update()
        {
            if (gameManager.GameState != GameStates.Overworld) return;

            CheckInput();
        }

        private void LateUpdate()
        {
            if (gameManager.GameState != GameStates.Overworld) return;

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

        #region CAMERA CONTROL METHODS

        /// <summary>
        /// Checks the input via the controls from the player.
        /// </summary>
        private void CheckInput()
        {
            _angle.x += _orbitCameraX * RotationSensitivity * Time.deltaTime;
            PhysicsQoL.ClampAngle180(ref _angle);
        }

        /// <summary>
        /// Rotates the camera based on inputs from the player.
        /// </summary>
        private void RotateCamera()
        {
            if (Target) // if the target exists
            {
                Quaternion angleRotation = Quaternion.Euler(_angle.y, _angle.x, 0); // swap x and y
                Quaternion currentRotation = Quaternion.Lerp(_oldRotation, angleRotation, Time.deltaTime * RotationSmoothing);

                _oldRotation = currentRotation;

                transform.position = Target.position - currentRotation * Vector3.forward * Distance;
            }
        }

        #endregion
    }
}
