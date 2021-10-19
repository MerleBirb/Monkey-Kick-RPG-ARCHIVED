// Merle Roji
// 10/11/21

using System;
using UnityEngine;

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
        [SerializeField] private GameStates gameState = GameStates.Overworld;
        public GameStates GameState
        {
            get { return gameState; }
            set 
            {
                Type t = value.GetType(); // check if type is of GameStates
                if (t.Equals(typeof(GameStates))) gameState = value;
            }
        }

        public void InitiateBattle(Vector3 camPos)
        {
            GameState = GameStates.Battle;
            OnBattleStart.Invoke(camPos);
        }

        #endregion

        #region GAME EVENTS

        public delegate void BattleTrigger(Vector3 camPos);
        public event BattleTrigger OnBattleStart;

        #endregion
    }
}
