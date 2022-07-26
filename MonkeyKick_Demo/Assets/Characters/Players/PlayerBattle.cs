// Merle Roji 7/12/22

using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.Managers.TurnSystem;

namespace MonkeyKick.Characters.Players
{
    /// <summary>
    /// Handles battle logic for players.
    /// 
    /// Notes:
    /// 
    /// </summary>
    public class PlayerBattle : CharacterBattle
    {
        private PlayerControls _controls;
        protected InputAction _move;
        private InputAction _select;
        private InputAction _jump;
        [HideInInspector] public bool PressedJump;
        private Vector2 _movement;
        private bool _movePressed = false;
        private InputAction _buttonNorth;
        public InputAction ButtonNorth { get => _buttonNorth; }
        [HideInInspector] public bool PressedButtonNorth;
        private InputAction _buttonEast;
        public InputAction ButtonEast { get => _buttonEast; }
        [HideInInspector] public bool PressedButtonEast;
        private InputAction _buttonSouth;
        public InputAction ButtonSouth { get => _buttonSouth; }
        [HideInInspector] public bool PressedButtonSouth;
        private InputAction _buttonWest;
        public InputAction ButtonWest { get => _buttonWest; }
        [HideInInspector] public bool PressedButtonWest;

        [SerializeField] private IntReference _menuChoice;

        protected override void Awake()
        {
            base.Awake();

            // create new instance of controls
            InputSystem.pollingFrequency = 180;
            _controls = new PlayerControls();

            // set controls
            _move = _controls.Battle.Move;
            _move.performed += context => _movement = context.ReadValue<Vector2>();

            _select = _controls.Battle.Select;
            _jump = _controls.Battle.Jump;
            _buttonNorth = _controls.Battle.North;
            _buttonSouth = _controls.Battle.South;
            _buttonEast = _controls.Battle.East;
            _buttonWest = _controls.Battle.West;
        }

        private void OnEnable()
        {
            _controls?.Battle.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _controls?.Battle.Disable();
        }

        protected override void Update()
        {
            base.Update();

            switch(_battleState)
            {
                case BattleStates.EnterBattle: EnterBattle(); break;
                case BattleStates.Wait: Wait(); break;
                case BattleStates.ChooseAction: ChooseAction(); break;
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            switch(_battleState)
            {

            }
        }

        private void CheckInput()
        {
            PressedJump = _jump.triggered;
            PressedButtonNorth = _buttonNorth.triggered;
            PressedButtonEast = _buttonEast.triggered;
            PressedButtonSouth = _buttonSouth.triggered;
            PressedButtonWest = _buttonWest.triggered;
        }

        protected override void EnterBattle()
        {
            base.EnterBattle();
            _menuChoice.Variable.Value = 0;
        }

        protected void ChooseAction()
        {
            // menu options
            const int FIGHT = 0;
            const int CHARGE = 1;
            const int ITEM = 2;

            MenuQoL.ScrollThroughMenu(ref _movePressed, ref _menuChoice.Variable.Value, _movement);
        }
    }
}

