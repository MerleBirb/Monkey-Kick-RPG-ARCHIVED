// Merle Roji
// 10/14/21

using UnityEngine;
using MonkeyKick.Managers;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.RPGSystem.Characters
{
    public class ManageCharacterState : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        #region CHARACTER COMPONENTS

        private CharacterMovement _movement;
        private CharacterBattle _battle;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            _movement = GetComponent<CharacterMovement>();
            _battle = GetComponent<CharacterBattle>();

            _movement.OnBattleStart += EnterBattle;
            _battle.OnBattleEnd += ExitBattle;

            MenuQoL.OnOpenOverworldMenu += PauseMovement;
            MenuQoL.OnCloseOverworldMenu += ResumeMovement;

            gameManager.OnDialogueEnd += ResumeMovement;
        }

        private void OnDestroy()
        {
            _movement.OnBattleStart -= EnterBattle;
            _battle.OnBattleEnd -= ExitBattle;

            MenuQoL.OnOpenOverworldMenu -= PauseMovement;
            MenuQoL.OnCloseOverworldMenu -= ResumeMovement;

            gameManager.OnDialogueEnd -= ResumeMovement;
        }

        #endregion

        public void PauseMovement()
        {
            _movement.enabled = false;
        }

        public void ResumeMovement()
        {
            _movement.enabled = true;
        }

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
