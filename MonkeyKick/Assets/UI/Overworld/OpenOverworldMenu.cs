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

            // add functions to menu event
            MenuQoL.OnOpenOverworldMenu += Pause;
            MenuQoL.OnCloseOverworldMenu += Resume;
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

            MenuQoL.OnOpenOverworldMenu -= Pause;
            MenuQoL.OnCloseOverworldMenu -= Resume;
        }

        #endregion

        #region MENU METHODS

        // checks if the menu button was pressed
        private void CheckInput()
        {
            if (_start.triggered)
            {
                if (!ui.activeInHierarchy) MenuQoL.InvokeOnOpenOverworldMenu();
                else MenuQoL.InvokeOnCloseOverworldMenu();
            }
        }

        private void Pause()
        {
            gameManager.GameState = GameStates.Menu;
            ui.SetActive(true);
            Time.timeScale = 0f;
        }

        private void Resume()
        {
            gameManager.GameState = GameStates.Overworld;
            ui.SetActive(false);
            Time.timeScale = 1f;
        }

        #endregion
    }
}
