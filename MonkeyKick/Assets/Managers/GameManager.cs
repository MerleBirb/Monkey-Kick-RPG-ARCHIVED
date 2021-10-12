// Merle Roji
// 10/11/21

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
        [Header("The state the game currently is in.")]
        public GameStates GameState;
    }
}
