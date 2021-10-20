// Merle Roji
// 10/13/21

using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.Controls;
using MonkeyKick.Managers;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public class PlayerBattle : CharacterBattle
    {
        #region CONTROLS

        private PlayerControls _controls;
        private InputAction _buttonSouth;

        #endregion

        #region UNITY METHODS

        protected override void Awake()
        {
            base.Awake();

            // create new instance of controls
            InputSystem.pollingFrequency = 180;
            _controls = new PlayerControls();

            // set controls
            _buttonSouth = _controls.Battle.South;
        }

        protected override void Update()
        {
            base.Update();

            if (gameManager.GameState == GameStates.Battle)
            {
                switch (_battleState)
                {
                    case BattleState.EnterBattle: EnterBattle(); break;
                    case BattleState.Wait: Wait(); break;
                }

            } 
        }

        private void OnEnable()
        {
            _controls.Battle.Enable();
        }

        private void OnDisable()
        {
            _controls.Battle.Disable();
        }

        #endregion

        #region METHODS

        protected override void EnterBattle()
        {
            base.EnterBattle();
            AnimationQoL.ChangeAnimation(_anim, _currentState, BATTLE_STANCE);
            _battleState = BattleState.Wait;
        }

        private void Wait()
        {
            if (_isTurn)
            {
                if (_buttonSouth.triggered)
                {
                    _physics.GetRigidbody().AddForce(Vector3.up * 300f);
                    _isTurn = false;
                    Turn.isTurn = _isTurn;
                    Turn.wasTurnPrev = true;
                }
            }
        }

        #endregion
    }
}
