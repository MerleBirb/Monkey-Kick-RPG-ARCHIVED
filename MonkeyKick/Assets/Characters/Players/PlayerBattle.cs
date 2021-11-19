// Merle Roji
// 10/13/21

using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.Controls;
using MonkeyKick.Managers;
using MonkeyKick.QualityOfLife;
using MonkeyKick.References;

namespace MonkeyKick.RPGSystem.Characters
{
    public class PlayerBattle : CharacterBattle
    {
        #region CONTROLS

        private PlayerControls _controls;
        protected InputAction _move;
        private InputAction _select;
        private InputAction _jump;
        [HideInInspector] public bool pressedJump;
        private Vector2 _movement;
        private bool _movePressed = false;
        private InputAction _buttonNorth;
        public InputAction ButtonNorth { get => _buttonNorth; }
        [HideInInspector] public bool pressedButtonNorth;
        private InputAction _buttonEast;
        public InputAction ButtonEast { get => _buttonEast; }
        [HideInInspector] public bool pressedButtonEast;
        private InputAction _buttonSouth;
        public InputAction ButtonSouth { get => _buttonSouth; }
        [HideInInspector] public bool pressedButtonSouth;
        private InputAction _buttonWest;
        public InputAction ButtonWest { get => _buttonWest; }
        [HideInInspector] public bool pressedButtonWest;

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
            _jump = _controls.Battle.Jump;
            _buttonNorth = _controls.Battle.North;
            _buttonSouth = _controls.Battle.South;
            _buttonEast = _controls.Battle.East;
            _buttonWest = _controls.Battle.West;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (gameManager.GameState == GameStates.Battle)
            {
                switch(_battleState)
                {
                    case BattleStates.Action:
                    {
                        Stats.SkillList[0].FixedTick();

                        break;
                    }
                }
            }
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
                    case BattleStates.Action:
                    {
                        CheckInput();
                        Stats.SkillList[0].Tick();

                        break;
                    }
                    case BattleStates.Counter: 
                    {
                        CheckInput();
                        if (_turnSystem.EnemyPartyDefeated()) { OnBattleEnd.Invoke(); }

                        break;
                    }
                }

            }
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

        #endregion

        #region GENERAL METHODS

        private void CheckInput()
        {
            pressedJump = _jump.triggered;
            pressedButtonNorth = _buttonNorth.triggered;
            pressedButtonSouth = _buttonSouth.triggered;
            pressedButtonEast = _buttonEast.triggered;
            pressedButtonWest = _buttonWest.triggered;
        }

        protected override void EnterBattle()
        {
            base.EnterBattle();
            menuChoice.Variable.Value = 0; // reset it every battle
            AnimationQoL.ChangeAnimation(_anim, _currentState, BATTLE_STANCE_R); // get into battle idle
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

                        // initiate the skill
                        Stats.SkillList[0].Init(this, new CharacterBattle[] { _turnSystem.EnemyParty[0] });

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
