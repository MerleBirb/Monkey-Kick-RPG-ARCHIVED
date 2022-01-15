// Merle Roji
// 1/15/22

using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.Controls;

namespace MonkeyKick.UserInterface
{
    public class AdvanceInput : MonoBehaviour
    {
        #region LUA FIELDS

        private LuaEnvironment _lua;

        #endregion

        #region CONTROLS

        private PlayerControls _controls;
        private InputAction _select;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            // create a new instance of controls
            InputSystem.pollingFrequency = 180;
            _controls = new PlayerControls();

            // set controls
            _select = _controls.Menu.Select;
        }

        private void Start()
        {
            _lua = FindObjectOfType<LuaEnvironment>();
        }

        private void Update()
        {
            CheckInput();
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

        #region INPUT METHODS

        /// <summary>
        /// Checks if the player has pressed certain buttons.
        /// </summary>
        private void CheckInput()
        {
            if (_select.triggered)
            {
                _lua.AdvanceScript();
            }
        }

        #endregion
        
    }
}
