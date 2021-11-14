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

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (gameManager.GameState == GameStates.Battle)
            {
                switch (_battleState)
                {
                    case BattleStates.Action:
                    {
                        Stats.SkillList[0].FixedTick();

                        break;
                    }
                }

            }
        }

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
                    case BattleStates.Action:
                    {
                        Stats.SkillList[0].Tick();

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
            AnimationQoL.ChangeAnimation(_anim, _currentState, BATTLE_STANCE_L);
        }

        protected void ChooseAction()
        {
            // save battle position for returning from skills and counterattacks
            _battlePos.x = transform.position.x;
            _battlePos.y = transform.position.z;

            Stats.SkillList[0].Init(this, new CharacterBattle[] { _turnSystem.PlayerParty[0] });
            _battleState = BattleStates.Action;
        }

        protected void CheckInterrupt()
        {
            
        }

        #endregion
    }
}