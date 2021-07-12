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

namespace MonkeyKick.Battle
{
    public class PlayerBattle : CharacterBattle
    {
        private PlayerControls _input;

        [SerializeField] private IntReference menuChoice;

        #region MENU CONTROLS

        private InputAction _moveSelector;
        private InputAction _select;
        private InputAction _cancel;

        #endregion

        #region BATTLE CONTROLS

        [HideInInspector] public SkillControls skillControls;

        #endregion

        private Vector2 _movementMenu;
        private bool _movePressed = false;
        private bool _selectPressed = false;

        public override void Awake()
        {
            base.Awake();

            _input = new PlayerControls();
            InputSystem.pollingFrequency = 180;

            _moveSelector = _input.BattleMenu.MoveSelector;
            _select = _input.BattleMenu.Select;
            _cancel = _input.BattleMenu.Cancel;

            _moveSelector.performed += context => _movementMenu = context.ReadValue<Vector2>();

            skillControls = new SkillControls(_input);
        }

        // separate battle logic between normal update and fixed update

        public override void Update()
        {
            base.Update();
            CheckInput();

            switch(_state)
            {
                case BattleStates.EnterBattle: EnterBattle(); break;
                case BattleStates.Wait: Wait(); break;
                case BattleStates.NavigateMenu: NavigateMenu(); break;
                case BattleStates.Action: break;
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

        private void CheckInput()
        {
            _selectPressed = _select.triggered;
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