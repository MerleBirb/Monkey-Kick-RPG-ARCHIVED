//===== CHARACTER BATTLE =====//
/*
5/23/21
Description:
- Abstract class to hold general information for all character logic in the battle game state.

*/

using UnityEngine;
using UnityEngine.Events;
using MonkeyKick.Managers;

namespace MonkeyKick.Battle
{
    public abstract class CharacterBattle : MonoBehaviour
    {
        [SerializeField] private GameStateData Game;

        protected BattleStates _state;
        protected bool _isTurn = false;
        protected UnityEvent _currentAction;

        [HideInInspector] public bool finishAction = false;
        [HideInInspector] public Rigidbody rb;
        public CharacterInformation Stats;
        [HideInInspector] public TurnClass Turn; // stores turn information

        public virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            _currentAction = new UnityEvent();
        }

        public virtual void Start()
        {
            if (!Game.CompareGameState(GameStates.Battle)) { enabled = false; }
            else
            {
                _state = BattleStates.EnterBattle;

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

            if (_isTurn != Turn.isTurn) { _isTurn = Turn.isTurn; }
        }

        public virtual void EnterBattle() // sets the initial state
        {
            _state = BattleStates.Wait;
        }

        public virtual void Wait()
        {
            if (_isTurn) { _state = BattleStates.Action; }
        }

        public virtual void Reset() // sets the turn to false
        {
            _isTurn = false;
            Turn.isTurn = _isTurn;
            Turn.wasTurnPrev = true;

            if (!_isTurn) { _state = BattleStates.Wait; }
        }
        public virtual void Action(Skill skill, CharacterBattle target) // use the skill chosen
        {
            _currentAction.RemoveAllListeners();
            _currentAction.AddListener(() => skill.Action(this, target));
            _currentAction.Invoke();
            _state = BattleStates.Action;
        }

        public void ChangeBattleState(BattleStates newState)
        {
            if (_state == newState) { return; }
            _state = newState;
        }

        public virtual void Kill()
        {
            Stats.isAlive = false;
            gameObject.SetActive(false);
        }
    }
}