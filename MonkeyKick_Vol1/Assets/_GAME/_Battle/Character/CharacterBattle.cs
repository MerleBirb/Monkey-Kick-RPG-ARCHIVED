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

        protected BattleStates state;
        protected bool isTurn = false;

        [HideInInspector] public bool finishAction = false;
        [HideInInspector] public Rigidbody rb;
        public CharacterInformation Stats;
        [HideInInspector] public TurnClass Turn; // stores turn information

        public virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public virtual void Start()
        {
            if (!Game.CompareGameState(GameStates.Battle)) { enabled = false; }
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

        public virtual void FixedUpdate()
        {
            if (!Game.CompareGameState(GameStates.Battle)) { this.enabled = false; }

            if (isTurn != Turn.isTurn) { isTurn = Turn.isTurn; }
        }

        public virtual void EnterBattle() // sets the initial state
        {
            state = BattleStates.Wait;
        }

        public virtual void Wait() // waits until is turn
        {
            if (isTurn) { state = BattleStates.Action; }
        }

        public void ChangeBattleState(BattleStates newState)
        {
            if (state == newState) { return; }
            state = newState;
        }
    }
}