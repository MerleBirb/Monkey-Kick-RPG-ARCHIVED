// Merle Roji
// 10/13/12

using UnityEngine;
using MonkeyKick.Managers;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public class EnemyBattle : CharacterBattle
    {
        #region UNITY METHODS

        private void Update()
        {
            if (gameManager.GameState == GameStates.Battle)
            {
                switch (_battleState)
                {
                    case BattleState.EnterBattle:
                        {
                            EnterBattle();
                            break;
                        }
                }

            } 
        }

        #endregion

        #region METHODS

        protected override void EnterBattle()
        {
            base.EnterBattle();
            AnimationQoL.ChangeAnimation(_anim, _currentState, BATTLE_STANCE, true);
            _battleState = BattleState.Wait;
        }

        #endregion
    }
}
