// Merle Roji
// 1/12/22

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using MonkeyKick.References;
using MonkeyKick.QualityOfLife;
using UnityEngine.InputSystem;
using MonkeyKick.Controls;

namespace MonkeyKick.UserInterface
{
    public class OverworldOptions : MonoBehaviour
    {
        [SerializeField] private IntReference menuChoice;
        [SerializeField] private List<Transform> menuTexts;
        [SerializeField] private int xOffset;
        [SerializeField] private Image selectorPrefab;
        private Image _selector;

        #region CONTROLS

        private PlayerControls _controls;
        private bool _movePressed = false;
        private Vector2 _movement;
        protected InputAction _move;
        private InputAction _start;
        private InputAction _select;
        private InputAction _cancel;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            // create new instance of controls
            InputSystem.pollingFrequency = 180;
            _controls = new PlayerControls();

            // set controls
            _move = _controls.Menu.Move;
            _move.performed += context => _movement = context.ReadValue<Vector2>();

            _start = _controls.Menu.Start;
            _select = _controls.Menu.Select;
            _cancel = _controls.Menu.Cancel;
        }

        private void OnEnable()
        {
            _controls?.Menu.Enable();

            _selector = Instantiate(selectorPrefab, transform);
            _selector.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
        
        private void OnDisable()
        {
            _controls?.Menu.Disable();

            Destroy(_selector);
        }

        private void Update()
        {
            ChooseMenuOption();
        }

        #endregion

        #region MENU METHODS

        private void ChooseMenuOption()
        {
            // keeps the selector in bounds
            if (menuChoice.Variable.Value < 0) { menuChoice.Variable.Value = menuTexts.Count - 1; }
            else if (menuChoice.Variable.Value > menuTexts.Count - 1) { menuChoice.Variable.Value = 0; }

            // sets the position of the selector
            MenuQoL.SelectMenu(menuTexts, _selector, menuChoice.Variable.Value, OffsetChoice.XAxis, xOffset);
            // scrolls the selector up and down
            MenuQoL.ScrollThroughMenu(ref _movePressed, ref menuChoice.Variable.Value, _movement);
        }

        #endregion
    }
}
