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

namespace MonkeyKick.Battle
{
    public class PlayerBattle : CharacterBattle
    {
        private PlayerControls input;

        #region CONTROLS

        private InputAction moveSelector;
        private InputAction select;
        private InputAction cancel;

        private bool movePressed = false;
        private bool selectPressed = false;
        private bool cancelPressed = false;

        #endregion

        public override void Awake()
        {
            base.Awake();

            input = new PlayerControls();
            InputSystem.pollingFrequency = 180;

            // continue here
        }

        private void OnEnable()
        {
            input.BattleMenu.Enable();
        }

        private void OnDisable()
        {
            input.BattleMenu.Disable();
        }
    }
}