// Merle Roji
// 10/14/21

using UnityEngine;
using MonkeyKick.Managers;

namespace MonkeyKick.Characters
{
    public class ManageCharacterState : MonoBehaviour
    {
        #region CHARACTER COMPONENTS

        private CharacterMovement _movement;
        private CharacterBattle _battle;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            _movement = GetComponent<CharacterMovement>();
            _battle = GetComponent<CharacterBattle>();
        }

        private void Start()
        {
            _movement.OnBattleStart += EnterBattle;
            _battle.OnBattleEnd += ExitBattle;
        }

        #endregion

        public void EnterBattle ()
        {
            _battle.enabled = true; // turn on battle script

            // turn every other character component off
            _movement.enabled = false;
        }

        public void ExitBattle()
        {
            _movement.enabled = true; // turn on movement script

            // turn every other character component off
            _battle.enabled = false;
        }
    }
}
