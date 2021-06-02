//===== CHARACTER BATTLE =====//
/*
5/23/21
Description:
- Abstract class to hold general information for all character logic in the battle game state.

*/

using UnityEngine;
using Unity.Collections;
using MonkeyKick.Managers;
using MonkeyKick.Overworld;

namespace MonkeyKick.Battle
{
    public abstract class CharacterBattle : MonoBehaviour
    {
        [SerializeField] private GameStateData Game;
        private bool isTurn = false;

        protected BattleStates state;

        public Rigidbody rb;
        public CharacterInformation Stats;
        [ReadOnly] public TurnClass Turn;

        public virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

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

        public virtual void EnterBattle()
        {
            state = BattleStates.Wait;
        }

        public virtual void Wait()
        {
            isTurn = Turn.isTurn;
            if (isTurn) { state = BattleStates.Active; }
        }

        public virtual void Active()
        {
            
        }

        public void ChangeBattleState(BattleStates newState)
        {
            if (state == newState) { return; }
            state = newState;
        }
    }
}