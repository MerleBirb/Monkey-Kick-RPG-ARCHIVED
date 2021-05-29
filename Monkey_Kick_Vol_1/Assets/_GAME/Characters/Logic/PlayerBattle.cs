//===== PLAYER BATTLE =====//
/*
5/26/21
Description:
- Holds player battle logic in the battle game state.

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.InputSystem;

namespace Merlebirb.CharacterLogic
{
    public class PlayerBattle : CharacterBattle
    {
        private PlayerInput input;

        #region MENU CONTROLS

        private const string Move = "Move";
        private const string Select = "Select";
        private const string Cancel = "Cancel";

        private InputAction move;
        private InputAction select;
        private InputAction cancel;

        private Vector2 movement;
        private bool stickPressed = false;
        private bool hasSelected = false;
        private bool hasCanceled = false;

        #endregion

        private void Awake()
        {
            input = GetComponent<PlayerInput>();
        }

        public override void Start()
        {
            base.Start();

            InputSystem.pollingFrequency = 180;
            input.SwitchCurrentActionMap("BattleMenu");

            move = input.actions.FindAction(Move);
            select = input.actions.FindAction(Select);
            cancel = input.actions.FindAction(Cancel);

            move.performed += context => movement = context.ReadValue<Vector2>();
        }

        public override void Update()
        {
            base.Update();
        }

        private void OnEnable()
        {
            ResetControls();
        }

        private void OnDisable()
        {
            ResetControls();
        }

        private void ResetControls()
        {
            stickPressed = false;
            hasSelected = false;
            hasCanceled = false;
        }
    }
}