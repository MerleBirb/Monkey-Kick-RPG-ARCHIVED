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
                    case BattleStates.EnterBattle: EnterBattle(); break;
                    case BattleStates.Wait: Wait(); break;
                    case BattleStates.ChooseAction: ChooseAction(); break;
                    case BattleStates.Action: break;
                }

            } 
        }

        #endregion

        #region METHODS

        protected override void EnterBattle()
        {
            base.EnterBattle();
            AnimationQoL.ChangeAnimation(_anim, _currentState, BATTLE_STANCE, true);
            if (_turnSystem.TurnSystemLoaded && _physics.OnGround()) _battleState = BattleStates.Wait;
        }

        protected virtual void ChooseAction()
        {
                // save battle position for returning from skills and counterattacks
                _battlePos.x = transform.position.x;
                _battlePos.y = transform.position.z;

            Stats.SkillList[0].Action(this, _turnSystem.PlayerParty[0]);
            _battleState = BattleStates.Action;
        }

        #endregion
    }
}