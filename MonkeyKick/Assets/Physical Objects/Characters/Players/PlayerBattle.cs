// Merle Roji
// 10/13/21

using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.Controls;
using MonkeyKick.Managers;
using MonkeyKick.QualityOfLife;
using MonkeyKick.References;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public class PlayerBattle : CharacterBattle
    {
        #region CONTROLS

        private PlayerControls _controls;
        private InputAction _move;
        private InputAction _select;
        private InputAction _buttonSouth;
        private Vector2 _movement;
        private bool _movePressed = false;
        [SerializeField] private IntReference menuChoice;

        #endregion

        #region UNITY METHODS

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
            _buttonSouth = _controls.Battle.South;
        }

        protected override void Update()
        {
            base.Update();

            if (gameManager.GameState == GameStates.Battle)
            {
                switch (_battleState)
                {
                    case BattleStates.EnterBattle: EnterBattle(); break;
                    case BattleStates.Wait: Wait(); break;
                    case BattleStates.ChooseAction: ChooseAction(); break;
                    case BattleStates.Action: break;
                }

            } 
        }

        private void OnEnable()
        {
            _controls?.Battle.Enable();
        }

        private void OnDisable()
        {
            _controls?.Battle.Disable();
        }

        #endregion

        #region METHODS

        protected override void EnterBattle()
        {
            base.EnterBattle();
            menuChoice.Variable.Value = 0; // reset it every battle
            AnimationQoL.ChangeAnimation(_anim, _currentState, BATTLE_STANCE); // get into battle idle
        }

        protected virtual void ChooseAction()
        {
            // menu options
            const float deadzone = 0.3f;
            const int FIGHT = 0;
            const int CHARGE = 1;
            const int ITEM = 2;

            // scrolling through the menu
            if (_movement.y < -deadzone)
            {
                if (!_movePressed)
                {
                    menuChoice.Variable.Value++;
                    _movePressed = true;
                }
            }
            else if (_movement.y > deadzone)
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

            if (_select.triggered)
            {
                switch(menuChoice.Variable.Value)
                {
                    case FIGHT:
                    {
                        // save battle position for returning from skills and counterattacks
                        _battlePos.x = transform.position.x;
                        _battlePos.y = transform.position.z;

                        Stats.SkillList[0].Action(this, _turnSystem.EnemyParty[0]);
                        _battleState = BattleStates.Action;

                        break;
                    }
                    case CHARGE: break;
                    case ITEM: break;
                }
            }
        }

        #endregion
    }
}
