// Merle Roji
// 1/12/22

using UnityEngine;
using MonkeyKick.Managers;
using MonkeyKick.Controls;
using MonkeyKick.QualityOfLife;
using UnityEngine.InputSystem;

namespace MonkeyKick.UserInterface
{
    public class OpenOverworldMenu : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private GameObject ui;

        #region CONTROLS

        private PlayerControls _controls;
        private InputAction _start;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            // create new instance of controls
            InputSystem.pollingFrequency = 180;
            _controls = new PlayerControls();

            // set controls
            _start = _controls.Menu.Start;
        }

        private void Update()
        {
            if (_start.triggered)
            {
                if (!ui.activeInHierarchy)
                {
                    MenuQoL.InvokeOnOpenOverworldMenu();
                    ui.SetActive(true);
                }
                else
                {
                    MenuQoL.InvokeOnCloseOverworldMenu();
                    ui.SetActive(false);
                }
            }
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
    }
}
