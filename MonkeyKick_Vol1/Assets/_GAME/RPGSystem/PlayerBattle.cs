//===== PLAYER BATTLE =====//
/*
5/26/21
Description:
- Holds player battle logic in the battle game state.

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.InputSystem;

namespace MonkeyKick.RPGSystem
{
    public class PlayerBattle : CharacterBattle
    {
        private PlayerControls input;

        private void Awake()
        {
            input = new PlayerControls();
            InputSystem.pollingFrequency = 180;
        }

        private void OnEnable()
        {

        }

        private void OnDisable()
        {
            
        }
    }
}