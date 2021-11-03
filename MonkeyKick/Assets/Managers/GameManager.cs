// Merle Roji
// 10/11/21

using System.Collections.Generic;
using UnityEngine;
using MonkeyKick.PhysicalObjects.Characters;

namespace MonkeyKick.Managers
{
    public enum GameStates
    {
        Overworld,
        Battle
    }

    [CreateAssetMenu(fileName = "GameManager", menuName = "Managers/Game Manager", order = 1)]
    public class GameManager : ScriptableObject
    {
        #region GAME STATE

        [Header("The state the game currently is in.")]
        [SerializeField] private GameStates _gameState = GameStates.Overworld;
        public GameStates GameState
        {
            get => _gameState;
            set => _gameState = value;
        }

        private List<CharacterBattle> _currentFighters = new List<CharacterBattle>(); // fighters in the turn order
        public List<CharacterBattle> CurrentFighters { get { return _currentFighters; } }

        public void InitiateBattle(Vector3 camPos, List<CharacterBattle> newFighters)
        {
            _currentFighters.Clear(); // clear the fighters list in case there are any
            _currentFighters.AddRange(newFighters); // add the new fighters to the current fighters list
            _gameState = GameStates.Battle;
            OnBattleStart.Invoke(camPos);
        }

        public void EndBattle()
        {
            _gameState = GameStates.Overworld;
            OnBattleEnd.Invoke();
        }

        public void ClearCurrentFighters()
        {
            _currentFighters.Clear();
        }

        #endregion

        #region GAME EVENTS

        public delegate void BattleStartTrigger(Vector3 camPos);
        public event BattleStartTrigger OnBattleStart;

        public delegate void BattleEndTrigger();
        public event BattleEndTrigger OnBattleEnd;

        #endregion
    }
}
