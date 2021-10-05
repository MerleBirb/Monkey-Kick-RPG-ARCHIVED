//===== SKILL CONTROLS (OBSELETE) =====//
/*
7/12/21
Description:
- Holds the controller inputs for skills and counterattacks

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.Controls;

namespace MonkeyKick.Skills
{
    public class SkillControls
    {
        public readonly PlayerControls input;
        public readonly InputAction joystick;
        public Vector2 stickMove;
        public readonly InputAction northButton;
        public readonly InputAction southButton;
        public readonly InputAction eastButton;
        public readonly InputAction westButton;

        public SkillControls(PlayerControls input)
        {
            this.input = input;

            joystick = input.Battle.Joystick;
            stickMove = Vector2.zero;
            joystick.performed += context => stickMove = context.ReadValue<Vector2>();

            northButton = input.Battle.North;
            southButton = input.Battle.South;
            eastButton = input.Battle.East;
            westButton = input.Battle.West;
        }
    }
}
