//===== PLAYER BATTLE =====//
/*
5/26/21
Description:
- Holds player battle logic in the battle game state.

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.References;
using MonkeyKick.Controls;
using MonkeyKick.QoL;

namespace MonkeyKick.Battle
{
    public class PlayerBattle : CharacterBattle
    {
        private PlayerControls _input;

        const string BATTLE_STANCE = "BattleStance";

        [SerializeField] private IntReference menuChoice;

        #region MENU CONTROLS

        private InputAction _moveSelector;
        private InputAction _select;
        private InputAction _cancel;

        #endregion

        #region BATTLE CONTROLS

        private InputAction _joystick;
        private InputAction _northButton;
        private InputAction _southButton;
        private InputAction _eastButton;
        private InputAction _westButton;
        [HideInInspector] public Vector2 joystickMove;
        [HideInInspector] public bool northPressed = false;
        [HideInInspector] public bool southPressed = false;
        [HideInInspector] public bool eastPressed = false;
        [HideInInspector] public bool westPressed = false;

        #endregion

        private Vector2 _movementMenu;
        private bool _movePressed = false;
        private bool _selectPressed = false;

        public override void Awake()
        {
            base.Awake();

            InputSystem.pollingFrequency = 180;
            _input = new PlayerControls();

            // menu controls
            _moveSelector = _input.BattleMenu.MoveSelector;
            _select = _input.BattleMenu.Select;
            _cancel = _input.BattleMenu.Cancel;
            _moveSelector.performed += context => _movementMenu = context.ReadValue<Vector2>();

            // battle controls
            _joystick = _input.Battle.Joystick;
            _joystick.performed += context => joystickMove = context.ReadValue<Vector2>();

            _northButton = _input.Battle.North;
            _southButton = _input.Battle.South;
            _eastButton = _input.Battle.East;
            _westButton = _input.Battle.West;
        }

        // separate battle logic between normal update and fixed update

        public override void Update()
        {
            base.Update();
            CheckMenuInput();

            switch(_state)
            {
                case BattleStates.EnterBattle:
                {
                    AnimQoL.PlayAnimation(_anim, _currentAnim, BATTLE_STANCE);
                    EnterBattle();
                    break;
                } 
                case BattleStates.Wait: Wait(); break;
                case BattleStates.NavigateMenu: NavigateMenu(); break;
                case BattleStates.Action: CheckActionInput(); break;
                case BattleStates.Reset: Reset(); break;
            }
        }

        public override void Wait() // wait until it's the character's turn
        {
            if (_isTurn)
            {
                menuChoice.Variable.Value = 0;
                _state = BattleStates.NavigateMenu;
            }
        }

        private void NavigateMenu() // navigates through the battle menu
        {
            const float _deadZone = 0.5f;

            #region MENU OPTIONS

            const int FIGHT = 0;
            const int CHARGE = 1;
            const int ITEMS = 2;

            #endregion

            if(finishAction) finishAction = false;

            if (_movementMenu.y < -_deadZone)
            {
                if (!_movePressed)
                {
                    menuChoice.Variable.Value++;
                    _movePressed = true;
                }
            }
            else if (_movementMenu.y > _deadZone)
            {
                if (!_movePressed)
                {
                    menuChoice.Variable.Value--;
                    _movePressed = true;
                }
            }
            else
            {
                _movePressed = false;
            }

            if (_selectPressed)
            {
                switch(menuChoice.Variable.Value)
                {
                    case FIGHT: Action(Stats.skillList[0], Turn.turnSystem.enemyList[0]); break;
                    case CHARGE: break;
                    case ITEMS: break;
                }
            }
        }

        private void CheckMenuInput()
        {
            _selectPressed = _select.triggered;
        }

        public void CheckActionInput()
        {
            northPressed = _northButton.triggered;
            southPressed = _southButton.triggered;
            eastPressed = _eastButton.triggered;
            westPressed = _westButton.triggered;
        }

        public override void Kill()
        {
            base.Kill();
            Turn.turnSystem.RemovePlayerFromParty(this);
        }

        private void OnEnable()
        {
            _input.BattleMenu.Enable();
            _input.Battle.Enable();
        }

        private void OnDisable()
        {
            _input.BattleMenu.Disable();
            _input.Battle.Disable();
        }
    }
}