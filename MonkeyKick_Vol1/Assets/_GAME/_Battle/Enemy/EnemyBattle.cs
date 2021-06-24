//===== ENEMY BATTLE =====//
/*
5/26/21
Description:
- Holds enemy battle logic in the battle game state.

Author: Merlebirb
*/

using UnityEngine;

namespace MonkeyKick.Battle
{
    public class EnemyBattle : CharacterBattle
    {
        public override void Update()
        {
            base.Update();

            switch(_state)
            {
                case BattleStates.EnterBattle: EnterBattle(); break;
                case BattleStates.Wait: Wait(); break;
                case BattleStates.Action: break;
                case BattleStates.Reset: Reset(); break;
            }
        }

        public override void Wait()
        {
            if (_isTurn) { Action(Stats.skillList[0], Turn.turnSystem.playerList[0]); _state = BattleStates.Action; }
        }
    }
}