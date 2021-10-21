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

        #endregion

        #region METHODS

        protected override void EnterBattle()
        {
            base.EnterBattle();
            AnimationQoL.ChangeAnimation(_anim, _currentState, BATTLE_STANCE, true);
            _battleState = BattleState.Wait;
        }

        private void Wait()
        {
            if (_isTurn)
            {
                _physics.GetRigidbody().AddForce(Vector3.up * 300f);
                _isTurn = false;
                Turn.isTurn = _isTurn;
                Turn.wasTurnPrev = true;
            }
        }

        #endregion
    }
}
