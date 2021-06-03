//===== PLAYER BATTLE =====//
/*
5/26/21
Description:
- Holds player battle logic in the battle game state.

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.Overworld;
using MonkeyKick.References;

namespace MonkeyKick.Battle
{
    public class PlayerBattle : CharacterBattle
    {
        private PlayerControls input;

        #region CONTROLS

        [SerializeField] private IntReference menuChoice;

        private InputAction moveSelector;
        private InputAction select;
        private InputAction cancel;

        private Vector2 movementMenu;
        private bool movePressed = false;

        #endregion

        public override void Awake()
        {
            base.Awake();

            input = new PlayerControls();
            InputSystem.pollingFrequency = 180;

            moveSelector = input.BattleMenu.MoveSelector;
            select = input.BattleMenu.Select;
            cancel = input.BattleMenu.Cancel;

            moveSelector.performed += context => movementMenu = context.ReadValue<Vector2>();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            switch(state)
            {
                case BattleStates.EnterBattle: EnterBattle(); break;
                case BattleStates.Wait: Wait(); break;
                case BattleStates.NavigateMenu: NavigateMenu(); break;
                case BattleStates.Action: Action(); break;
            }
        }

        public override void Wait()
        {
            if (isTurn)
            {
                menuChoice.Variable.Value = 0;
                state = BattleStates.NavigateMenu;
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

            if (movementMenu.y < -_deadZone)
            {
                if (!movePressed)
                {
                    menuChoice.Variable.Value++;
                    movePressed = true;
                }
            }
            else if (movementMenu.y > _deadZone)
            {
                if (!movePressed)
                {
                    menuChoice.Variable.Value--;
                    movePressed = true;
                }
            }
            else
            {
                movePressed = false;
            }

            if (select.triggered)
            {
                switch(menuChoice.Variable.Value)
                {
                    case FIGHT: state = BattleStates.Action; break;
                    case CHARGE: break;
                    case ITEMS: break;
                }
            }
        }

        private void Action()
        {
            Stats.skillList[0].Action(this, Turn.turnSystem.enemyList[0]);
        }

        private void OnEnable() => input.BattleMenu.Enable();

        private void OnDisable() => input.BattleMenu.Disable();
    }
}