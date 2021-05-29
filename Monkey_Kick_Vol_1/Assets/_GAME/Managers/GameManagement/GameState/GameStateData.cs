//===== GAME STATE DATA =====//
/*
5/23/21
Description:
- The game state.
- Handles what logic is run and what isnt at the time.

Author: Merlebirb
*/

using UnityEngine;

namespace Merlebirb.Managers
{
    [CreateAssetMenu(menuName = "New Data/Game State Data", fileName = "GameState")]
    public class GameStateData : ScriptableObject
    {
        [SerializeField] private GameStates GameState;

        public GameStates GetGameState() { return GameState; }
        public void SetGameState(GameStates _newState)
        {
            if (GameState == _newState) return;

            GameState = _newState;
        }

        public bool CompareGameState(GameStates _comparisonState)
        {
            bool _isTheSame = false;

            if (GameState == _comparisonState)
            {
                _isTheSame = true;
            }

            return _isTheSame;
        }
    }
}