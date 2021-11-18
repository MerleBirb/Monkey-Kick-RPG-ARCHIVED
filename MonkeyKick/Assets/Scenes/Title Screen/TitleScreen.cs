// Merle Roji
// 11/17/21

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using MonkeyKick.QualityOfLife;
using MonkeyKick.Controls;

namespace MonkeyKick.UserInterface
{
    public class TitleScreen : MonoBehaviour
    {
        #region CONTROLS

        private PlayerControls _controls;
        private InputAction _start;
        private InputAction _select;

        #endregion

        #region SCENES

        [SerializeField] private SceneField firstScene; // first scene that the title screen transitions into

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            // create new instance of controls
            InputSystem.pollingFrequency = 180;
            _controls = new PlayerControls();

            _start = _controls.Menu.Start;
            _select = _controls.Menu.Select;
        }

        private void Update()
        {
            CheckIfGameStarted();
        }

        private void OnEnable()
        {
            _controls?.Menu.Enable();
        }

        private void OnDisable()
        {
            _controls?.Menu.Disable();
        }

        #endregion

        #region METHODS

        private void CheckIfGameStarted()
        {
            if (_start.triggered)
            {
                OnStartGame();
            }
        }

        public void OnStartGame()
        {
            SceneManager.LoadScene(firstScene);
        }

        #endregion
    }
}
