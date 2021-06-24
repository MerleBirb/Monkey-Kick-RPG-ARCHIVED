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
                case BattleStates.Reset: Reset(); break;
            }
        }

        private void FixedUpdate()
        {
            switch(_state)
            {
                case BattleStates.Action: Action(); break;
            }
        }

        public override void Wait()
        {
            if (_isTurn) { _state = BattleStates.Action; }
        }

        private void Action() // use the skill chosen
        {
            Stats.skillList[0].Action(this, Turn.turnSystem.playerList[0]);
        }
    }
}