//===== CHARACTER BATTLE =====//
/*
5/23/21
Description:
- Abstract class to hold general information for all character logic in the battle game state.

*/

using UnityEngine;
using Merlebirb.RPGSystem;
using Merlebirb.Managers;

namespace Merlebirb.CharacterLogic
{
    public abstract class CharacterBattle : MonoBehaviour
    {
        [SerializeField] private GameStateData Game;

        protected BattleStates state;

        public CharacterInformation Stats;
        [ReadOnly] public TurnClass Turn;

        public virtual void Start()
        {
            if (!Game.CompareGameState(GameStates.Battle)) { this.enabled = false; }
            else
            {
                state = BattleStates.EnterBattle;

                foreach (var tc in Turn.turnSystem.GetTurnOrder())
                {
                    if (tc.charName == Stats.CharacterName)
                    {
                        Turn = tc;
                    }
                }
            }
        }

        public virtual void Update()
        {
            if (!Game.CompareGameState(GameStates.Battle)) { this.enabled = false; }
        }

        public virtual void BattleStateMachine()
        {

        }
    }
}